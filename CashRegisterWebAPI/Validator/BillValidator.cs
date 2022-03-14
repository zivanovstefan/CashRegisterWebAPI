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
            RuleFor(b => b.BillNumber).Must(isBillNumberValid).WithMessage("Bill number is not valid");
        }
        private bool isBillNumberValid(string billNumber)
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
    }
}
