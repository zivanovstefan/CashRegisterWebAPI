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
using CashRegister.Domain.Commands;

namespace CashRegisterAPI_Tests.ServicesTests
{
    public class BillServiceTests
    {
        private MapperConfiguration _mapperConfiguration;
        private Mock<IBillRepository> _repositoryMock;
        private Mock<IMediatorHandler> _busMock;
        private Mapper _mapper;
        private Mock<IEnumerable<Bill>> _billListMock;
        private BillService _billService;
        private Bill _bill;
        private Bill _nullBill;
        private BillVM _billVM;
        private string _billNumber;
        private string _emptyBillNumber;
        private ErrorResponseModel _noBillForEnteredBillNumber;
        [SetUp]
        public void Setup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BillVMToDomainProfile());
                cfg.AddProfile(new BillDomainToVMProfile());
            });
            _repositoryMock = new Mock<IBillRepository>();
            _busMock = new Mock<IMediatorHandler>();
            _mapper = new Mapper(_mapperConfiguration);
            _billListMock = new Mock<IEnumerable<Bill>>();
            _billService = new BillService(_mapper, _repositoryMock.Object, _busMock.Object);
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
        public void GetAllBills_NoBillsInDB_ReturnsNotFoundObjectResult()
        {
            //Arrange
            _repositoryMock.Setup(x => x.GetAllBills()).Returns(new List<Bill>().AsQueryable());
            //Act
            var result = _billService.GetAllBills().ToList();
            //Assert
            result.Should().BeOfType<List<BillVM>>();
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
            _busMock.Setup(x => x.SendCommand(It.IsAny<CreateBillCommand>())).Returns(Task.FromResult(true));
            //Act
            var result = _billService.Create(_billVM);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void Create_BillWithEnteredBillNumberDontExist_ReturnsFalse()
        {
            //Arrange
            _busMock.Setup(x => x.SendCommand(It.IsAny<CreateBillCommand>())).Returns(Task.FromResult(false));
            //Act
            var result = _billService.Create(_billVM);
            //Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Test]
        public void Create_BillVMIsNull_ReturnsFalse()
        {
            //Arrange
            _busMock.Setup(x => x.SendCommand(It.IsAny<CreateBillCommand>())).Returns(Task.FromResult(false));
            //Act
            var result = _billService.Create(null);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void Update_ValidBillVM_ReturnsTrue()
        {
            //Arrange
            _busMock.Setup(x => x.SendCommand(It.IsAny<UpdateBillCommand>())).Returns(Task.FromResult(true));
            //Act
            var result = _billService.Update(_billVM);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void Update_BillWithEnteredBillNumberDontExist_ReturnsBadRequest()
        {
            //Arrange
            _busMock.Setup(x => x.SendCommand(It.IsAny<UpdateBillCommand>())).Returns(Task.FromResult(false));
            //Act
            var result = _billService.Update(_billVM);
            //Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Test]
        public void Update_BillVMIsNull_ReturnsFalse()
        {
            //Arrange
            _busMock.Setup(x => x.SendCommand(It.IsAny<UpdateBillCommand>())).Returns(Task.FromResult(false));
            //Act
            var result = _billService.Update(null);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void Delete_ValidBillNumber_ReturnsTrue()
        {
            //Arrange
            _repositoryMock.Setup(x => x.Delete(_bill));
            //Act
            var result = _billService.Delete(_billNumber);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void Delete_BillNumberIsEmptyString_ReturnsTrue()
        {
            //Arrange
            _repositoryMock.Setup(x => x.Delete(_bill));
            //Act
            var result = _billService.Delete(_emptyBillNumber);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void GetBillById_IdIsValid_ReturnsBill()
        {
            //Arrange
            _repositoryMock.Setup(x => x.GetBillByID(_billNumber)).Returns(_bill);
            //Act
            var result = _billService.GetBillByID(_billNumber);
            //Assert
            result.Should().BeOfType<ActionResult<BillVM>>();
        }
        [Test]
        public void GetBillById_InvalidId_ReturnsNull()
        {
            //Arrange
            _repositoryMock.Setup(x => x.GetBillByID(It.IsAny<string>())).Returns(new Bill());
            //Act
            var result = _billService.GetBillByID(null);
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
            var actualResult = _billService.GetBillByID(_billNumber);
            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}
