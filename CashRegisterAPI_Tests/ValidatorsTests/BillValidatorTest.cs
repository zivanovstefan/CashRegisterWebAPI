using CashRegister.API.Validator;
using CashRegister.Application.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CashRegisterAPI_Tests.ValidatorsTests
{
    [TestFixture]
    public class BillValidatorTest
    {
        //Setup
        private BillValidator _validator;
        public void TestInitialize()
        {
            _validator = new BillValidator();
        }
        [Test]
        public void IsCreditCardNumberValid_ValidCreditCardNumber_ReturnsTrue()
        {
            //Arrange
            var bill = new BillVM { BillNumber = "260005601001611379", PaymentMethod = "MasterCard", TotalPrice = 1000, CreditCardNumber = "371449635398431" };
            //Act
            var result = _validator.Validate(bill);
            //Assert
            result.Should().Be(true);
    }
  }
}
