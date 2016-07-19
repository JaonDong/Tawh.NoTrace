﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Tawh.NoTrace.Auditing.Dto;
using Tawh.NoTrace.Auditing.Exporting;
using Tawh.NoTrace.Authorization;
using Tawh.NoTrace.Authorization.Users;
using Tawh.NoTrace.Dto;

namespace Tawh.NoTrace.Auditing
{
    [DisableAuditing]
    [AbpAuthorize(AppPermissions.Pages_Administration_AuditLogs)]
    public class AuditLogAppService : AbpZeroTemplateAppServiceBase, IAuditLogAppService
    {
        private readonly IRepository<AuditLog, long> _auditLogRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IAuditLogListExcelExporter _auditLogListExcelExporter;

        public AuditLogAppService(IRepository<AuditLog, long> auditLogRepository, IRepository<User, long> userRepository, IAuditLogListExcelExporter auditLogListExcelExporter)
        {
            _auditLogRepository = auditLogRepository;
            _userRepository = userRepository;
            _auditLogListExcelExporter = auditLogListExcelExporter;
        }
        
        public async Task<PagedResultOutput<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input)
        {
            var query = CreateAuditLogAndUsersQuery(input);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var auditLogListDtos = ConvertToAuditLogListDtos(results);

            return new PagedResultOutput<AuditLogListDto>(resultCount, auditLogListDtos);
        }

        public async Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input)
        {
            var auditLogs = await CreateAuditLogAndUsersQuery(input)
                        .AsNoTracking()
                        .OrderByDescending(al => al.AuditLog.ExecutionTime)
                        .ToListAsync();

            var auditLogListDtos = ConvertToAuditLogListDtos(auditLogs);

            return _auditLogListExcelExporter.ExportToFile(auditLogListDtos);
        }

        private static List<AuditLogListDto> ConvertToAuditLogListDtos(List<AuditLogAndUser> results)
        {
            return results.Select(
                result =>
                {
                    var auditLogListDto = result.AuditLog.MapTo<AuditLogListDto>();
                    auditLogListDto.UserName = result.User == null ? null : result.User.UserName;
                    auditLogListDto.ServiceName = StripNameSpace(auditLogListDto.ServiceName);
                    return auditLogListDto;
                }).ToList();
        }

        private IQueryable<AuditLogAndUser> CreateAuditLogAndUsersQuery(GetAuditLogsInput input)
        {
            var query = from auditLog in _auditLogRepository.GetAll()
                join user in _userRepository.GetAll() on auditLog.UserId equals user.Id into userJoin
                from joinedUser in userJoin.DefaultIfEmpty()
                where auditLog.ExecutionTime >= input.StartDate && auditLog.ExecutionTime < input.EndDate
                select new AuditLogAndUser {AuditLog = auditLog, User = joinedUser};

            query = query
                .WhereIf(!input.UserName.IsNullOrWhiteSpace(), item => item.User.UserName.Contains(input.UserName))
                .WhereIf(!input.ServiceName.IsNullOrWhiteSpace(), item => item.AuditLog.ServiceName.Contains(input.ServiceName))
                .WhereIf(!input.MethodName.IsNullOrWhiteSpace(), item => item.AuditLog.MethodName.Contains(input.MethodName))
                .WhereIf(!input.BrowserInfo.IsNullOrWhiteSpace(), item => item.AuditLog.BrowserInfo.Contains(input.BrowserInfo))
                .WhereIf(input.MinExecutionDuration.HasValue && input.MinExecutionDuration > 0, item => item.AuditLog.ExecutionDuration >= input.MinExecutionDuration.Value)
                .WhereIf(input.MaxExecutionDuration.HasValue && input.MaxExecutionDuration < int.MaxValue, item => item.AuditLog.ExecutionDuration <= input.MaxExecutionDuration.Value)
                .WhereIf(input.HasException == true, item => item.AuditLog.Exception != null && item.AuditLog.Exception != "")
                .WhereIf(input.HasException == false, item => item.AuditLog.Exception == null || item.AuditLog.Exception == "");
            return query;
        }

        private static string StripNameSpace(string serviceName)
        {
            if (serviceName.IsNullOrEmpty() || !serviceName.Contains("."))
            {
                return serviceName;
            }

            var lastDotIndex = serviceName.LastIndexOf('.');
            return serviceName.Substring(lastDotIndex + 1);
        }
    }
}
