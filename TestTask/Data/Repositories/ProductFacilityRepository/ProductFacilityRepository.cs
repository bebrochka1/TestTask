using TestTask.Data.DbContext;
using TestTask.Data.Models;
using TestTask.Data.Repositories.ProductFacilityRepository;

namespace TestTask.Data.Repositories.ProductFacilityRepository
{
    public class ProductFacilityRepository : IProductFacilityRepository
    {
        private readonly TestTaskDbContext _context;

        public ProductFacilityRepository(TestTaskDbContext context)
        {
            _context = context;
        }

        public async Task<ProductFacility> GetByCode(string code)
        {
            var productInDb = await _context.Products.FindAsync(code);

            if (productInDb == null) throw new ArgumentException($"Product facility with code {code} does not exist");

            return productInDb;
        }
    }
}
