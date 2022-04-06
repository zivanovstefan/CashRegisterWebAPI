using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Commands
{
    public class CreateProductCommand : ProductCommand
    {
        public CreateProductCommand(int id, string name, int price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
