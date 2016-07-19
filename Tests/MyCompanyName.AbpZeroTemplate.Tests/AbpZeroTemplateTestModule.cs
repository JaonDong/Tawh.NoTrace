using Abp.Modules;
using Abp.Zero.Configuration;

namespace Tawh.NoTrace.Tests
{
    [DependsOn(
        typeof(AbpZeroTemplateApplicationModule),
        typeof(AbpZeroTemplateDataModule))]
    public class AbpZeroTemplateTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Use database as language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();
        }
    }
}
