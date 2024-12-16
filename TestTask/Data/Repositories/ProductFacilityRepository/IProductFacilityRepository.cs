using TestTask.Data.Models;

namespace TestTask.Data.Repositories.ProductFacilityRepository
{
    public interface IProductFacilityRepository
    {
        Task<ProductFacility> GetByCode(string code);
    }
}
