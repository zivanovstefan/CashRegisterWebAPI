using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using FluentAssertions;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Core.Bus;
using AutoMapper;
using CashRegister.Application.Services;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Models;
using CashRegister.Application.AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CashRegister.Application.ErrorModels;
using CashRegister.Domain.Common;

namespace CashRegisterAPI_Tests.ServicesTests
{
    public class BillServiceTests
    {
        private MapperConfiguration _domainToVMconfiguration;
        private MapperConfiguration _VMToDomainConfiguration;
        private Mock<IBillRepository> _repositoryMock;
        private Mock<IMediatorHandler> _busMock;
        private Mock<IMapper> _mapperMock;
        private Mapper _domainToVMMapper;
        private Mapper _VMToDomainmapper;
        private Mock<IEnumerable<Bill>> _billListMock;
        private BillService _billService;
        private BillService _VMToDomainBillService;
        private Bill _bill;
        private Bill _nullBill;
        private BillVM _billVM;
        private string _billNumber;
        private string _emptyBillNumber;
        private ErrorResponseModel _noBillForEnteredBillNumber;
        [SetUp]
        public void Setup()
        {
            _domainToVMconfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new BillDomainToVMProfile()));
            _VMToDomainConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new BillVMToDomainProfile()));
            _repositoryMock = new Mock<IBillRepository>();
            _busMock = new Mock<IMediatorHandler>();
            _mapperMock = new Mock<IMapper>();
            _domainToVMMapper = new Mapper(_domainToVMconfiguration);
            _VMToDomainmapper = new Mapper(_VMToDomainConfiguration);
            _billListMock = new Mock<IEnumerable<Bill>>();
            _billService = new BillService(_domainToVMMapper, _repositoryMock.Object, _busMock.Object);
            _VMToDomainBillService = new BillService(_VMToDomainmapper, _repositoryMock.Object, _busMock.Object);
            _nullBill = null;
            _bill = new Bill()
            {
                BillNumber = "200000000007540220",
                PaymentMethod = "MasterCard",
                TotalPrice = 1000,
                CreditCardNumber = "371449635398431"
            };
            _billVM = new BillVM()
            {
                BillNumber = "200000000007540220",
                PaymentMethod = "MasterCard",
                TotalPrice = 1000,
                CreditCardNumber = "371449635398431"
            };
            _billNumber = "200000000007540220";
            _emptyBillNumber = "";
            _noBillForEnteredBillNumber = new ErrorResponseModel()
            {
                ErrorMessage = Messages.Bill_Does_Not_Exist,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
        [Test]
        public void GetAllBills_NoBillsInDB_ReturnsAllNotFoundObjectResult()
        {
            //Arrange
            _repositoryMock.Setup(x => x.GetAllBills()).Returns(new List<Bill>().AsQueryable());
            //Act
            var result = _billService.GetAllBills().ToList();
            //Assert
            result.Should().BeOfType(typeof(List<BillVM>));
        }
        [Test]
        public void GetAllBills_ValidMethodCall_ReturnsAllBills()
        {
            //Arrange
            _repositoryMock.Setup(x => x.GetAllBills()).Returns(new List<Bill>().AsQueryable());
            //Act
            var result = _billService.GetAllBills().ToList();
            //Assert
            result.Should().BeOfType(typeof(List<BillVM>));
        }
        [Test]
        public void Create_ValidBillVM_ReturnsTrue()
        {
            //Arrange
            _repositoryMock.Setup(x => x.Add(_bill));
            //Act
            var result = _VMToDomainBillService.Create(_billVM);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void Create_BillVMIsNull_ReturnsFalse()
        {
            //Arrange
            _repositoryMock.Setup(x => x.Add(It.IsAny<Bill>()));
            //Act
            var result = _VMToDomainBillService.Create(null);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void Update_ValidBillVM_ReturnsTrue()
        {
            //Arrange
            _repositoryMock.Setup(x => x.Update(_bill, _billNumber));
            //Act
            var result = _VMToDomainBillService.Update(_billVM);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void Update_BillVMIsNull_ReturnsFalse()
        {
            //Arrange
            _repositoryMock.Setup(x => x.Update(_bill, _billNumber));
            //Act
            var result = _VMToDomainBillService.Update(null);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void Delete_ValidBillNumber_ReturnsTrue()
        {
            //Arrange
            _repositoryMock.Setup(x => x.Delete(_bill));
            //Act
            var result = _VMToDomainBillService.Delete(_billNumber);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void Delete_BillNumberIsEmptyString_ReturnsTrue()
        {
            //Arrange
            _repositoryMock.Setup(x => x.Delete(_bill));
            //Act
            var result = _VMToDomainBillService.Delete(_emptyBillNumber);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void GetBillById_IdIsValid_ReturnsBill()
        {
            //Arrange
            _repositoryMock.Setup(x => x.GetBillByID(_billNumber)).Returns(new Bill());
            //Act
            var result = _VMToDomainBillService.GetBillByID(_billNumber);
            //Assert
            result.Should().BeOfType(typeof(ActionResult<BillVM>));
        }
        [Test]
        public void GetBillById_InvalidId_ReturnsNull()
        {
            //Arrange
            _repositoryMock.Setup(x => x.GetBillByID(It.IsAny<string>())).Returns(new Bill());
            //Act
            var result = _VMToDomainBillService.GetBillByID(null);
            //Assert
            Assert.IsNull(result.Value);
        }
        [Test]
        public void GetBillById_NoBillWithEnteredBillNumber_ReturnsNull()
        {
            //Arrange
            ActionResult<BillVM> expectedResult = new BadRequestObjectResult(_noBillForEnteredBillNumber);
            _repositoryMock.Setup(x => x.GetBillByID(It.IsAny<string>())).Returns(_nullBill);
            //Act
            var actualResult = _VMToDomainBillService.GetBillByID(_billNumber);
            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}
