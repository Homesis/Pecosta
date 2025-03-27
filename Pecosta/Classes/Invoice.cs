using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecosta
{
    public class Invoice
    {
        public string DocumentNumber { get; set; }
        public List<Product> Products { get; set; }

        public Invoice() {
            Products = new List<Product>();
        }

        public override string ToString()
        {
            return $"\n\nDocument number: {DocumentNumber}\nProducts:\n{"CatalogCode".PadRight(15)}{"ProductName".PadRight(60)}{"Quantity".PadLeft(15)}/{"Unit".PadRight(5)}{"UnitPrice".PadLeft(15)}{"VatRate".PadLeft(15)}\n{string.Join("\n", Products)}";
        }
    }
}
