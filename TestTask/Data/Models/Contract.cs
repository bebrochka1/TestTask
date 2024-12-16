namespace TestTask.Data.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string ProductFacilityCode { get; set; } = string.Empty;
        public ProductFacility? Product {  get; set; }
        public string EquipmentTypeCode { get; set; } = string.Empty;
        public EquipmentType? Equipment { get; set; }
        public int EquipmentCount { get; set; }
    }

    public record class ContractDTO(string productName, string equipmentName, int amount);
}
