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
                int i = 0;
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Win32_Fan instance {0}", i);
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Description: {0}", queryObj["Description"]);
                    Console.WriteLine("Status: {0}", queryObj["Status"]);
                    i++;
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
        }

        public bool check_for_xiring_device()
        {
            var usbDevices = GetUSBDevices();
            foreach (var usbDevice in usbDevices)
            {
                Debug.WriteLine("Device ID : {0}, PNP Device ID {1}, Device Description {2}", usbDevice.DeviceID, usbDevice.PnpDeviceID, usbDevice.Description);
                if (usbDevice.PnpDeviceID == "SWD\\SCDEVICEENUM\\1_XIRING_USB_SMART_CARD_READER_0")
                {
                    return true;
                }
            }
            return false;
        }

        static List<USBDeviceInfo> GetUSBDevices()
        {
            List<USBDeviceInfo> devices = new List<USBDeviceInfo>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_PnPEntity"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                devices.Add(new USBDeviceInfo(
                (string)device.GetPropertyValue("DeviceID"),
                (string)device.GetPropertyValue("PNPDeviceID"),
                (string)device.GetPropertyValue("Description")
                ));
            }

            collection.Dispose();
            return devices;
        }
    }
    class USBDeviceInfo
    {
        public USBDeviceInfo(string deviceID, string pnpDeviceID, string description)
        {
            this.DeviceID = deviceID;
            this.PnpDeviceID = pnpDeviceID;
            this.Description = description;
        }
        public string DeviceID { get; private set; }
        public string PnpDeviceID { get; private set; }
        public string Description { get; private set; }
    }
}
