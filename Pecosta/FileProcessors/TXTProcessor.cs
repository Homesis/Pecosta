using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pecosta
{
    public class TXTProcessor : IInvoiceProcesor
    {
        public Invoice ProcessInvoice(string filePath)
        {
            Invoice invoice = new Invoice();
            string[] lines = File.ReadAllLines(filePath, Encoding.GetEncoding("windows-1250"));

            foreach (string line in lines)
            {
                string[] values = line.Split(' ',StringSplitOptions.RemoveEmptyEntries);
                if (values[0].StartsWith("HDR"))
                {
                    invoice.DocumentNumber = values[2];
                }

                if (values[0].StartsWith("LIN"))
                {

                    string CatalogCode = values[2];


                    decimal UnitPrice = 0;
                    decimal VatRate = 0;
                    Match match = Regex.Match(values[5], @"^(\d+)\.(\d{2})(\d*)\.(\d{2})$");
                    if (match.Success)
                    {
                        string price = $"{match.Groups[1].Value}.{match.Groups[2].Value}";
                        string vat = $"{match.Groups[3].Value}.{match.Groups[4].Value}";
                        UnitPrice = decimal.Parse(price, CultureInfo.InvariantCulture);
                        VatRate = decimal.Parse(vat, CultureInfo.InvariantCulture);
                    }
                    else { throw new FormatException(); }
                    

                    decimal Quantity = 0;
                    string Unit = "j.";
                    match = Regex.Match(values[6], @"^([\d.,]+)([a-zA-Z]*)$");
                    if (match.Success)
                    {
                        Quantity = decimal.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture); ;
                        Unit = match.Groups[2].Value;
                    }
                    else { throw new FormatException(); }
                    
                    
                    string ProductName = string.Join(" ", values.Skip(7));


                    Product product = new Product(ProductName, CatalogCode, Quantity, UnitPrice, VatRate, Unit);
                    invoice.Products.Add(product);
                }
            }

            return invoice;
        }
    }
}
