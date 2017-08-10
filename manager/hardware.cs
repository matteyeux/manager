using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Management.Instrumentation;
using System.IO;
using System.Diagnostics;
using OpenHardwareMonitor.Collections;

namespace hardwaremanager
{
    public class hardwareinfo
    {   
         public int partition_number()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_DiskDrive");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("Partitions: {0}", queryObj["Partitions"]);
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
                return -1;        
            }
            return 0;
        }
               
        public void voltage_info()
        {
            System.Management.ObjectQuery query = new ObjectQuery("Select * FROM Win32_Battery");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject mo in collection)
            {
                foreach (PropertyData property in mo.Properties)
                {
                    Console.WriteLine("Property {0}: Value is {1}", property.Name, property.Value);
                }
            }
        }
        public void all_ram_info()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("Tag: {0}", queryObj["Tag"]);
                    Console.WriteLine("Attributes: {0}", queryObj["Attributes"]);
                    Console.WriteLine("Manufacturer: {0}", queryObj["Manufacturer"]);
                    Console.WriteLine("SerialNumber: {0}", queryObj["SerialNumber"]);
                    Console.WriteLine("TotalWidth: {0}", queryObj["TotalWidth"]); // max possible ?
                    Console.WriteLine("Capacity: {0}", queryObj["Capacity"]); // RAM 
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
        }
        
        public string ram_Type()
        {
               
            int type = 0;
            var searcher = new ManagementObjectSearcher("Select * from Win32_PhysicalMemory");
            foreach (ManagementObject obj in searcher.Get())
            {
                type = Int32.Parse(obj.GetPropertyValue("MemoryType").ToString());

            }

            switch (type)
            {
                case 20:
                    return "DDR";
                case 21:
                    return "DDR-2";
                case 17:
                    return "SDRAM";
                default:
                    if (type == 0 || type > 22)
                        return "DDR-3";
                    else
                        return "Unknown";
            }

        }
        public void fan_stuff()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2","SELECT * FROM Win32_Fan");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Win32_Fan instance");
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Description: {0}", queryObj["Description"]);
                    Console.WriteLine("Status: {0}", queryObj["Status"]);
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
        }
    }
}
