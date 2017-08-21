using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections;
using System.Diagnostics;
using Microsoft.Win32;
using commonmanager;

namespace xmlmanager
{
    public class xmlparser
    {
        commonstuff common = new commonstuff();
        public void xml2txt(string xmlfile)
        {
            if (common.check4file(xmlfile) != 0)
            {
                Console.WriteLine("[ERROR] {0} : file not found", xmlfile);
                Debug.WriteLine(common.check4file(xmlfile));
                return;
            }
            XmlReader xmlReader = XmlReader.Create(xmlfile);
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "user"))
                {
                    if (xmlReader.HasAttributes)
                    {
                        Console.WriteLine(xmlReader.GetAttribute("age") + ": " + xmlReader.GetAttribute("   "));
                    }
                }
            }
            Console.ReadLine();
        }
        //string[] plp = new string[] { "matteyeux", "eehp", "Thomas", "Thibaut", "Theo", "Ghywane" };
        // I keep this code as a sample
        //System.Collections.Generic.List<string> age = new System.Collections.Generic.List<string> { "19", "21", "20", "20", "19", "23"};
        //System.Collections.Generic.List<int> age = new System.Collections.Generic.List<int> { 19, 21, 20, 20, 19, 23 };
        //string[] age = new string[] { "19", "21", "20", "20", "19", "23" };


        /// <summary>
        /// method to send all data in a xml file 
        /// </summary>
        /// <returns>nothing lol</returns>
        public void write2xml(string xml2write, string element, string elementname, string thing2write, string element_attrib)
        {
            XmlWriter xmlWriter = XmlWriter.Create(xml2write); 
            

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement(element);

            xmlWriter.WriteStartElement(elementname);
            xmlWriter.WriteAttributeString("version", element_attrib);
            xmlWriter.WriteString(thing2write);


            xmlWriter.WriteEndElement(); 
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            Console.ReadLine();
        }
        //xml_sample("xmlfile", "element", "element_name", "thing2write", "element_attrib");
        //public void xml_sample(string xml2write, string element, string elementname, string thing2write, string element_attrib)
        //{
        //    XmlWriter xmlWriter = XmlWriter.Create(xml2write);


        //    xmlWriter.WriteStartDocument();
        //    xmlWriter.WriteStartElement(element);

        //    xmlWriter.WriteStartElement(elementname);
        //    xmlWriter.WriteAttributeString("version", element_attrib);
        //    xmlWriter.WriteString(thing2write);


        //    xmlWriter.WriteEndElement();
        //    xmlWriter.WriteEndDocument();
        //    xmlWriter.Close();
        //    Console.ReadLine();
        //}

        public void getallsoft(string file2write)
        {
            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"; 
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                if (common.check4file(file2write) == 0)
                {
                    File.Delete(file2write);
                }

                XmlWriter xmlWriter = XmlWriter.Create(file2write);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("softs");
                // Compose a string that consists of three lines.

                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter("test.txt");

                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        string softname = (string)subkey.GetValue("DisplayName");
                        string softvers = (string)subkey.GetValue("DisplayVersion");
                        if ((softname != "" && softvers != "") || (softname != "" || softvers != ""))
                        {
                            Console.Write(softname + "\t");
                            Console.Write(softvers + "\n");
                            xmlWriter.WriteStartElement("soft");
                            xmlWriter.WriteAttributeString("version", softvers);
                            xmlWriter.WriteString(softname);
                            xmlWriter.WriteEndElement();
                        }

                        //Console.Write(softname + "\t");
                        //Console.Write(softvers + "\n");
                        //file.WriteLine(softname);
                       

                    }
                }
                //file.Close();
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
        }
    }
}
