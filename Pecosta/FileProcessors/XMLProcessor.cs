using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pecosta
{
    public class XMLProcessor : IInvoiceProcesor
    {
        public Invoice ProcessInvoice(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            Invoice invoice = new Invoice();
            invoice.DocumentNumber = doc.Root.Element("id").Value;


            var items = doc.Descendants("items").Elements("item");

            foreach (var item in items)
            {
                string ProductName = item.Element("itemName").Value;
                string CatalogCode = item.Element("itemId").Value;
                decimal UnitPrice = decimal.Parse(item.Element("costPerUnitBrut").Value, CultureInfo.InvariantCulture);
                decimal VatRate = decimal.Parse(item.Element("vatRate").Value, CultureInfo.InvariantCulture);

                var quantityEl = item.Element("itemQty");
                decimal Quantity = decimal.Parse(quantityEl.Element("quantity").Value, CultureInfo.InvariantCulture);
                string Unit = quantityEl.Element("unit").Value;

                Product product = new Product(ProductName, CatalogCode, Quantity, UnitPrice, VatRate, Unit);

                invoice.Products.Add(product);
            }

            return invoice;
        }
    }
}
