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
        [SetUp]
        public void Setup()
        {
            _productServiceMock = new Mock<IProductService>();
            _productVMMock = new Mock<ProductVM>();
        }
        [Test]
        public void GetAllProducts_Valid_ReturnsAllProducts()
        {
            //Arrange
            _productServiceMock.Setup(x => x.GetAllProducts()).Returns(new List<ProductVM>());
            var controller = new ProductController(_productServiceMock.Object);
            //Act
            var result = controller.GetAllProducts();
            //Assert
            result.GetType().Should().Be(typeof(List<ProductVM>));        
        }
        [Test]
        public void Create_Valid_ReturnsOk()
        {
            //Arrange
            _productServiceMock.Setup(x => x.Create(_productVMMock.Object));
            var controller = new ProductController(_productServiceMock.Object);
            //Act
            var result = controller.CreateProduct(_productVMMock.Object);
            //Assert
            result.GetType().Should().Be(typeof(OkResult));
        }
        [Test]
        public void Update_Valid_ReturnsOk()
        {
            //Arrange
            _productServiceMock.Setup(x => x.Update(_productVMMock.Object)); // it isAny
            var controller = new ProductController(_productServiceMock.Object);
            //Act
            var result = controller.UpdateProduct(_productVMMock.Object); // prosledjivanje objekta
            //Assert
            result.GetType().Should().Be(typeof(OkResult));
        }
        [Test]
        public void Delete_Valid_ReturnsOk()
        {
            //Arrange
            _productServiceMock.Setup(x => x.Delete(It.IsAny<int>()));
            var controller = new ProductController(_productServiceMock.Object);
            //Act
            var result = controller.DeleteProduct(5);
            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }
    }
}
