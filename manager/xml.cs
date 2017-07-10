using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections;
using System.Diagnostics;
using Microsoft.Win32;

namespace xmlmanager
{
    public class xmlparser
    {
        //static void Main(string[] args)
        //{
        //    //xml2txt("hi.xml");

        //    //File.Delete("test.xml");
        //    // txt2xml rewrites txtfile 
        //    //txt2xml("hi.xml");
        //    getallsoft();
        //    Console.ReadLine();
        //}
        static void getallsoft()
        {
            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        Console.Write(subkey.GetValue("DisplayName") + "\t");

                        Console.Write(subkey.GetValue("DisplayVersion") + "\n");


                    }
                }
            }
        }
        
        static int check4file(string datfile)
        {
            if (File.Exists(datfile))
                return 0;
            else
            {
                return -1;
            }
            //Console.WriteLine(File.Exists(datfile) ? "File exists." : "File does not exist.");
        }

        static void xml2txt(string xmlfile)
        {
            if (check4file(xmlfile) != 0) { 
                Console.WriteLine("[ERROR] {0} : file not found", xmlfile);
                Debug.WriteLine(check4file(xmlfile));
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
        static void txt2xml(string txtfile)
        {
            XmlWriter xmlWriter = XmlWriter.Create(txtfile);
            string[] plp = new string[] { "matteyeux", "Felicien", "Thomas", "Thibaut", "Theo", "Ghywane" };
            // I keep this code as a sample
            //System.Collections.Generic.List<string> age = new System.Collections.Generic.List<string> { "19", "21", "20", "20", "19", "23"};
            //System.Collections.Generic.List<int> age = new System.Collections.Generic.List<int> { 19, 21, 20, 20, 19, 23 };
            string[] age = new string []{ "19", "21", "20", "20", "19", "23" };

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("users");
            for (int i = 0; i < plp.Length; i++)
            {   
                xmlWriter.WriteStartElement("user");

                Console.WriteLine("Age : {0}", age[i]);
                xmlWriter.WriteAttributeString("age", age[i]);

                Console.WriteLine("plp : {0}", plp[i]);
                xmlWriter.WriteString(plp[i]);

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            Console.ReadLine();
        }
    }
}