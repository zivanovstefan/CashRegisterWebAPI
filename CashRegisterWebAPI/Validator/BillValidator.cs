using CashRegister.Application.ViewModels;
using FluentValidation;
using System;
namespace CashRegister.API.Validator
{
    public class BillValidator : AbstractValidator<BillVM>
    {
        public BillValidator()
        {
            RuleFor(b => b.BillNumber).Length(18);
            RuleFor(b => b.BillNumber).Must(IsBillNumberValid).WithMessage("Bill number is not valid");
            RuleFor(b => b.CreditCardNumber).Must(IsCreditCardNumberValid).WithMessage("Credit card number is not valid");
            RuleFor(b => b.TotalPrice).LessThanOrEqualTo(5000);
        }
        private bool IsBillNumberValid(string billNumber)
        {
            int controlNumber = Convert.ToInt32(billNumber.Substring(billNumber.Length - 2));
            string billSubstring = billNumber.Substring(0, 16);
            long numberBody = long.Parse(billSubstring);
            if (98 - ((numberBody * 100) % 97) == controlNumber)
            {
                return true;
            }
            return false;
        }
        private bool IsCreditCardNumberValid(string cardNumber)
        {
            bool isValid = true;
            if (cardNumber == null)
            {
                isValid = true;
                return isValid;
            }
            if (cardNumber.Length != 13 && cardNumber.Length != 15 && cardNumber.Length != 16)
            {
                isValid = false;
            }
            else
            {
                if ((cardNumber.Length == 13 || cardNumber.Length == 16) && cardNumber.StartsWith('4'))
                {
                    isValid = ValidateCreditCard(cardNumber);

                }
                else if (cardNumber.Length == 15 && (cardNumber.StartsWith("34") || cardNumber.StartsWith("37")))
                {
                    isValid = ValidateCreditCard(cardNumber);
                }
                else if (cardNumber.Length == 16 && (cardNumber.StartsWith("51") || cardNumber.StartsWith("52") || cardNumber.StartsWith("53")
                    || cardNumber.StartsWith("54") || cardNumber.StartsWith("55")))
                {
                    isValid = ValidateCreditCard(cardNumber);
                }

                else isValid = false;
            }
            return isValid;
        }
        private bool ValidateCreditCard(string cardNumber)
        {
            var cardReverse = cardNumber.Reverse();
            //reversing every digit with odd index digits
            var reverseEveryDigitWithOddIndex = new string(cardReverse.Where((ch, index) => index % 2 != 0).ToArray());
            //multiplication of odd index digits
            string MultiplyDigitsByTwo = "";
            for (int i = 0; i < reverseEveryDigitWithOddIndex.Length; i++)
            {
                int num = Int32.Parse((reverseEveryDigitWithOddIndex[i].ToString()));
                num = num * 2;
                MultiplyDigitsByTwo = MultiplyDigitsByTwo + num.ToString();
            }
            int multipliedDigitsSummed = 0;
            for (int i = 0; i < MultiplyDigitsByTwo.Length; i++)
            {
                int num = Int32.Parse((MultiplyDigitsByTwo[i].ToString()));
                multipliedDigitsSummed = multipliedDigitsSummed + num;
            }
            //sum of digits that were not multiplied
            var digitsWerentMultiplied = new string(cardReverse.Where((ch, index) => index % 2 == 0).ToArray());
            var digitsWerentMultipliedToDigits = Int32.Parse(digitsWerentMultiplied);
            int digitsWerentMultipliedSum = 0;
            for (int i = 0; i < digitsWerentMultiplied.Length; i++)
            {
                int num = Int32.Parse((digitsWerentMultiplied[i].ToString()));
                digitsWerentMultipliedSum = digitsWerentMultipliedSum + num;
            }
            //
            int endResult = digitsWerentMultipliedSum + multipliedDigitsSummed;
            bool isValidCard = endResult % 10 == 0;
            return isValidCard;
        }
    }
}

