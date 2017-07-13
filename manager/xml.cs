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
        //static void Main(string[] args)
        //{
        //    //xml2txt("hi.xml");

        //    //File.Delete("test.xml");
        //    // txt2xml rewrites txtfile 
        //    //txt2xml("hi.xml");
        //    getallsoft();
        //    Console.ReadLine();
        //}
        //commonstuff common = new commonstuff();
        //static int check4file(string datfile)
        //{
        //    if (File.Exists(datfile))
        //        return 0;
        //    else
        //    {
        //        return -1;
        //    }
        //    //Console.WriteLine(File.Exists(datfile) ? "File exists." : "File does not exist.");
        //}
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
        public void txt2xml(string txtfile, string tool, string version)
        {
            XmlWriter xmlWriter = XmlWriter.Create(txtfile);
            string[] plp = new string[] { "matteyeux", "Felicien", "Thomas", "Thibaut", "Theo", "Ghywane" };
            // I keep this code as a sample
            //System.Collections.Generic.List<string> age = new System.Collections.Generic.List<string> { "19", "21", "20", "20", "19", "23"};
            //System.Collections.Generic.List<int> age = new System.Collections.Generic.List<int> { 19, 21, 20, 20, 19, 23 };
            string[] age = new string[] { "19", "21", "20", "20", "19", "23" };

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("softs");

            xmlWriter.WriteStartElement("soft");
            xmlWriter.WriteAttributeString("version", version);
            xmlWriter.WriteString(tool);

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            Console.ReadLine();
        }

        public void getallsoft()
        {
            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {   /*XmlWriter xmlWriter = XmlWriter.Create("xml.xml");
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("softs");*/
                // Compose a string that consists of three lines.

                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter("test.txt");

                ;

                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        string softname = (string)subkey.GetValue("DisplayName");
                        string softvers = (string)subkey.GetValue("DisplayVersion");
                        Console.Write(softname + "\t");
                        Console.Write(softvers + "\n");
                        file.WriteLine(softname);
                        //if (softname == "DeepBurner")
                        //{
                        //    Console.WriteLine("what");
                        //    Console.ReadLine();
                        //}
                        /*xmlWriter.WriteStartElement("soft");
                        xmlWriter.WriteAttributeString("version", softvers);
                        xmlWriter.WriteString(softname);
                        xmlWriter.WriteEndElement();*/

                    }
                }
                file.Close();
                /*xmlWriter.WriteEndDocument();
                xmlWriter.Close();*/
            }
        }
    }
}
