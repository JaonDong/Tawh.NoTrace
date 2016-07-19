using Abp.WebApi.Controllers;

namespace Tawh.NoTrace.WebApi
{
    public abstract class AbpZeroTemplateApiControllerBase : AbpApiController
    {
        protected AbpZeroTemplateApiControllerBase()
        {
            LocalizationSourceName = AbpZeroTemplateConsts.LocalizationSourceName;
        }
    }
}