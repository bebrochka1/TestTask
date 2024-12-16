namespace TestTask.Data.Models
{
    public class EquipmentType
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Area { get; set; }
        public ICollection<Contract> Contracts { get; set; } = [];
    }
}
