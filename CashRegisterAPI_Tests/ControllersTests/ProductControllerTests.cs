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
        [Test]
        public void GetAllProducts_Valid_ReturnsAllProducts()
        {
            //Arrange
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.GetAllProducts()).Returns(new List<ProductVM>());
            var controller = new ProductController(productService.Object);

            //Act
            var result = controller.GetAllProducts();

            //Assert
            result.GetType().Should().Be(typeof(List<ProductVM>));
        }
        [Test]
        public void Create_Valid_ReturnsOk()
        {
            //Arrange
            var productVM = new Mock<ProductVM>();
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.Create(productVM.Object));
            var controller = new ProductController(productService.Object);
            //Act
            var result = controller.Create(productVM.Object);
            //Assert
            result.GetType().Should().Be(typeof(OkResult));
        }
        [Test]
        public void Update_Valid_ReturnsOk()
        {
            //Arrange
            var productVM = new Mock<ProductVM>();
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.Update(productVM.Object));
            var controller = new ProductController(productService.Object);
            //Act
            var result = controller.UpdateProduct(productVM.Object);
            //Assert
            result.GetType().Should().Be(typeof(OkResult));
        }
        [Test]
        public void Delete_Valid_ReturnsOk()
        {
            //Arrange
            var id = 5;
            var productVM = new Mock<ProductVM>();
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.Delete(id));
            var controller = new ProductController(productService.Object);
            //Act
            var result = controller.DeleteProduct(id);
            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }
    }
}
