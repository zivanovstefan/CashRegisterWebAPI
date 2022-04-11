using AutoMapper;
using CashRegister.Application.AutoMapper;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterAPI_Tests.MapperTests
{
    public class DomainToVMTMappingTests
    {
        MapperConfiguration productConfig;
        MapperConfiguration billConfig;
        IMapper productMapper;
        IMapper billMapper;
        Product product;
        Bill bill;
        ProductBill productBill;

        [SetUp]
        public void Setup()
        {
            productConfig = AutoMapperConfiguration.RegisterMappings();
            productMapper = productConfig.CreateMapper();

            billConfig = new MapperConfiguration(cfg => cfg.AddProfile(new BillDomainToVMProfile()));
            billMapper = billConfig.CreateMapper();

            bill = new Bill();
            bill.BillNumber = "105008123123123173";
            bill.TotalPrice = 500;
            bill.CreditCardNumber = "5555555555554444";

            product = new Product();
            product.Id = 1;
            product.Name = "Book";
            product.Price = 20;

            productBill = new ProductBill();
            productBill.ProductId = 1;
            productBill.BillNumber = "105008123123123173";
            productBill.ProductQuantity = 1;
            productBill.ProductsPrice = 30;

        }

        [Test]
        public void CreateMap_BillToBillVM_SuccesfullyMapped()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new BillDomainToVMProfile()));
            var billMapper = configuration.CreateMapper();
            var result = billMapper.Map<BillVM>(bill);
            result.Should().BeOfType<BillVM>();
            result.BillNumber.Should().Be("105008123123123173");
            result.TotalPrice.Should().Be(500);
            result.CreditCardNumber.Should().Be("5555555555554444");
        }
        [Test]
        public void CreateMap_ProductToProductVM_SuccesfullyMapped()
        {
            var result = productMapper.Map<ProductVM>(product);
            result.Should().BeOfType<ProductVM>();
            result.Id.Should().Be(1);
            result.Name.Should().Be("Book");
            result.Price.Should().Be(20);
        }
        [Test]
        public void CreateMap_ProductBillToProductBillVM_SuccesfullyMapped()
        {
            var result = billMapper.Map<ProductBillVM>(productBill);
            result.Should().BeOfType<ProductBillVM>();
            result.ProductId.Should().Be(1);
            result.BillNumber.Should().Be("105008123123123173");
            result.ProductQuantity.Should().Be(1);
            result.ProductsPrice.Should().Be(30);
        }
    }
}
