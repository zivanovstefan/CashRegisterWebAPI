using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Core.Bus;
using AutoMapper;
using CashRegister.Application.Services;
using Moq;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Models;
using CashRegister.Application.AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace CashRegisterAPI_Tests.ServicesTests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private MapperConfiguration _domainToVMconfiguration;
        private MapperConfiguration _VMToDomainConfiguration;
        private Mock<IProductRepository> _repositoryMock;
        private Mock<IMediatorHandler> _busMock;
        private Mock<IMapper> _mapperMock;
        private Mapper _domainToVMMapper;
        private Mapper _VMToDomainMapper;
        private ProductService _productService;
        private ProductVM _productVM;
        private List<Product> _products;
        private List<ProductVM> _productsVMs;
        private Product _product;
        private int _productId;
        [SetUp]
        public void Setup()
        {
            _domainToVMconfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new ProductDomainToVMProfile()));
            _VMToDomainConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new ProductVMToDomainProfile()));
            _repositoryMock = new Mock<IProductRepository>();
            _busMock = new Mock<IMediatorHandler>();
            _mapperMock = new Mock<IMapper>();
            _domainToVMMapper = new Mapper(_domainToVMconfiguration);
            _VMToDomainMapper = new Mapper(_VMToDomainConfiguration);
            _productService = new ProductService(_repositoryMock.Object, _busMock.Object, _domainToVMMapper);
            _productId = 1;
            _product = new Product()
            {
                Id = 1,
                Name = "Book",
                Price = 50
            };
            _products = new List<Product>();
            _products.Add(_product);
            _productVM = new ProductVM()
            {
                Id = 1,
                Name = "Book",
                Price = 50
            };
            _productsVMs = new List<ProductVM>();
            _productsVMs.Add(_productVM);
        }
        [Test]
        public void Create_ValidProductVM_ReturnsTrue()
        {
            _repositoryMock.Setup(x => x.Add(_product));
            //Act
            var result = _productService.Create(_productVM);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void Create_ProductVMIsNull_ReturnsFalse()
        {
            _repositoryMock.Setup(x => x.Add(_product));
            //Act
            var result = _productService.Create(null);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void Update_ValidProductVM_ReturnsTrue()
        {
            _repositoryMock.Setup(x => x.Update(_product, _productId));
            var productService = new ProductService(_repositoryMock.Object, _busMock.Object, _VMToDomainMapper);
            //Act
            var result = productService.Update(_productVM);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void Update_ProductVMIsNull_ReturnsFalse()
        {
            _repositoryMock.Setup(x => x.Update(_product, _productId));
            var productService = new ProductService(_repositoryMock.Object, _busMock.Object, _VMToDomainMapper);
            //Act
            var result = _productService.Update(null);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void Delete_ValidId_ReturnsTrue()
        {
            //Arrange
            _repositoryMock.Setup(repo => repo.GetAllProducts()).Returns(_products.AsQueryable);
            _repositoryMock.Setup(repo => repo.Delete(It.IsAny<Product>()));
            //Act
            var result = _productService.Delete(1);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void Delete_IdIsZero_ReturnsFalse()
        {
            //Act
            var result = _productService.Delete(0);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void Delete_IfProductDoesNotExist_ReturnsNotFoundObjectResult()
        {
            //Arrange
            _repositoryMock.Setup(repo => repo.GetAllProducts()).Returns(new List<Product>().AsQueryable);
            _repositoryMock.Setup(repo => repo.Delete(It.IsAny<Product>()));
            //Act
            var result = _productService.Delete(1);
            //Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }
        [Test]
        public void GetAllProducts_ValidMethodCall_ReturnsAllProducts()
        {
            //Arrange
            _repositoryMock.Setup(x => x.GetAllProducts()).Returns(new List<Product>());
            var service = new ProductService(_repositoryMock.Object, _busMock.Object, _domainToVMMapper);
            //Act
            var result = service.GetAllProducts();
            //Assert
            result.Value.Should().BeOfType(typeof(List<ProductVM>));
        }
    }
}
