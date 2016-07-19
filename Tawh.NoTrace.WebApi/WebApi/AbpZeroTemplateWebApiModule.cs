﻿using System.Linq;
using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;
using Abp.WebApi.OData;
using Abp.WebApi.OData.Configuration;
using Swashbuckle.Application;

namespace Tawh.NoTrace.WebApi
{
    /// <summary>
    /// Web API layer of the application.
    /// </summary>
    [DependsOn(typeof(AbpWebApiModule), typeof(AbpWebApiODataModule), typeof(AbpZeroTemplateApplicationModule))]
    public class AbpZeroTemplateWebApiModule : AbpModule
    {
        public override void PreInitialize()
        {
            ConfigureOData();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Automatically creates Web API controllers for all application services of the application
            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(AbpZeroTemplateApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

            ConfigureSwaggerUi(); //Remove this line to disable swagger UI.
        }

        private void ConfigureOData()
        {
            var builder = Configuration.Modules.AbpWebApiOData().ODataModelBuilder;

            //Configure your entities here... see documentation: http://www.aspnetboilerplate.com/Pages/Documents/OData-Integration
            //builder.EntitySet<YourEntity>("YourEntities");
        }

        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Tawh.NoTrace.WebApi");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                })
                .EnableSwaggerUi();
        }
    }
}
