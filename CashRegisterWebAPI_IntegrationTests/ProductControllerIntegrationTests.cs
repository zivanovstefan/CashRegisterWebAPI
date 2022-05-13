using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using CashRegister.Application.ViewModels;
using System.Text.Json;
using FluentAssertions;

namespace CashRegisterWebAPI_IntegrationTests
{
    public class ProductControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        [Fact]
        public async Task GetAllProducts_IfProductTableIsNotEmpty_ReturnProducts()
        {
            HttpClient client = new TestingWebAppFactory<Program>().CreateClient();

            var response = await client.GetAsync("/api/Product/Get all products");
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Book", responseString);
            Assert.Contains("Computer", responseString);
        }

        [Fact]
        public async Task GetAllProducts_IfProductTableIsEmpty_ReturnNotFoundObjectResult()
        {
            HttpClient client = new TestingWebAppFactory<Program>().CreateClient();

            await client.DeleteAsync("/api/Product/Delete product1");
            await client.DeleteAsync("/api/Product/Delete product2");
            var response = await client.GetAsync("/api/Product/Get all products");
            var responseString = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
            Assert.Contains("Products table is empty.", responseString);
        }

        [Fact]
        public async Task DeleteProduct_ProductWithInsertedIdExist_ReturnsTrue()
        {
            HttpClient client = new TestingWebAppFactory<Program>().CreateClient();

            var response = await client.DeleteAsync("/api/Product/Delete product1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("true", responseString);
        }

        [Fact]
        public async Task DeleteProduct_IfProductDoesNotExist_ReturnsNotFoundObjectResultWithErrorMessage()
        {
            HttpClient client = new TestingWebAppFactory<Program>().CreateClient();

            var response = await client.DeleteAsync("/api/Product/Delete product5");
            var responseString = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
            Assert.Contains("Product with inserted id does not exist.", responseString);
        }

        [Fact]
        public async Task CreateProduct_IfProductWithInsertedIdDoesNotExist_ReturnsOkStatusCode()
        {
            var productVM = new ProductVM()
            {
                Id = 5,
                Name = "Phone",
                Price = 100
            };
            var serializedVM = JsonSerializer.Serialize(productVM);
            var content = new StringContent(serializedVM, Encoding.UTF8, "application/json");
            HttpClient client = new TestingWebAppFactory<Program>().CreateClient();
            var response = await client.PostAsync("/api/Product/Create product", content);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
