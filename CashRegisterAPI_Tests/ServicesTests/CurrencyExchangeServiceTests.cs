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
        private int _amount;
        private string _eurCurrrency;
        private string _usdCurrency;
        private string _notSupportedCurrency;
        [SetUp]
        public void Setup()
        {
            _amount = 1000;
            _eurCurrrency = "EUR";
            _usdCurrency = "USD";
            _notSupportedCurrency = "GBP";
        }
        [Test]
        public void Exchange_EURCurrency_ReturnsExchangedAmount()
        {
            //Arrange
            var expectedResult = 10;
            //Act
            var actualResult = new CurrencyExchangeService().Exchange(_amount, _eurCurrrency);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void Exchange_USDCurrency_ReturnsExchangedAmount()
        {
            //Arrange
            var expectedResult = 20;
            //Act
            var actualResult = new CurrencyExchangeService().Exchange(_amount, _usdCurrency);
            //Assert
            actualResult.Should().Be(expectedResult);
        }
    }
}
