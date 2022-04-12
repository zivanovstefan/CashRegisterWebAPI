using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using FluentAssertions;
using CashRegister.Application.Services;
using Microsoft.AspNetCore.Mvc;
using CashRegister.Application.ErrorModels;
using CashRegister.Domain.Common;

namespace CashRegisterAPI_Tests.ServicesTests
{
    [TestFixture]
    public class CurrencyExchangeServiceTests
    {
        private int _amount;
        private int _zeroAmount;
        private string _eurCurrrency;
        private string _usdCurrency;
        private string _notSupportedCurrency;
        ErrorResponseModel notSupportedCurrencyError;
        ErrorResponseModel invalidAmountError;
        [SetUp]
        public void Setup()
        {
            _amount = 1000;
            _eurCurrrency = "EUR";
            _usdCurrency = "USD";
            _notSupportedCurrency = "GBP";
            notSupportedCurrencyError = new ErrorResponseModel()
            {
                ErrorMessage = Messages.CurrencyNotSupported,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
            invalidAmountError = new ErrorResponseModel()
            {
                ErrorMessage = Messages.InvalidAmount,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
        [Test]
        public void Exchange_EURCurrency_ReturnsExchangedAmount()
        {
            //Arrange
            ActionResult<int> expectedResult = 10;
            //Act
            var actualResult = new CurrencyExchangeService().Exchange(_amount, _eurCurrrency);
            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
        [Test]
        public void Exchange_USDCurrency_ReturnsExchangedAmount()
        {
            //Arrange
            ActionResult<int> expectedResult = 20;
            //Act
            var actualResult = new CurrencyExchangeService().Exchange(_amount, _usdCurrency);
            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
        [Test]
        public void Exchange_NotSupportedCurrency_ReturnsNotSupportedCurrencyError()
        {
            //Arrange
            ActionResult<int> expectedResult = new BadRequestObjectResult(notSupportedCurrencyError);
            //Act
            var actualResult = new CurrencyExchangeService().Exchange(_amount, _notSupportedCurrency);
            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
        [Test]
        public void Exchange_ZeroAmount_ReturnsInvalidAmountError()
        {
            //Arrange
            ActionResult<int> expectedResult = new BadRequestObjectResult(invalidAmountError);
            //Act
            var actualResult = new CurrencyExchangeService().Exchange(_zeroAmount, _notSupportedCurrency);
            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}
