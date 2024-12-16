using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Data.DbContext;
using TestTask.Data.Models;
using TestTask.Data.Repositories.ContractRepository;

namespace TestTask.Tests.Repositories
{
    public class ContractRepositoryTests
    {
        private async Task<TestTaskDbContext> GetDataBaseContext()
        {
            var options = new DbContextOptionsBuilder<TestTaskDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new TestTaskDbContext(options);
            databaseContext.Database.EnsureCreated();

            if(await databaseContext.Products.CountAsync() <= 0)
            {
                databaseContext.Products.Add(
                    new ProductFacility
                    {Code = "p001", Name = "ProductA", Area = 100});

                databaseContext.Products.Add(
                    new ProductFacility
                    { Code = "p002", Name = "ProductB", Area = 50 });

                databaseContext.Products.Add(
                    new ProductFacility
                    { Code = "p003", Name = "ProductC", Area = 75 });

                databaseContext.Products.Add(
                    new ProductFacility
                    { Code = "p004", Name = "ProductD", Area = 25 });
            }

            if (await databaseContext.EquipmentTypes.CountAsync() <= 0)
            {
                databaseContext.EquipmentTypes.Add(
                    new EquipmentType
                    { Code = "e001", Name = "EquipmentA", Area = 10 });

                databaseContext.EquipmentTypes.Add(
                    new EquipmentType
                    { Code = "e002", Name = "EquipmentB", Area = 15 });

                databaseContext.EquipmentTypes.Add(
                    new EquipmentType
                    { Code = "e003", Name = "EquipmentC", Area = 20 });

                databaseContext.EquipmentTypes.Add(
                    new EquipmentType
                    { Code = "e004", Name = "EquipmentD", Area = 40 });
            }

            if (await databaseContext.Contracts.CountAsync() <= 0)
            {
                databaseContext.Contracts.Add(
                    new Contract
                    { Id = 1, ProductFacilityCode = "p001", EquipmentTypeCode = "e003", EquipmentCount = 4 });

                databaseContext.Contracts.Add(
                    new Contract
                    { Id = 2, ProductFacilityCode = "p002", EquipmentTypeCode = "e002", EquipmentCount = 3 });

                databaseContext.Contracts.Add(
                    new Contract
                    { Id = 3, ProductFacilityCode = "p004", EquipmentTypeCode = "e004", EquipmentCount = 1 });

                databaseContext.Contracts.Add(
                    new Contract
                    { Id = 4, ProductFacilityCode = "p001", EquipmentTypeCode = "e004", EquipmentCount = 2 });

            }

            await databaseContext.SaveChangesAsync();

            return databaseContext;
        }

        [Fact]
        public async Task ContractRepository_AddContract_Should_ThrowInvalidOperationException_When_EquipmentArea_Summary_Greater_Than_ProductFacilityArea()
        {
            //Arrange
            string equipmentCode = "e001";
            string productCode = "p001";
            int amount = 11;
            var context = await GetDataBaseContext();
            var repository = new ContractRepository(context);

            //Act
            Func<Task> act = async () => await repository.AddAsync(productCode, equipmentCode,  amount);

            //Assert
            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task ContractRepository_AddAsync_Should_Throw_ArgumentException_When_Amount_Less_Or_Equal_To_Zero()
        {
            //Arrange
            string equipmentCode = "e001";
            string productCode = "p001";
            int amount = 0;
            var context = await GetDataBaseContext();
            var repository = new ContractRepository(context);

            //Act
            Func<Task> act = async () => await repository.AddAsync(productCode, equipmentCode, amount);

            //Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task ContractRepository_AddAsync_Should_Throw_ArgumentException_When_EquipmentTypeCode_Is_Invalid_Or_Does_Not_Exist() 
        {
            //Arrange
            string equipmentCode = "InvalidCode";
            string productCode = "p001";
            int amount = 0;
            var context = await GetDataBaseContext();
            var repository = new ContractRepository(context);

            //Act
            Func<Task> act = async () => await repository.AddAsync(productCode, equipmentCode, amount);

            //Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task ContractRepository_AddAsync_Should_Throw_ArgumentException_When_ProducFacilityCode_Is_Invalid_Or_Does_Not_Exist()
        {
            //Arrange
            string equipmentCode = "e001";
            string productCode = "InvalidCode";
            int amount = 0;
            var context = await GetDataBaseContext();
            var repository = new ContractRepository(context);

            //Act
            Func<Task> act = async () => await repository.AddAsync(productCode, equipmentCode, amount);

            //Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task ContractRepository_GetAllAsync_Should_Return_List_Of_Contracts()
        {
            //Arrange
            var context = await GetDataBaseContext();
            var repository = new ContractRepository(context);
            var contracts = new List<Contract>()
            {
                new Contract
                    { Id = 1, ProductFacilityCode = "p001", EquipmentTypeCode = "e003", EquipmentCount = 4 },
                new Contract
                    { Id = 2, ProductFacilityCode = "p002", EquipmentTypeCode = "e002", EquipmentCount = 3 },
                new Contract
                    { Id = 3, ProductFacilityCode = "p004", EquipmentTypeCode = "e004", EquipmentCount = 1 },
                new Contract
                    { Id = 4, ProductFacilityCode = "p001", EquipmentTypeCode = "e004", EquipmentCount = 2 }
            };

            //Act
            var result = await repository.GetAllAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Contract>>();
            result.Should().BeEquivalentTo(contracts);
        }
    }
}
