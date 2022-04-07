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
        //ARRANGE
        private BillValidator _validator;
        BillVM BillVMAllValid;
        BillVM BillVMInvalidCreditCard;
        BillVM BillVMInvalidBillNumber;
        BillVM BillVMBothInvalid;
        BillVM BillVMInvalidTotalPrice;
        [SetUp]
        public void Setup()
        {
            //valid VM
            _validator = new BillValidator();
            BillVMAllValid = new BillVM();
            BillVMAllValid.BillNumber = "260005601001611379";
            BillVMAllValid.TotalPrice = 1000;
            BillVMAllValid.CreditCardNumber = "4111111111111111";

            //invalid VM (invalid credit card)
            _validator = new BillValidator();
            BillVMInvalidCreditCard = new BillVM();
            BillVMInvalidCreditCard.BillNumber = "260005601001611379";
            BillVMInvalidCreditCard.TotalPrice = 1000;
            BillVMInvalidCreditCard.CreditCardNumber = "13281381900";

            //invalid VM (invalid bill number)
            _validator = new BillValidator();
            BillVMInvalidBillNumber = new BillVM();
            BillVMInvalidBillNumber.BillNumber = "262009601001611379";
            BillVMInvalidBillNumber.TotalPrice = 1000;
            BillVMInvalidBillNumber.CreditCardNumber = "4111111122111111";

            //invalid VM (both bill number and credit card invalid)
            _validator = new BillValidator();
            BillVMBothInvalid = new BillVM();
            BillVMBothInvalid.BillNumber = "260005601001611379";
            BillVMBothInvalid.TotalPrice = 1000;
            BillVMBothInvalid.CreditCardNumber = "4111122111111111";

            //invalid VM (invalid Total price)
            _validator = new BillValidator();
            BillVMInvalidTotalPrice = new BillVM();
            BillVMInvalidTotalPrice.BillNumber = "260005601001611379";
            BillVMInvalidTotalPrice.TotalPrice = 10000;
            BillVMInvalidTotalPrice.CreditCardNumber = "4111111111111111";
        }
        [Test]
        public void Validate_ValidBillVM_ReturnsTrue()
        {
            //ACT
            var result = _validator.Validate(BillVMAllValid);
            //ASSERT
            result.IsValid.Should().BeTrue();
        }
        [Test]
        public void Validate_InvalidCreditCard_ReturnsFalse()
        {
            //ACT
            var result = _validator.Validate(BillVMInvalidCreditCard);
            //ASSERT
            result.IsValid.Should().BeFalse();
        }
        [Test]
        public void Validate_ValidBillNumberAndInvalidCreditCard_ReturnsFalse()
        {
            //ACT
            var result = _validator.Validate(BillVMInvalidBillNumber);
            //ASSERT
            result.IsValid.Should().BeFalse();
        }
        [Test]
        public void Validate_InvalidBillNumberAndValidCreditCard_ReturnsFalse()
        {
            //ACT
            var result = _validator.Validate(BillVMBothInvalid);
            //ASSERT
            result.IsValid.Should().BeFalse();
        }
        [Test]
        public void Validate_InvalidTotalAmount_ReturnsFalse()
        {
            //ACT
            var result = _validator.Validate(BillVMInvalidTotalPrice);
            //ASSERT
            result.IsValid.Should().BeFalse();
        }
    }
  }
