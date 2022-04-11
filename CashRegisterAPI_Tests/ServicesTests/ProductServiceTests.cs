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

namespace CashRegisterAPI_Tests.ServicesTests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _repositoryMock;
        private Mock<IMediatorHandler> _busMock;
        private Mock<IMapper> _mapperMock;
        private ProductService _productService;
        private ProductVM productVM;
        private List<Product> products;
        private List<ProductVM> productsVMs;
        private Product product;
        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _busMock = new Mock<IMediatorHandler>();
            _mapperMock = new Mock<IMapper>();
            _productService = new ProductService(_repositoryMock.Object, _busMock.Object, _mapperMock.Object);
            product = new Product()
            {
                Id = 1,
                Name = "Book",
                Price = 50
            };
            products = new List<Product>();
            products.Add(product);
            productVM = new ProductVM()
            {
                Id = 1,
                Name = "Book",
                Price = 50
            };
            productsVMs = new List<ProductVM>();
            productsVMs.Add(productVM);
        }
        [Test]
        public void Create_ValidProductVM_ReturnsTrue()
        {
            _repositoryMock.Setup(x => x.Add(product));
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new ProductDomainToVMProfile()));
            var mapper = new Mapper(configuration);

            var productService = new ProductService(_repositoryMock.Object, _busMock.Object, mapper);
            //Act
            var result = productService.Create(productVM);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void Create_ProductVMIsNull_ReturnsFalse()
        {
            //Act
            var result = _productService.Create(null);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void Update_ValidProductVM_ReturnsTrue()
        {
            //Act
            var result = _productService.Update(productVM);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void Update_ProductVMIsNull_ReturnsFalse()
        {
            //Act
            var result = _productService.Update(null);
            //Assert
            Assert.IsFalse(result.Value);
        }
        [Test]
        public void Delete_ValidId_ReturnsTrue()
        {
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
        public void GetAllProducts_ValidMethodCall_ReturnsAllProducts()
        {
            //Act
            _repositoryMock.Setup(x => x.GetAllProducts()).Returns(new List<Product>().AsQueryable());

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new ProductDomainToVMProfile()));
            var mapper = new Mapper(configuration);

            var service = new ProductService(_repositoryMock.Object, _busMock.Object, mapper);

            var result = service.GetAllProducts().ToList();

            //Assert
            result.Should().BeOfType(typeof(List<ProductVM>));
        }
    }
}
