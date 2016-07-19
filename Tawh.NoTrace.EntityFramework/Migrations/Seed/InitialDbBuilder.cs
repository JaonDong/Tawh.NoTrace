using EntityFramework.DynamicFilters;
using Tawh.NoTrace.EntityFramework;

namespace Tawh.NoTrace.Migrations.Seed
{
    public class InitialDbBuilder
    {
        private readonly AbpZeroTemplateDbContext _context;

        public InitialDbBuilder(AbpZeroTemplateDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new DefaultTenantRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
