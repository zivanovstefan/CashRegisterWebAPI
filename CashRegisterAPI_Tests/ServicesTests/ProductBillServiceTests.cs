using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using Moq;
using CashRegister.Application.AutoMapper;
using CashRegister.Domain.Models;
using CashRegister.Domain.Interfaces;
using CashRegister.Application.Services;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Core.Bus;
using Microsoft.AspNetCore.Mvc;

namespace CashRegisterAPI_Tests.ServicesTests
{
    [TestFixture]
    public class ProductBillServiceTests
    {
        //Repositories
        private Mock<IProductBillRepository> _productBillRepositoryMock;
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<IBillRepository> _billRepositoryMock;
        //Mapper Configurations
        private MapperConfiguration _mapperConfiguration;
        //Mappers
        private Mock<IMapper> _mapperMock;
        private Mapper _mapper;
        //Entities
        private ProductBillService _productBillService;
        private ProductBill _productBill;
        private ProductBillVM _productBillVM;
        private List<ProductBillVM> _productBillVMs;
        private List<ProductBill> _productBills;
        private Product _product;
        private List<Product> _products;
        private Bill _bill;
        private Bill _billOver5000;
        [SetUp]
        public void Setup()
        {
            //Mapper configurations
            _mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ProductBillDomainToProductBillVMProfile());
                cfg.AddProfile(new ProductBillVMToProductBillDomainProfile());
            });
            //Repositories and mediator
            _productBillRepositoryMock = new Mock<IProductBillRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _billRepositoryMock = new Mock<IBillRepository>();
            //Mappers and service
            _mapperMock = new Mock<IMapper>();
            _mapper = new Mapper(_mapperConfiguration);
            _productBillService = new ProductBillService(_productBillRepositoryMock.Object, _mapper, _billRepositoryMock.Object, _productRepositoryMock.Object);
            //Entities
            _productBill = new ProductBill()
            {
                BillNumber = "200000000007540220",
                ProductId = 1,
                ProductQuantity = 2,
                ProductsPrice = 50
                //BillNumber = "200000000007540220",
                //ProductId = 1
            };
            _productBills = new List<ProductBill>();
            _productBills.Add(_productBill);
            _productBillVM = new ProductBillVM()
            {
                ProductQuantity = 2,
                ProductsPrice = 50,
                BillNumber = "200000000007540220",
                ProductId = 1
            };
            _productBillVMs = new List<ProductBillVM>();
            _productBillVMs.Add(_productBillVM);
            _product = new Product()
            {
                Id = 1,
                Name = "Baloon",
                Price = 50,
                BillProducts = _productBills
            };
            _products = new List<Product>();
            _products.Add(_product);
            _bill = new Bill()
            {
                BillNumber = "200000000007540220",
                PaymentMethod = "MasterCard",
                TotalPrice = 50,
                CreditCardNumber = "5555555555554444",
                BillProducts = _productBills
            };
            _billOver5000 = new Bill()
            {
                BillNumber = "200000000007540220",
                PaymentMethod = "MasterCard",
                TotalPrice = 6000,
                CreditCardNumber = "5555555555554444",
                BillProducts = _productBills
            };
        }
        [Test]
        public void GetAllBillProducts_ValidMethodCall_ReturnAllBillProducts()
        {
            _productBillRepositoryMock.Setup(x => x.GetProductBills()).Returns(_productBills);
            //Act
            var result = _productBillService.GetAllBillProducts();
            //Assert
            result.Value.Should().BeEquivalentTo(_productBillVMs);
        }
        [Test]
        public void GetAllBillProducts_EmptyDB_ReturnAllBillProducts()
        {
            _productBillRepositoryMock.Setup(x => x.GetProductBills()).Returns(new List<ProductBill>());
            //Act
            var result = _productBillService.GetAllBillProducts();
            //Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Test]
        public void AddProductToBill_ProductDoesNotExist_ReturnProductNotFoundError()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.GetAllProducts()).Returns(new List<Product>());
            //Act
            var result = _productBillService.AddProductToBill(_productBillVM);
            //Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }
        [Test]
        public void AddProductToBillProduct_ProductBillExistAndTotalPriceIsLessThan5000_ReturnsTrue()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.GetAllProducts()).Returns(_products);
            _productBillRepositoryMock.Setup(x => x.GetProductBills()).Returns(_productBills);
            _billRepositoryMock.Setup(x => x.GetBillByID(It.IsAny<string>())).Returns(_bill);
            _productBillRepositoryMock.Setup(x => x.Update(It.IsAny<ProductBill>()));
            _billRepositoryMock.Setup(x => x.AddToTotalPrice(It.IsAny<int>(), It.IsAny<string>()));
            //Act
            var result = _productBillService.AddProductToBill(_productBillVM);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void AddProductToBillProduct_BillProductExistButBillsTotalCostGoesOver5000_ReturnsBadRequestObjectResult()
        {
            //Arrange
            _productRepositoryMock.Setup(x => x.GetAllProducts()).Returns(_products);
            //products returned
            _productBillRepositoryMock.Setup(x => x.GetProductBills()).Returns(_productBills);
            //product bills returned
            _billRepositoryMock.Setup(x => x.GetBillByID(It.IsAny<string>())).Returns(_billOver5000);
            //returns bill over 5000
            _productBillRepositoryMock.Setup(x => x.Update(It.IsAny<ProductBill>()));
            _billRepositoryMock.Setup(x => x.AddToTotalPrice(It.IsAny<int>(), It.IsAny<string>()));
            //Act
            var result = _productBillService.AddProductToBill(_productBillVM);
            //Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Test]
        public void Delete_BillProductDoesNotExist_ReturnsNotFoundObjectResult()
        {
            //Arrange
            _productBillRepositoryMock.Setup(x => x.GetProductBills()).Returns(new List<ProductBill>());
            //Act
            var result = _productBillService.DeleteProductsFromBill("105008123123123173", 3, 2);
            //Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }
        [Test]
        public void Delete_ValidBillNumberProductIdAndQuantity_ReturnsTrue()
        {
            //Arrange
            _productBillRepositoryMock.Setup(x => x.GetProductBills()).Returns(_productBills);
            //products returned
            _billRepositoryMock.Setup(x => x.RemoveFromTotalPrice(It.IsAny<int>(), It.IsAny<string>()));
            //remove from total price
            _productBillRepositoryMock.Setup(x => x.Delete(It.IsAny<ProductBill>()));
            //Act
            var result = _productBillService.DeleteProductsFromBill("200000000007540220", 1, 2);
            //Assert
            Assert.IsTrue(result.Value);
        }
        [Test]
        public void Delete_EnteredQuantityIsBiggerThanQuantityInDB_ReturnsNotFoundObjectResult()
        {
            //Arrange
            _productBillRepositoryMock.Setup(x => x.GetProductBills()).Returns(_productBills);
            //Act
            var result = _productBillService.DeleteProductsFromBill("200000000007540220", 1, 5);
            //Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}

