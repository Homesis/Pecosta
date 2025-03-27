using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecosta
{
    public class Product
    {

        public string ProductName;
        public string CatalogCode;
        public decimal Quantity;
        public decimal UnitPrice;
        public string Unit;
        public decimal VatRate;

        public Product(string productName, string? catalogCode, decimal quantity, decimal unitPrice, decimal vatRate, string unit = "j.")
        {
            ProductName = productName;
            if (catalogCode == null)
            {
                Random random = new Random();
                int randomNumber = random.Next(10000, 99999);
                CatalogCode = $"{productName.Substring(0, Math.Min(productName.Length, 5))}{randomNumber.ToString()}";
            }
            else
            {
                CatalogCode = catalogCode;
            }

            if (quantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative");
            }
            Quantity = quantity;

            if (unitPrice < 0)
            {
                throw new ArgumentException("Unit price cannot be negative");
            }
            UnitPrice = unitPrice;

            Unit = unit;

            if (vatRate < 0)
            {
                throw new ArgumentException("Vat rate cannot be negative");
            }
            if (vatRate > 100) {
                throw new ArgumentException("Vat rate cannot be greater than 100");
            }
            VatRate = vatRate;
        }

        public override string ToString()
        {
            return $"{CatalogCode.PadRight(15)}{ProductName.PadRight(60)}{Quantity.ToString().PadLeft(15)}/{Unit.PadRight(5)}{UnitPrice.ToString().PadLeft(15)}{VatRate.ToString().PadLeft(15)}";
        }
    }
}
