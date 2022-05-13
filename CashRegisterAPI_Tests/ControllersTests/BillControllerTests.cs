using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using FluentAssertions;
using CashRegister.Application.Interfaces;
using CashRegister.API.Controllers;
using CashRegister.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CashRegister.Application.ErrorModels;
using CashRegister.Domain.Common;

namespace CashRegisterAPI_Tests.ControllersTests
{
    [TestFixture]
    public class BillControllerTests
    {
        private Mock<IBillService> _billServiceMock;
        private Mock<BillVM> _billVMMock;
        private BillVM _validVM;
        private BillController _controller;
        [SetUp]
        public void Setup()
        {
            _billServiceMock = new Mock<IBillService>();
            _controller = new BillController(_billServiceMock.Object);
            _billVMMock = new Mock<BillVM>();
            _validVM = new BillVM()
            {
                BillNumber = " 200000000007540220",
                PaymentMethod = "MasterCard",
                TotalPrice = 1000,
                CreditCardNumber = "371449635398431"
            };
        }
        [Test]
        public void GetAllBills_ValidCall_ReturnsAllBills()
        {
            //Arrange
            _billServiceMock.Setup(x => x.GetAllBills()).Returns(new List<BillVM>());
            //Act
            var result = _controller.GetAllBills();
            //Assert
            result.Should().BeOfType<List<BillVM>>();
        }
        [Test]
        public void Create_Valid_ReturnsOk()
        {
            //Arrange
            _billServiceMock.Setup(x => x.Create(It.IsAny<BillVM>()));
            //Act
            var result = _controller.CreateBill(_validVM);
            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Test]
        public void CreateBill_BillVMisNull_ReturnsBadRequest()
        {
            //Arrange
            _billServiceMock.Setup(x => x.Create(It.IsAny<BillVM>()));
            //Act
            var result = _controller.CreateBill(null);
            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }
        [Test]
        public void UpdateBill_Valid_ReturnsOkObjectResult()
        {
            //Arrange
            _billServiceMock.Setup(x => x.Update(It.IsAny<BillVM>())); // it isAny
            //Act
            var result = _controller.UpdateBill(_billVMMock.Object); // prosledjivanje objekta
            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Test]
        public void UpdateBill_BillVMIsNull_ReturnsBadRequest()
        {
            //Arrange
            _billServiceMock.Setup(x => x.Update(It.IsAny<BillVM>())); // it isAny
            //Act
            var result = _controller.UpdateBill(null); // prosledjivanje objekta
            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }
        [Test]
        public void Delete_Valid_ReturnsOkObjectResult()
        {
            //Arrange
            _billServiceMock.Setup(x => x.Delete(It.IsAny<string>()));
            //Act
            var result = _controller.DeleteBill("200000000007540220");
            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Test]
        public void GetBillByBillNumber_BillNumberExists_ReturnsOkObjectResult()
        {
            //Arrange
            _billServiceMock.Setup(x => x.GetBillByID(It.IsAny<string>())).Returns(new BillVM());
            //Act
            var result = _controller.GetBillByBillNumber("105008123123123173");
            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Test]
        public void GetBillByBillNumber_BillNumberIsNull_ReturnsBadRequest()
        {
            //Arrange
            _billServiceMock.Setup(x => x.GetBillByID(It.IsAny<string>())).Returns(new BillVM());
            //Act
            var result = _controller.GetBillByBillNumber(null);
            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Test]
        public void GetBillByBillNumber_BillNumberIsEmptyString_ReturnsBadRequest()
        {
            //Arrange
            _billServiceMock.Setup(x => x.GetBillByID(It.IsAny<string>())).Returns(new BillVM());
            //Act
            var result = _controller.GetBillByBillNumber("");
            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}
