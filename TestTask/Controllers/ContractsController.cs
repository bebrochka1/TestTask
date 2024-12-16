using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TestTask.Data.Models;
using TestTask.Data.Repositories.ContractRepository;
using TestTask.Data.Repositories.EquipmentTypeRepository;
using TestTask.Data.Repositories.ProductFacilityRepository;

namespace TestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractRepository _contractRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IProductFacilityRepository _productFacilityRepository;

        public ContractsController(
            IContractRepository contractRepository,
            IProductFacilityRepository productFacilityRepository,
            IEquipmentRepository equipmentRepository)
        {
            _contractRepository = contractRepository;
            _productFacilityRepository = productFacilityRepository;
            _equipmentRepository = equipmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetContracts()
        {
            var contractsDtos = new List<ContractDTO>();

            var contractsInDb = await _contractRepository.GetAllAsync();

            if(contractsInDb.Count == 0)
            {
                return Ok(new List<ContractDTO>());
            }

            EquipmentType? equipmentInDb = null;
            ProductFacility? productFacilityInDb = null;

            try
            {
                foreach (var contract in contractsInDb)
                {
                    equipmentInDb = await _equipmentRepository.GetByCode(contract.EquipmentTypeCode);
                    productFacilityInDb = await _productFacilityRepository.GetByCode(contract.ProductFacilityCode);

                    int amount = contract.EquipmentCount;

                    contractsDtos.Add(new ContractDTO(
                        productName: productFacilityInDb.Name,
                        equipmentName: equipmentInDb.Name,
                        amount: amount
                        ));
                }
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(contractsDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddContract(string productFacilityCode, string equipmentTypeCode, int amount)
        {
            try
            {
                await _contractRepository.AddAsync(productFacilityCode, equipmentTypeCode, amount);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Created();
        }
    }
}
