using System;
using System.Collections.Generic;
using System.IO;

using System.Text;
using NDesk.Options;

namespace WcfConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            bool show_help = false;
            bool decode = false;
            bool encode = false;
            bool b64 = false;
            bool s = true;
            string file = "";
            string dict = null;
            int spec = 2;
            byte[] output;
            var p = new OptionSet() {
				{ "d|decode", "WCF binary to XML converter",
					v => decode = v !=null },
				{ "e|encode", "XML to WCF converter",
					v => encode = v !=null },
				{ "f|file=", "File location",
					(string v) => file = v },
				{ "b64|out-base64", "Encode output in Base64 format",
					v => b64 = v !=null },
				{ "s|out-string", "Encode output in String format (default)",
					v => s = v !=null },
                { "dict=", "Use external dictionary (using Microsoft dictionary by default)",
				 (string v) => dict = v},
                  { "spec=", "Specify NBFS specification (1 or 2). Default 2",
				 (int v) => spec = v},
				{ "h|help",  "show this message and exit", 
					v => show_help = v != null },
			};

            List<string> extra;
            if (args.Length == 0)
                ShowHelp(p);
            try
            {
                extra = p.Parse(args);
                WcfXmlMessage.buildDictionary(dict, spec);
                if (show_help)
                    ShowHelp(p);
                if (file.Length > 0)
                {
                    byte[] bytes = File.ReadAllBytes(file);
                    if (decode)
                        output = System.Text.Encoding.UTF8.GetBytes(WcfXmlMessage.FromArray(bytes));
                    else
                        output = WcfXmlMessage.ToArray(System.Text.Encoding.UTF8.GetString(bytes));
                    if (b64)
                        Console.WriteLine(System.Convert.ToBase64String(output));
                    else
                        Console.WriteLine(System.Text.Encoding.UTF8.GetString(output));
                }
            }
            catch (Exception exception)
            {
                Exception exception1 = exception;
                if (b64)
                    Console.WriteLine(Convert.ToBase64String(Encoding.UTF8.GetBytes(exception1.Message)));
                else
                    Console.WriteLine(exception1);
            }
        }
        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Convert from WCF SOAP binary to XML and vice versa");
            Console.WriteLine("Usage: wcfxml.exe <CONVERT OPTIONS> -f <File location> [OUTPUT OPTIONS] ");
            Console.WriteLine("[*] Decode example: wcfxml.exe -d -f test.bin -s");
            Console.WriteLine("[*] Encode example: wcfxml.exe -e -f test.bin -b64");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }

    }

}
