using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using CashRegister.Application.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Application.ViewModels;
using CashRegister.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace CashRegisterAPI_Tests.ControllersTests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductService> _productServiceMock;
        private Mock<ProductVM> _productVMMock;
        private ProductController _controller;
        private ProductVM _product;
        private List<ProductVM> _products;
        [SetUp]
        public void Setup()
        {
            _productServiceMock = new Mock<IProductService>();
            _controller = new ProductController(_productServiceMock.Object);
            _productVMMock = new Mock<ProductVM>();
            _product = new ProductVM
            {
                Id = 1,
                Name = "Pencil",
                Price = 50
            };
            _products = new List<ProductVM>();
            _products.Add(_product);    
        }
        [Test]
        public void GetAllProducts_ListWithOneProduct_ReturnsAllProducts()
        {
            //Arrange
            _productServiceMock.Setup(x => x.GetAllProducts()).Returns(_products);
            var expectedCount = 1;
            //Act
            var result = _controller.GetAllProducts();
            //Assert
            result.Value.GetType().Should().Be(typeof(List<ProductVM>));  
            result.Value.Count.Should().Be(expectedCount);
        }
        [Test]
        public void GetAllProducts_EmptyList_ReturnsAllProducts()
        {
            //Arrange
            _productServiceMock.Setup(x => x.GetAllProducts()).Returns(new List<ProductVM>());
            var expectedCount = 0;  
            //Act
            var result = _controller.GetAllProducts();
            //Assert
            result.Value.GetType().Should().Be(typeof(List<ProductVM>));
            result.Value.Count.Should().Be(expectedCount);
        }
        [Test]
        public void Create_Valid_ReturnsOk()
        {
            //Arrange
            _productServiceMock.Setup(x => x.Create(_productVMMock.Object));
            //Act
            var result = _controller.CreateProduct(_productVMMock.Object);
            //Assert
            result.GetType().Should().Be(typeof(OkResult));
        }
        [Test]
        public void Create_ProductVMIsNull_ReturnsBadRequest()
        {
            //Arrange
            _productServiceMock.Setup(x => x.Create(_productVMMock.Object));
            //Act
            var result = _controller.CreateProduct(null);
            //Assert
            result.GetType().Should().Be(typeof(BadRequestResult));
        }
        [Test]
        public void Update_Valid_ReturnsOk()
        {
            //Arrange
            _productServiceMock.Setup(x => x.Update(_productVMMock.Object));
            //Act
            var result = _controller.UpdateProduct(_productVMMock.Object);
            //Assert
            result.GetType().Should().Be(typeof(OkResult));
        }
        [Test]
        public void Update_ProductVMIsNull_ReturnsBadRequest()
        {
            //Arrange
            _productServiceMock.Setup(x => x.Update(_productVMMock.Object));
            //Act
            var result = _controller.UpdateProduct(null);
            //Assert
            result.GetType().Should().Be(typeof(BadRequestResult));
        }
        [Test]
        public void Delete_ValidProductId_ReturnsTrue()
        {
            //Arrange
            _productServiceMock.Setup(x => x.Delete(It.IsAny<int>())).Returns(true);
            //Act
            var result = _controller.DeleteProduct(1);
            //Assert
            result.Value.Should().BeTrue();
        }
        [Test]
        public void Delete_IdIsZero_ReturnsFalse()
        {
            //Arrange
            _productServiceMock.Setup(x => x.Delete(It.IsAny<int>())).Returns(false);
            //Act
            var result = _controller.DeleteProduct(0);
            //Assert
            result.Value.Should().BeFalse();
        }
    }
}
