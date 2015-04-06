using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;
using System.IO;
using System.Resources;
using System.Runtime.Serialization;

namespace WcfConverter
{
    public class WcfXmlMessage
    {
        private static XmlDictionary wcfDictionary;
      
       
        public static void buildDictionary(String dictionaryLocation, int spec)
        {
            string dictionary = null;
            if (dictionaryLocation == null || dictionaryLocation.Length==0){
                if (spec == 1) dictionary = WcfXml.Properties.Resources.dictionary_1_0;
                if (spec == 2) dictionary = WcfXml.Properties.Resources.dictionary_2_0;
            }

   
            else
            {
                StreamReader dictionaryFile = new StreamReader(dictionaryLocation);
                dictionary = dictionaryFile.ReadToEnd();
            }

            String[] lines = dictionary.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            WcfXmlMessage.wcfDictionary = new XmlDictionary();            
            foreach (string line in lines)
            {
                WcfXmlMessage.wcfDictionary.Add(line);
            }
        }


        public static string FromArray(byte[] data)
        {
            Stream stream = new MemoryStream(data);
            XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(stream, wcfDictionary,XmlDictionaryReaderQuotas.Max);          
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
            XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(memoryStream, wcfDictionary);
            xmlDocument.WriteTo(xmlDictionaryWriter);
            xmlDictionaryWriter.Flush();
            return memoryStream.ToArray();
        }
    }
}
