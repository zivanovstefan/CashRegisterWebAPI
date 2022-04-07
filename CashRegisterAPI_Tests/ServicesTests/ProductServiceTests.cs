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

namespace CashRegisterAPI_Tests.ServicesTests
{
    [TestFixture]
    public class ProductServiceTests
    {
        //[Test]
        //public void Create_ValidProductVM_ReturnsSuccess()
        //{
        //    var id = 5;
        //    var name = "Box";
        //    var price = 100;
        //    var product = new Product()
        //    {
        //        Id = id,
        //        Name = name,
        //        Price = price
        //    };
        //    var mockMediator = new Mock<IMediatorHandler>();
        //    mockMediator.Setup(x => x.SendCommand());
        //    var mockRepo = new Mock<IProductRepository>();
        //    mockRepo.Setup(x => x.Add(product));

        //    var service = new ProductService();
        //}
    }
}
