using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Controllers;
using TestTask.Data.Models;
using TestTask.Data.Repositories.ContractRepository;
using TestTask.Data.Repositories.EquipmentTypeRepository;
using TestTask.Data.Repositories.ProductFacilityRepository;

namespace TestTask.Tests.Controllers
{
    public class ContractsControllerTests
    {
        private readonly IContractRepository _contractRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IProductFacilityRepository _productFacilityRepository;

        public ContractsControllerTests()
        {
            _contractRepository = A.Fake<IContractRepository>();
            _equipmentRepository = A.Fake<IEquipmentRepository>();
            _productFacilityRepository = A.Fake<IProductFacilityRepository>();
        }

        //GetContracts

        [Fact]
        public async Task ContractsController_GetContracts_Returns_Ok()
        {
            //Arrange
            var controller = new ContractsController(
                _contractRepository,
                _productFacilityRepository,
                _equipmentRepository);

            //Act
            var result = await controller.GetContracts();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task ContractsController_GetContracts_Returns_ContractsDtos()
        {
            //Arrange
            var controller = new ContractsController(
                _contractRepository,
                _productFacilityRepository,
                _equipmentRepository);

            //Act
            var result = await controller.GetContracts() as OkObjectResult;
            var contracts = result?.Value;

            //Assert
            controller.Should().NotBeNull();
            contracts.Should().BeOfType<List<ContractDTO>>();
        }

        //AddContract

        [Fact]
        public async Task ContractsController_AddContract_Returns_BadRequest_When_EquipmentArea_Greater_Than_Product_Facility()
        {
            //Arrange
            var controller = new ContractsController(
                _contractRepository,
                _productFacilityRepository,
                _equipmentRepository);
            string fakeProductCode = "p000";
            string fakeEquipmentCode = "e000";

            int amount = 10;

            A.CallTo(() => _contractRepository.AddAsync(fakeProductCode, fakeEquipmentCode, amount))
                .Throws(new InvalidOperationException());     

            //Act
            var result = await controller.AddContract(fakeProductCode, fakeEquipmentCode, amount);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
