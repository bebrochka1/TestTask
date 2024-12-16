using TestTask.Data.Models;

namespace TestTask.Data.Repositories.EquipmentTypeRepository
{
    public interface IEquipmentRepository
    {
        Task<EquipmentType> GetByCode(string code);
    }
}
