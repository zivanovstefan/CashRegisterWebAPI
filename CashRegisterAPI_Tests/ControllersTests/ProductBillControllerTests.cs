using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using Moq;
using CashRegister.Application.Interfaces;
using CashRegister.Application.ViewModels;
using CashRegister.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CashRegisterAPI_Tests.ControllersTests
{
    [TestFixture]
    public class ProductBillControllerTests
    {
        private Mock<IProductBillService> _productBillServiceMock;
        private Mock<ProductBillVM> _productVMMock;
        private ProductBillController _controller;
        [SetUp]
        public void Setup()
        {
            _productBillServiceMock = new Mock<IProductBillService>();
            _controller = new ProductBillController(_productBillServiceMock.Object);
            _productVMMock = new Mock<ProductBillVM>();
        }
        [Test]
        public void GetAllBillProducts_Valid_ReturnsAllBillProducts()
        {
            //Arrange
            _productBillServiceMock.Setup(x => x.GetAllBillProducts()).Returns(new List<ProductBillVM>());
            //Act
            var result = _controller.GetAllBillProducts();
            //Assert
            result.GetType().Should().Be(typeof(List<ProductBillVM>));
        }
        [Test]
        public void CreateBill_ValidProductBillVM_ReturnsOkObjectResult()
        {
            //Arrange
            _productBillServiceMock.Setup(x => x.AddProductToBill(_productVMMock.Object));
            //Act
            var result = _controller.CreateBill(_productVMMock.Object);
            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }
        [Test]
        public void CreateBill_BillVMIsNull_ReturnsBadRequestResult()
        {
            //Arrange
            _productBillServiceMock.Setup(x => x.AddProductToBill(_productVMMock.Object));
            //Act
            var result = _controller.CreateBill(null);
            //Assert
            result.GetType().Should().Be(typeof(BadRequestResult));
        }
        [Test]
        public void Delete_ValidId_ReturnsTrue()
        {
            var billNumber = "260005601001611379";
            var productId = 1;
            var quantity = 2;
            //Arrange
            _productBillServiceMock.Setup(x => x.DeleteProductsFromBill(billNumber, productId, quantity));
            //Act
            var result = _controller.Delete(billNumber, productId, quantity);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void Delete_BillNumberEmptyProductIdisZeroQuantityIsZero_ReturnsFalse()
        {
            var billNumber = "";
            var productId = 0;
            var quantity = 0;
            //Arrange
            _productBillServiceMock.Setup(x => x.DeleteProductsFromBill(billNumber, productId, quantity));
            //Act
            var result = _controller.Delete(billNumber, productId, quantity);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void Delete_BillNumberIsValidProductIdIsZeroQuantityIsZero_ReturnsFalse()
        {
            var billNumber = "260005601001611379";
            var productId = 0;
            var quantity = 0;
            //Arrange
            _productBillServiceMock.Setup(x => x.DeleteProductsFromBill(billNumber, productId, quantity));
            //Act
            var result = _controller.Delete(billNumber, productId, quantity);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void Delete_BillNumberIsValidProductIdIsValidQuantityIsZero_ReturnsFalse()
        {
            var billNumber = "260005601001611379";
            var productId = 2;
            var quantity = 0;
            //Arrange
            _productBillServiceMock.Setup(x => x.DeleteProductsFromBill(billNumber, productId, quantity));
            //Act
            var result = _controller.Delete(billNumber, productId, quantity);
            //Assert
            Assert.IsFalse(result.Value);
        }
    }
}
