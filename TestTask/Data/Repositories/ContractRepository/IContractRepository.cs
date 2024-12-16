using TestTask.Data.Models;

namespace TestTask.Data.Repositories.ContractRepository
{
    public interface IContractRepository
    {
        public Task AddAsync(string productFacilityCode, string equipmentTypeCode, int amount);
        public Task<List<Contract>> GetAllAsync();
    }
}
