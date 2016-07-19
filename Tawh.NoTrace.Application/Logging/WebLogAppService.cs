using System.IO;
using System.Linq;
using Abp.Authorization;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Tawh.NoTrace.Authorization;
using Tawh.NoTrace.Dto;
using Tawh.NoTrace.IO;
using Tawh.NoTrace.Logging.Dto;
using Tawh.NoTrace.Net.MimeTypes;

namespace Tawh.NoTrace.Logging
{
    [AbpAuthorize(AppPermissions.Pages_Administration_Host_Maintenance)]
    public class WebLogAppService : AbpZeroTemplateAppServiceBase, IWebLogAppService
    {
        private readonly IAppFolders _appFolders;

        public WebLogAppService(IAppFolders appFolders)
        {
            _appFolders = appFolders;
        }

        public GetLatestWebLogsOutput GetLatestWebLogs()
        {
            var directory = new DirectoryInfo(_appFolders.WebLogsFolder);
            var lastLogFile = directory.GetFiles("*.txt", SearchOption.AllDirectories)
                                        .OrderByDescending(f => f.LastWriteTime)
                                        .FirstOrDefault();

            if (lastLogFile == null)
            {
                return new GetLatestWebLogsOutput();
            }

            var lines = AppFileHelper.ReadLines(lastLogFile.FullName).Reverse().Take(100).Reverse().ToList();

            return new GetLatestWebLogsOutput
            {
                LatesWebLogLines = lines
            };
        }

        public FileDto DownloadWebLogs()
        {
            var zipFileDto = new FileDto("WebSiteLogs.zip", MimeTypeNames.ApplicationZip);
            var outputZipFilePath = Path.Combine(_appFolders.TempFileDownloadFolder, zipFileDto.FileToken);

            using (var outputZipFileStream = File.Create(outputZipFilePath))
            {
                using (var zipStream = new ZipOutputStream(outputZipFileStream))
                {
                    var directory = new DirectoryInfo(_appFolders.WebLogsFolder);
                    var logFiles = directory.GetFiles("*.txt", SearchOption.AllDirectories).ToList();

                    foreach (var logFile in logFiles)
                    {
                        var logFileInfo = new FileInfo(logFile.FullName);
                        var logZipEntry = new ZipEntry(logFile.Name)
                        {
                            DateTime = logFileInfo.LastWriteTime,
                            Size = logFileInfo.Length
                        };

                        zipStream.PutNextEntry(logZipEntry);

                        using (var fs = new FileStream(logFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan))
                        {
                            StreamUtils.Copy(fs, zipStream, new byte[4096]);
                        }

                        zipStream.CloseEntry();
                    }

                    // Makes the Close also Close the underlying stream
                    zipStream.IsStreamOwner = true;
                }
            }

            return zipFileDto;
        }
    }
}