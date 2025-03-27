using System.Globalization;
using System.Xml.Linq;
using Pecosta;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);


string filePath = "C:\\Users\\Feda\\source\\repos\\Pecosta\\Pecosta\\Files\\priklad2.xml";

IInvoiceProcesor processor = ProcesorFactory.GetProcessor(filePath);

Invoice invoice1 = processor.ProcessInvoice(filePath);

Console.WriteLine(invoice1);


filePath = "C:\\Users\\Feda\\source\\repos\\Pecosta\\Pecosta\\Files\\priklad3.txt";

processor = ProcesorFactory.GetProcessor(filePath);

Invoice invoice2 = processor.ProcessInvoice(filePath);

Console.WriteLine(invoice2);


filePath = "C:\\Users\\Feda\\source\\repos\\Pecosta\\Pecosta\\Files\\priklad.csv";

processor = ProcesorFactory.GetProcessor(filePath);

Invoice invoice3 = processor.ProcessInvoice(filePath);

Console.WriteLine(invoice3);


filePath = "C:\\Users\\Feda\\source\\repos\\Pecosta\\Pecosta\\Files\\priklad1.csv";

processor = ProcesorFactory.GetProcessor(filePath);

Invoice invoice4 = processor.ProcessInvoice(filePath);

Console.WriteLine(invoice4);

Console.WriteLine(Path.Combine(AppContext.BaseDirectory, "Files", "priklad3.txt"));