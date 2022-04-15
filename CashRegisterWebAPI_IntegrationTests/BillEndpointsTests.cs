using CashRegister.Application.Interfaces;
using NSubstitute;
using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Domain.Models;
using CashRegister.Application.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Net;
using CashRegister.Domain.Interfaces;
using CashRegister.Application.Services;
using AutoMapper;
using CashRegister.Domain.Core.Bus;
using Moq;
using CashRegister.Application.AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace CashRegisterWebAPI_IntegrationTests
{
    public class BillEndpointsTests
    {
        private Mock<IBillRepository> _billRepository;
        private IBillService _billService;
        private IMapper _mapper;
        private IMediatorHandler _mediator =
    Substitute.For<IMediatorHandler>();
        [SetUp]
        public void Setup()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BillVMToDomainProfile());
                cfg.AddProfile(new BillDomainToVMProfile());
            });
            _mapper = new Mapper(mapperConfiguration);

            _billRepository = new Mock<IBillRepository>();
            _billService = new BillService(_mapper, _billRepository.Object, _mediator);
        }
        [Test]
        public async Task GetAllBills_WhenBillExists_ReturnsBills()
        {
            //Arrange
            var billNumber = "200000000007540220";
            var bill = new BillVM { BillNumber = billNumber, PaymentMethod = "Visa", TotalPrice = 50, CreditCardNumber = "4111111111111111" };
            _billService.GetBillByID(Arg.Is(billNumber)).Returns(bill);

            using var app = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_billService);
            });

            var httpClient = app.CreateClient();

            //Act
            _billRepository.Setup(x => x.GetAllBills()).Returns(new List<Bill>().AsQueryable());
            var response = await httpClient.GetAsync("/api/Bill/GetAllBills");
            var responseString = await response.Content.ReadAsStringAsync();
            var billResult = JsonSerializer.Deserialize<BillVM>(responseString);

            Assert.Contains(bill, (System.Collections.ICollection?)billResult);
        }
    }
}
