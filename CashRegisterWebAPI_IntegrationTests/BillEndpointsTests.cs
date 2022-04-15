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

namespace CashRegisterWebAPI_IntegrationTests
{
    public class BillEndpointsTests
    {
        private  Mock<IBillRepository> _billRepository;
        private  IBillService _billService =
            Substitute.For<IBillService>();
        private  BillService billService;
        private  IMapper _mapper =
    Substitute.For<IMapper>();
        private  IMediatorHandler _mediator =
    Substitute.For<IMediatorHandler>();
        [SetUp]
        public void Setup()
        {
            _billRepository = new Mock<IBillRepository>();
            var billService = new BillService(_mapper, _billRepository.Object, _mediator);
        }
        [Test]
        public async Task GetBillById_WhenBillExists_ReturnsBill()
        {
            //Arrange
            var billNumber = "200000000007540220";
            var bill = new Bill { BillNumber = billNumber, PaymentMethod = "Visa", TotalPrice = 50, CreditCardNumber = "4111111111111111" };
            var billVM = new BillVM { BillNumber = billNumber, PaymentMethod = "Visa" , TotalPrice = 50, CreditCardNumber = "4111111111111111"};
            _billRepository.Setup(x => x.GetBillByID(billNumber)).Returns(bill);
            _billService.GetBillByID(billNumber).Returns(billVM);

            using var app = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_billService);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.GetAsync($"/api/Bill/GetBillByBillNumber{billNumber}");
            var responseText = await response.Content.ReadAsStringAsync();
            var billResult = JsonSerializer.Deserialize<BillVM>(responseText);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            billResult.Should().BeEquivalentTo(billVM);
        }
        [Test]
        public async Task GetBillById_WhenBillDoesntExist_ReturnsNotFound()
        {
            //Arrange
            _billService.GetBillByID(Arg.Any<string>()).Returns((BillVM?)null);

            using var app = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_billService);
            });

            var billNumber = "200000000007540220";
            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.GetAsync($"/api/Bill/GetBillByBillNumber{billNumber}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }
        [Test]
        public async Task GetAllBills_WhenThereAreNoBillsInDB_ReturnsOKStatusCode()
        {
            //Arrange
            _billService.GetAllBills().Returns(new List<BillVM>());

            using var app = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_billService);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.GetAsync($"/api/Bill/Get all bills");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
