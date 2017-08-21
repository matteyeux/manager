using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Xml;


namespace systemmanager
{
    class systeminfo
    {
        public void osinfo()
        {
            int uptime = Environment.TickCount & Int32.MaxValue;
            float secuptime = uptime / 1000; // millisec -> sec
            float houruptime = secuptime / 3600; // sec -> hours
            int dayuptime = (int) houruptime / 24; // hours -> days
            //XmlWriter xmlWriter = XmlWriter.Create("xml.xml");
            //xmlWriter.WriteStartDocument();
            //xmlWriter.WriteStartElement("softs");

            //xmlWriter.WriteStartElement("systeminfo");
            //xmlWriter.WriteAttributeString("sample", "sample");
            //xmlWriter.WriteString("test");
            //xmlWriter.WriteEndElement();

            //xmlWriter.WriteStartElement("soft");
            //xmlWriter.WriteAttributeString("version", "version");
            //xmlWriter.WriteString("test");
            //xmlWriter.WriteEndElement();

            //xmlWriter.WriteStartElement("soft");
            //xmlWriter.WriteAttributeString("what", "osversion");
            //xmlWriter.WriteString(Environment.OSVersion.ToString());
            //xmlWriter.WriteEndElement();

            //xmlWriter.WriteStartElement("soft");
            //xmlWriter.WriteAttributeString("what", "username");
            //xmlWriter.WriteString(Environment.UserName.ToString());
            //xmlWriter.WriteEndElement();

            //xmlWriter.WriteStartElement("soft");
            //xmlWriter.WriteAttributeString("what", "machine-name");
            //xmlWriter.WriteString(Environment.MachineName.ToString());
            //xmlWriter.WriteEndElement();

            //xmlWriter.WriteStartElement("soft");
            //xmlWriter.WriteAttributeString("what", "proc_number");
            //xmlWriter.WriteString(Environment.ProcessorCount.ToString());
            //xmlWriter.WriteEndElement();

            //xmlWriter.WriteEndElement();
            //xmlWriter.WriteEndDocument();

            //xmlWriter.Close();
            //Console.WriteLine("done");
            Console.WriteLine("OS Version: {0}", Environment.OSVersion.ToString());
            Console.WriteLine("User: {0}", Environment.UserName.ToString());
            Console.WriteLine("System name: {0}", Environment.MachineName.ToString());
            Console.WriteLine("Proc count: {0}", Environment.ProcessorCount.ToString());
            Console.WriteLine("Uptime: {0} days", dayuptime);
            Console.WriteLine("System Drive: {0} ", Environment.ExpandEnvironmentVariables("%SystemDrive%"));
        }

        // This function is used to check integrity of boot process
        public int boot_conf()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_BootConfiguration");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("\nBoot Config");
                    Console.WriteLine("BootDirectory: {0}", queryObj["BootDirectory"]);
                    Console.WriteLine("TempDirectory: {0}", queryObj["TempDirectory"]);
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: {0}", e.Message);
                return -1;
            }
            return 0;
        }

        public bool windows_info()
        {
            bool is64bit = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
            if (is64bit)
            {
                Console.WriteLine("Windows architecture is 64 bits");
            }
            else
            {
                Console.WriteLine("Windows architecture is 32 bits");
            }
            return is64bit;
        }

        public int bios_is_cool()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_BIOS");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("BIOS: {0}", queryObj["Caption"]); //Just check if default BIOS / RIP backdoors

                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
            return 0;
        }

    }
}
