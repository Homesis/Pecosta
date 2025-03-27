using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecosta
{
    public class ProcessorFactory
    {
        public static IInvoiceProcesor GetProcessor(string filePath)
        {
            if (filePath.EndsWith(".xml"))
            {
                return new XMLProcessor();
            }
            else if (filePath.EndsWith(".txt"))
            {
                return new TXTProcessor();
            }
            else if (filePath.EndsWith(".csv"))
            {
                return CSVProcessorFactory.GetProcessor(filePath);
            }
            else
            {
                throw new ArgumentException("Unsupported file format");
            }
        }
    }

    public class CSVProcessorFactory
    {
        public static IInvoiceProcesor GetProcessor(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string firstLine = sr.ReadLine();
                string secondLine = sr.ReadLine();
                if (firstLine.Contains("JACOBS"))
                {
                    return new JacobsCSVProcessor();
                }
                else if (secondLine.Contains("Almeco"))
                {
                    return new AlmecoCSVProcessor();
                }
                else
                {
                    throw new ArgumentException("Unsupported Invoice");
                }
            }
        }
    }
}
