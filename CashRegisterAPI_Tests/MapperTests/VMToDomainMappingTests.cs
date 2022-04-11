using AutoMapper;
using CashRegister.Application.AutoMapper;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Commands;
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
    public class VMToDomainMappingTests
    {
        MapperConfiguration productConfig;
        MapperConfiguration billConfig;
        MapperConfiguration productBillConfig;
        IMapper productMapper;
        IMapper billMapper;
        IMapper productBillMapper;
        ProductVM product;
        BillVM bill;
        ProductBillVM productBill;

        [SetUp]
        public void Setup()
        {
            productConfig = AutoMapperConfiguration.RegisterMappings();
            productMapper = productConfig.CreateMapper();

            billConfig = new MapperConfiguration(cfg => cfg.AddProfile(new BillVMToDomainProfile()));
            billMapper = billConfig.CreateMapper();

            productBillConfig = new MapperConfiguration(cfg => cfg.AddProfile(new ProductBillVMToProductBillDomainProfile()));
            productBillMapper = productBillConfig.CreateMapper();

            bill = new BillVM();
            bill.BillNumber = "105008123123123173";
            bill.TotalPrice = 500;
            bill.CreditCardNumber = "5555555555554444";

            product = new ProductVM();
            product.Id = 1;
            product.Name = "Book";
            product.Price = 20;

            productBill = new ProductBillVM();
            productBill.ProductId = 1;
            productBill.BillNumber = "105008123123123173";
            productBill.ProductsPrice = 30;
            productBill.ProductQuantity = 1;

        }

        [Test]
        public void CreateMap_BillToBillVM_SuccesfullyMapped()
        {
            var result = billMapper.Map<CreateBillCommand>(bill);
            result.Should().BeOfType<CreateBillCommand>();
            result.BillNumber.Should().Be("105008123123123173");
            result.TotalPrice.Should().Be(500);
            result.CreditCardNumber.Should().Be("5555555555554444");
        }
        [Test]
        public void CreateMap_ProductToProductVM_SuccesfullyMapped()
        {
            var result = productMapper.Map<CreateProductCommand>(product);
            result.Should().BeOfType<CreateProductCommand>();
            result.Id.Should().Be(1);
            result.Name.Should().Be("Book");
            result.Price.Should().Be(20);
        }
        [Test]
        public void CreateMap_ProductBillToProductBillVM_SuccesfullyMapped()
        {
            var result = productBillMapper.Map<AddProductsCommand>(productBill);
            result.Should().BeOfType<AddProductsCommand>();
            result.ProductId.Should().Be(1);
            result.BillNumber.Should().Be("105008123123123173");
            result.ProductsPrice.Should().Be(30);
            result.ProductQuantity.Should().Be(1);
        }
    }
}
