using TestTask.Data.Models;
using TestTask.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Data.Repositories.ContractRepository
{
    public class ContractRepository : IContractRepository
    {
        private readonly TestTaskDbContext _context;

        public ContractRepository(TestTaskDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(string productFacilityCode, string equipmentTypeCode, int amount)
        {
            if (string.IsNullOrWhiteSpace(productFacilityCode)) throw new ArgumentNullException("Product facility code is null or empty");
            if (string.IsNullOrWhiteSpace(equipmentTypeCode)) throw new ArgumentNullException("Equipment type code is null or empty");

            var productFacilityInDb = await _context.Products.FirstOrDefaultAsync(p => p.Code == productFacilityCode);

            if (productFacilityInDb == null) throw new ArgumentException($"Product facility with code {productFacilityCode} does not exist");

            var equipmentTypeInDb = await _context.EquipmentTypes.FirstOrDefaultAsync(e => e.Code == equipmentTypeCode);

            if (equipmentTypeInDb == null) throw new ArgumentException($"Equipment type with code {equipmentTypeCode} does not exist");

            if (amount <= 0) throw new ArgumentException("Amount should be greater than zero");

            double equipmentTypeArea = equipmentTypeInDb.Area * amount;

            if (equipmentTypeArea > productFacilityInDb.Area) throw new InvalidOperationException("Equipment type area summary should be less than product facility area");

            var contract = new Contract
            {
                ProductFacilityCode = productFacilityCode,
                EquipmentTypeCode = equipmentTypeCode,
                EquipmentCount = amount,
            };

            await _context.Contracts.AddAsync(contract);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Contract>> GetAllAsync() => await _context.Contracts.ToListAsync();
    }
}
