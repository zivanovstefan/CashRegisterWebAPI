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
        private ProductBillVM _productBillVM;
        private List<ProductBillVM> _productBillVMs;
        private ProductBillController _controller;
        private string _validBillNumber;
        private string _emptyBillNumber;
        private int _validProductId;
        private int _zeroProductId;
        private int _validQuantity;
        private int _zeroQuantity;
        private int _productsPrice;
        [SetUp]
        public void Setup()
        {
            _productBillServiceMock = new Mock<IProductBillService>();
            _controller = new ProductBillController(_productBillServiceMock.Object);
            _productVMMock = new Mock<ProductBillVM>();
            _validBillNumber = "260005601001611379";
            _emptyBillNumber = "";
            _validProductId = 2;
            _zeroProductId = 0;
            _validQuantity = 2;
            _zeroQuantity = 0;
            _productsPrice = 100;
            _productBillVM = new ProductBillVM()
            {
                BillNumber = _validBillNumber,
                ProductId = _validProductId,
                ProductQuantity = _validQuantity,
                ProductsPrice = _productsPrice
            };
            _productBillVMs = new List<ProductBillVM>();
            _productBillVMs.Add(_productBillVM);
        }
        [Test]
        public void GetAllBillProducts_Valid_ReturnsAllBillProducts()
        {
            //Arrange
            _productBillServiceMock.Setup(x => x.GetAllBillProducts()).Returns(_productBillVMs);
            //Act
            var result = _controller.GetAllBillProducts();
            //Assert
            result.Value.Should().BeEquivalentTo(_productBillVMs);
        }
        [Test]
        public void CreateBill_ValidProductBillVM_ReturnsOkObjectResult()
        {
            //Arrange
            _productBillServiceMock.Setup(x => x.AddProductToBill(It.IsAny<ProductBillVM>())).Returns(true);
            //Act
            var result = _controller.CreateBill(_productBillVM);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void CreateBill_BillVMIsNull_ReturnsBadRequestResult()
        {
            //Arrange
            _productBillServiceMock.Setup(x => x.AddProductToBill(_productVMMock.Object));
            //Act
            var result = _controller.CreateBill(null);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void Delete_ValidId_ReturnsTrue()
        {
            //Arrange
            _productBillServiceMock.Setup(x => x.DeleteProductsFromBill(_validBillNumber, _validProductId, _validQuantity));
            //Act
            var result = _controller.Delete(_validBillNumber, _validProductId, _validQuantity);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void Delete_BillNumberEmptyProductIdisZeroQuantityIsZero_ReturnsFalse()
        {
            //Arrange
            _productBillServiceMock.Setup(x => x.DeleteProductsFromBill(_emptyBillNumber, _zeroProductId, _zeroQuantity));
            //Act
            var result = _controller.Delete(_emptyBillNumber, _zeroProductId, _zeroQuantity);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void Delete_BillNumberIsValidProductIdIsZeroQuantityIsZero_ReturnsFalse()
        {
            //Arrange
            _productBillServiceMock.Setup(x => x.DeleteProductsFromBill(_validBillNumber, _zeroProductId, _zeroQuantity));
            //Act
            var result = _controller.Delete(_validBillNumber, _zeroProductId, _zeroQuantity);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void Delete_BillNumberIsValidProductIdIsValidQuantityIsZero_ReturnsFalse()
        {
            //Arrange
            _productBillServiceMock.Setup(x => x.DeleteProductsFromBill(_validBillNumber, _validProductId, _zeroQuantity));
            //Act
            var result = _controller.Delete(_validBillNumber, _validProductId, _zeroQuantity);
            //Assert
            Assert.IsFalse(result.Value);
        }
    }
}
