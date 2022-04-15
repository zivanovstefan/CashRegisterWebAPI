//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;
//using FluentAssertions;
//using System.Diagnostics;

//namespace CashRegisterWebAPI_IntegrationTests
//{
//    public class BillControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
//    {
//        private readonly HttpClient _client;

//        public BillControllerIntegrationTests(TestingWebAppFactory<Program> factory)
//            => _client = factory.CreateClient();
//        [Fact]
//        public async Task Index_WhenCalled_ReturnBills()
//        {
//            var response = await _client.GetAsync("/api/Bill/Bills");

//            response.EnsureSuccessStatusCode();

//            var responseString = await response.Content.ReadAsStringAsync();

//            Assert.Contains("260005601001611379", responseString);
//        }
//    }
//}
