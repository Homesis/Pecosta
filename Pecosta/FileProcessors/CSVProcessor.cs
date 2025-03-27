using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pecosta
{
    public class JacobsCSVProcessor : IInvoiceProcesor
    {
        public Invoice ProcessInvoice(string filePath)
        {
            Invoice invoice = new Invoice();

            foreach (string line in File.ReadLines(filePath))
            {
                string[] values = line.Split(',');
                if (values[7] == "Č. faktury")
                {
                    invoice.DocumentNumber = values[9];
                }

                if (values[0].StartsWith("8")){
                    List<string> lineValues = ParseCsvLine(line);
                    string CatalogCode = lineValues[0];
                    string ProductName = lineValues[2];
                    string Unit = lineValues[3];
                    decimal Quantity = decimal.Parse(lineValues[5]);
                    decimal UnitPrice = decimal.Parse(lineValues[6]);


                    decimal VatRate = decimal.Parse(lineValues[9].TrimEnd('%'));

                    Product product = new Product(ProductName, CatalogCode, Quantity, UnitPrice, VatRate, Unit);
                    invoice.Products.Add(product);
                }
            }
            return invoice;
        }

        static List<string> ParseCsvLine(string line)
        {
            List<string> result = new List<string>();
            string pattern = "(?<=^|,)(?:\"(.*?)\"|([^,]*))";

            foreach (Match match in Regex.Matches(line, pattern))
            {
                result.Add(match.Groups[1].Success ? match.Groups[1].Value : match.Groups[2].Value);
            }

            return result;
        }
    }

    public class AlmecoCSVProcessor : IInvoiceProcesor
    {
        public Invoice ProcessInvoice(string filePath)
        {
            Invoice invoice = new Invoice();
            bool productStarted = false;
            string[] lines = File.ReadAllLines(filePath, Encoding.GetEncoding("windows-1250"));

            foreach (string line in lines)
            {
                string[] values = line.Split(';');
                if (values.Length > 2 && values[2].StartsWith("FAKTURA"))
                {
                    invoice.DocumentNumber = values[4];
                }
                else if (values[0].StartsWith("Označení"))
                {
                    productStarted = true;
                }
                else if (line == "")
                {
                    break;
                }
                else if (productStarted)
                {
                    if (values[0] == "")
                    {
                        continue;
                    }
                    else
                    {
                        string CatalogCode = values[3];
                        string ProductName = values[0];
                        string Unit = values[6];
                        decimal Quantity = decimal.Parse(values[4]);
                        decimal UnitPrice = decimal.Parse(values[7]);
                        decimal VatRate = decimal.Parse(values[8].TrimEnd('%'));
                        Product product = new Product(ProductName, CatalogCode, Quantity, UnitPrice, VatRate, Unit);
                        invoice.Products.Add(product);
                    }

                }
            }
            return invoice;
        }
    }
}
