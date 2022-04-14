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

namespace CashRegisterWebAPI_IntegrationTests
{
    public class BillEndpointsTests
    {
        private readonly IBillService _billService =
            Substitute.For<IBillService>();

        [Test]
        public async Task GetBillById_WhenBillExists_ReturnsBill()
        {
            //Arrange
            var billNumber = "200000000007540220";
            var bill = new BillVM { BillNumber = billNumber, PaymentMethod = "Visa" , TotalPrice = 50, CreditCardNumber = "4111111111111111"};
            _billService.GetBillByID(Arg.Is(billNumber)).Returns(bill);

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
            billResult.Should().BeEquivalentTo(bill);
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
            response.StatusCode.Should().Be(HttpStatusCode.OK);
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
