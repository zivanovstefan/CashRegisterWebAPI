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

namespace CashRegisterAPI_Tests.ServicesTests
{
    [TestFixture]
    public class ProductBillServiceTests
    {
        private MapperConfiguration _domainToVMconfiguration;
        private MapperConfiguration _VMToDomainConfiguration;
        private Mock<IProductBillRepository> _productBillRepositoryMock;
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<IBillRepository> _billRepositoryMock;
        private Mock<IMediatorHandler> _busMock;
        private Mock<IMapper> _mapperMock;
        private Mapper _domainToVMMapper;
        private Mapper _VMToDomainMapper;
        private ProductBillService _productBillService;
        private ProductBill _productBill;
        private ProductBillVM _productBillVM;
        private List<ProductBill> _productBills;
        [SetUp]
        public void Setup()
        {
            _domainToVMconfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new ProductBillDomainToProductBillVMProfile()));
            _VMToDomainConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new ProductBillVMToProductBillDomainProfile()));
            _productBillRepositoryMock = new Mock<IProductBillRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _billRepositoryMock = new Mock<IBillRepository>();
            _busMock = new Mock<IMediatorHandler>();
            _mapperMock = new Mock<IMapper>();
            _domainToVMMapper = new Mapper(_domainToVMconfiguration);
            _VMToDomainMapper = new Mapper(_VMToDomainConfiguration);
            _productBillService = new ProductBillService(_productBillRepositoryMock.Object, _mapperMock.Object, _billRepositoryMock.Object, _productRepositoryMock.Object, _busMock.Object);
            _productBill = new ProductBill()
            {
                ProductQuantity = 2,
                ProductsPrice = 50,
                BillNumber = "200000000007540220",
                ProductId = 1
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
        }
        [Test]
        public void GetAllBillProducts_ValidMethodCall_ReturnAllBillProducts()
        {
            _productBillRepositoryMock.Setup(x => x.GetProductBills()).Returns((_productBills));
            //Act
            var result = _productBillService.GetAllBillProducts();
            //Assert
            result.Should().BeOfType(typeof(List<ProductBillVM>));
        }
    }
}

