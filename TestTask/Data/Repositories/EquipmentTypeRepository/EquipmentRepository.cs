
using TestTask.Data.DbContext;
using TestTask.Data.Models;

namespace TestTask.Data.Repositories.EquipmentTypeRepository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly TestTaskDbContext _context;

        public EquipmentRepository(TestTaskDbContext context)
        {
            _context = context;
        }

        public async Task<EquipmentType> GetByCode(string code)
        {
            var equipmentTypeInDb = await _context.EquipmentTypes.FindAsync(code);

            if (equipmentTypeInDb == null) throw new ArgumentException($"Equipment type with code {code} does not exist");

            return equipmentTypeInDb;
        }
    }
}
