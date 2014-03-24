using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Resources;

namespace WcfConverter
{
    public class WcfXmlMessage
    {
        private static XmlDictionary wcfDictionary;

        public static void buildDictionary(String dictionaryLocation)
        {
            string dictionary = null;
            if (dictionaryLocation == null || dictionaryLocation.Length==0)
                dictionary = WcfXml.Properties.Resources.dictionary;
            else
            {
                StreamReader dictionaryFile = new StreamReader(dictionaryLocation);
                dictionary = dictionaryFile.ReadToEnd();
            }

            List<string> lines = dictionary.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            WcfXmlMessage.wcfDictionary = new XmlDictionary();

            foreach (string line in lines)
            {
                WcfXmlMessage.wcfDictionary.Add(line);
            }
        }


        public static string FromArray(byte[] data)
        {
            XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(data, 0, (int)data.Length, WcfXmlMessage.wcfDictionary, XmlDictionaryReaderQuotas.Max);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlDictionaryReader);
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlDocument.WriteTo(xmlTextWriter);
            return stringWriter.ToString();
        }

        public static byte[] ToArray(string value)
        {
            if (value == null)
            {
                return null;
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(value);
            MemoryStream memoryStream = new MemoryStream();
            XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(memoryStream, WcfXmlMessage.wcfDictionary);
            xmlDocument.WriteTo(xmlDictionaryWriter);
            xmlDictionaryWriter.Flush();
            return memoryStream.ToArray();
        }
    }
}
