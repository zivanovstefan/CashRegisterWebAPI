using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using FluentAssertions;
using CashRegister.Application.Services;

namespace CashRegisterAPI_Tests.ServicesTests
{
    [TestFixture]
    public class CurrencyExchangeServiceTests
    {
        [Test]
        public void Exchange_EURCurrency_ReturnsExchangedAmount()
        {
            //Arrange
            var amount = 1000;
            var currency = "EUR";
            var expectedResult = 10;
            //Act
            var actualResult = new CurrencyExchangeService().Exchange(amount, currency);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void Exchange_USDCurrency_ReturnsExchangedAmount()
        {
            //Arrange
            var amount = 1000;
            var currency = "USD";
            var expectedResult = 20;
            //Act
            var actualResult = new CurrencyExchangeService().Exchange(amount, currency);
            //Assert
            actualResult.Should().Be(expectedResult);
        }
        //[Test]
        //public void Exchange_NotSupportedCurrency_ThrowsException()
        //{
        //    //Arrange
        //    var amount = 1000;
        //    var currency = "GBP";
        //    //Act
        //    var actualResult = new CurrencyExchangeService().Exchange(amount, currency);
        //    //Assert
        //    Assert.Throws<Exception>("Currency not supported");
        //}
    }
}
