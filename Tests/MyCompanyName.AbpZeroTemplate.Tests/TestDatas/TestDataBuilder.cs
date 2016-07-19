using EntityFramework.DynamicFilters;
using Tawh.NoTrace.EntityFramework;

namespace Tawh.NoTrace.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly AbpZeroTemplateDbContext _context;

        public TestDataBuilder(AbpZeroTemplateDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new TestOrganizationUnitsBuilder(_context).Create();

            _context.SaveChanges();
        }
    }
}
