using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using FluentAssertions;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Core.Bus;
using AutoMapper;
using CashRegister.Application.Services;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Models;
using CashRegister.Application.AutoMapper;

namespace CashRegisterAPI_Tests.ServicesTests
{
    public class BillServiceTests
    {
        private Mock<IBillRepository> _repositoryMock;
        private Mock<IMediatorHandler> _busMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IEnumerable<Bill>> _billListMock;
        private BillService _billService;
        private Bill _bill;
        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IBillRepository>();
            _busMock = new Mock<IMediatorHandler>();
            _mapperMock = new Mock<IMapper>();
            _billListMock = new Mock<IEnumerable<Bill>>();
            _billService = new BillService(_mapperMock.Object, _repositoryMock.Object, _busMock.Object);
            _bill = new Bill()
            {
                BillNumber = " 200000000007540220",
                PaymentMethod = "MasterCard",
                TotalPrice = 1000,
                CreditCardNumber = "371449635398431"
            };
        }
        [Test]
        public void GetAllBills_ValidMethodCall_ReturnsAllProducts()
        {
            //Act
            _repositoryMock.Setup(x => x.GetAllBills()).Returns(new List<Bill>().AsQueryable());

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new BillDomainToVMProfile()));
            var mapper = new Mapper(configuration);

            var service = new BillService(mapper, _repositoryMock.Object, _busMock.Object);

            var result = service.GetAllBills().ToList();

            //Assert
            result.Should().BeOfType(typeof(List<BillVM>));
        }
    }
}
