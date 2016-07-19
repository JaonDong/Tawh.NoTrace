using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Auditing;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using Tawh.NoTrace.Authorization.Users;
using Tawh.NoTrace.IO;
using Tawh.NoTrace.Net.MimeTypes;
using Tawh.NoTrace.Storage;

namespace Tawh.NoTrace.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : AbpZeroTemplateControllerBase
    {
        private readonly UserManager _userManager;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IAppFolders _appFolders;

        public ProfileController(
            UserManager userManager,
            IBinaryObjectManager binaryObjectManager,
            IAppFolders appFolders)
        {
            _userManager = userManager;
            _binaryObjectManager = binaryObjectManager;
            _appFolders = appFolders;
        }

        [DisableAuditing]
        public async Task<FileResult> GetProfilePicture()
        {
            var user = await _userManager.GetUserByIdAsync(AbpSession.GetUserId());
            if (user.ProfilePictureId == null)
            {
                return GetDefaultProfilePicture();
            }

            return await GetProfilePictureById(user.ProfilePictureId.Value);
        }

        [DisableAuditing]
        public async Task<FileResult> GetProfilePictureById(string id = "")
        {
            if (id.IsNullOrEmpty())
            {
                return GetDefaultProfilePicture();
            }

            return await GetProfilePictureById(Guid.Parse(id));
        }

        [UnitOfWork]
        public virtual async Task<JsonResult> ChangeProfilePicture()
        {
            try
            {
                //Check input
                if (Request.Files.Count <= 0 || Request.Files[0] == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                var file = Request.Files[0];

                if (file.ContentLength > 30720) //30KB.
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit"));
                }

                //Get user
                var user = await _userManager.GetUserByIdAsync(AbpSession.GetUserId());

                //Delete old picture
                if (user.ProfilePictureId.HasValue)
                {
                    await _binaryObjectManager.DeleteAsync(user.ProfilePictureId.Value);
                }

                //Save new picture
                var storedFile = new BinaryObject(file.InputStream.GetAllBytes());
                await _binaryObjectManager.SaveAsync(storedFile);

                //Update new picture on the user
                user.ProfilePictureId = storedFile.Id;

                //Return success
                return Json(new MvcAjaxResponse());
            }
            catch (UserFriendlyException ex)
            {
                //Return error message
                return Json(new MvcAjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        public JsonResult UploadProfilePicture()
        {
            try
            {
                //Check input
                if (Request.Files.Count <= 0 || Request.Files[0] == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                var file = Request.Files[0];

                if (file.ContentLength > 5242880) //1MB.
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit"));
                }

                //Check file type & format
                var fileImage = Image.FromStream(file.InputStream);
                if (!fileImage.RawFormat.Equals(ImageFormat.Jpeg) && !fileImage.RawFormat.Equals(ImageFormat.Png))
                {
                    throw new ApplicationException("Uploaded file is not an accepted image file !");
                }

                //Delete old temp profile pictures
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.TempFileDownloadFolder, "userProfileImage_" + AbpSession.GetUserId());

                //Save new picture
                var fileInfo = new FileInfo(file.FileName);
                var tempFileName = "userProfileImage_" + AbpSession.GetUserId() + fileInfo.Extension;
                var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder, tempFileName);
                file.SaveAs(tempFilePath);

                using (var bmpImage = new Bitmap(tempFilePath))
                {
                    return Json(new MvcAjaxResponse(new { fileName = tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                }
            }
            catch (UserFriendlyException ex)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        private FileResult GetDefaultProfilePicture()
        {
            return File(Server.MapPath("~/Common/Images/default-profile-picture.png"), MimeTypeNames.ImagePng);
        }

        private async Task<FileResult> GetProfilePictureById(Guid profilePictureId)
        {
            var file = await _binaryObjectManager.GetOrNullAsync(profilePictureId);
            if (file == null)
            {
                return GetDefaultProfilePicture();
            }

            return File(file.Bytes, MimeTypeNames.ImageJpeg);
        }
    }
}