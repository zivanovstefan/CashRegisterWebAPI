﻿using System;
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

namespace CashRegisterAPI_Tests.ControllersTests
{
    [TestFixture]
    public class BillContollerTests
    {
        private Mock<IBillService> _billServiceMock;
        private Mock<BillVM> _billVMMock;
        private BillVM validVM;
        private BillVM invalidVM;
        [SetUp]
        public void Setup()
        {
            _billServiceMock = new Mock<IBillService>();
            _billVMMock = new Mock<BillVM>();
            validVM = new BillVM()
            {
                BillNumber = " 200000000007540220",
                PaymentMethod = "MasterCard",
                TotalPrice = 1000,
                CreditCardNumber = "371449635398431"
            };
            invalidVM = new BillVM()
            {
                BillNumber = "4433443",
                PaymentMethod = "MasterCard",
                TotalPrice = 1000,
                CreditCardNumber = "371449635398431"
            };
        }
        [Test]
        public void GetAllBills_Valid_ReturnsAllBills()
        {
            _billServiceMock.Setup(x => x.GetAllBills()).Returns(new List<BillVM>());
            var controller = new BillController(_billServiceMock.Object);
            //Act
            var result = controller.GetAllBills();
            //Assert
            result.GetType().Should().Be(typeof(List<BillVM>));
        }
        [Test]
        public void Create_Valid_ReturnsOk()
        {
            //Arrange
            _billServiceMock.Setup(x => x.Create(It.IsAny<BillVM>()));
            var controller = new BillController(_billServiceMock.Object);
            //Act
            var result = controller.CreateBill(validVM);
            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }
        //ZA PROVERU
        [Test]
        public void CreateBill_Invalid_ReturnsBadRequest()
        {
            //Arrange
            _billServiceMock.Setup(x => x.Create(It.IsAny<BillVM>()));
            var controller = new BillController(_billServiceMock.Object);
            //Act
            var result = controller.CreateBill(invalidVM);
            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }
        [Test]
        public void UpdateBill_Valid_ReturnsOk()
        {
            //Arrange
            _billServiceMock.Setup(x => x.Update(It.IsAny<BillVM>())); // it isAny
            var controller = new BillController(_billServiceMock.Object);
            //Act
            var result = controller.UpdateBill(_billVMMock.Object); // prosledjivanje objekta
            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }
        [Test]
        public void Delete_Valid_ReturnsOk()
        {
            //Arrange
            _billServiceMock.Setup(x => x.Delete(It.IsAny<string>()));
            var controller = new BillController(_billServiceMock.Object);
            //Act
            var result = controller.DeleteBill("200000000007540220");
            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }
        [Test]
        public void GetBillByBillNumber_BillNumberExists_Returns()
        {
            //Arrange
            _billServiceMock.Setup(x => x.GetBillByID(It.IsAny<string>())).Returns(new BillVM());
            var controller = new BillController(_billServiceMock.Object);
            //Act
            var result = controller.GetBillByBillNumber("105008123123123173");
            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }
        [Test]
        public void GetBillByBillNumber_BillNumberDoes_Not_Exists_Returns()
        {
            //Arrange
            _billServiceMock.Setup(x => x.GetBillByID(It.IsAny<string>())).Returns(new BillVM());
            var controller = new BillController(_billServiceMock.Object);
            //Act
            var result = controller.GetBillByBillNumber(null);
            //Assert
            result.GetType().Should().Be(typeof(BadRequestObjectResult));
        }
    }
}