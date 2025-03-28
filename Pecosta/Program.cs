using System.Globalization;
using System.Xml.Linq;
using Pecosta;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);


string filePath = "";

IInvoiceProcesor processor = ProcessorFactory.GetProcessor(filePath);

Invoice invoice1 = processor.ProcessInvoice(filePath);

Console.WriteLine(invoice1);

