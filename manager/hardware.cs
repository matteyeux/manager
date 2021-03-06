﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Management.Instrumentation;
using System.IO;
using System.Diagnostics;
using OpenHardwareMonitor.Collections;
using commonmanager;

namespace hardwaremanager
{
    public class hardwareinfo
    {
        commonstuff common = new commonstuff();

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
        /// <summary>
        /// permet de recuperer les infos sur la mémoire RAM
        /// </summary>
        /// <returns>void</returns>
        public void all_ram_info()
        {
            int slotnb = 0; // ARRAY STARTS AT 0;
            double total_ram = 0;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
                
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("\nSlot : {0}", slotnb);
                    Console.WriteLine("Tag: {0}", queryObj["Tag"]);
                    Console.WriteLine("Attributes: {0}", queryObj["Attributes"]);
                    Console.WriteLine("Manufacturer: {0}", queryObj["Manufacturer"]);
                    Console.WriteLine("SerialNumber: {0}", queryObj["SerialNumber"]);
                    Console.WriteLine("Capacity: {0}GB", common.convertb2gb(0, Convert.ToDouble(queryObj["Capacity"])));
                    total_ram += common.convertb2gb(0, Convert.ToDouble(queryObj["Capacity"]));
                    slotnb++;
                }
                Console.WriteLine("\nTotal RAM : {0}", total_ram);
            }   
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: {0} ", e.Message);
            }
        }

        /// <summary>
        /// verifie quel type de RAM est present dans la machine
        /// </summary>
        /// <returns>Le type de RAM (str)</returns>
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
        /// <summary>
        /// check dynamiquement la presence et le statut des ventilateurs
        /// </summary>
        /// <returns>void</returns>
        public void fan_stuff()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2","SELECT * FROM Win32_Fan");
                int fan_nb = 0;
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("\nFAN {0}", fan_nb);
                    Console.WriteLine("Description: {0}", queryObj["Description"]);
                    Console.WriteLine("Status: {0}", queryObj["Status"]);
                    fan_nb++;
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: {0}", e.Message);
            }
        }

        /// <summary>
        /// Verifie si le lecteur carte agent est branché
        /// Marche sur ma machine
        /// </summary>
        /// <returns>booleen (true or false)</returns>
        public bool check_for_xiring_device()
        {
            var usbDevices = GetUSBDevices();
            foreach (var usbDevice in usbDevices)
            {
                Debug.WriteLine("Device ID : {0}, PNP Device ID {1}, Device Description {2}", usbDevice.DeviceID, usbDevice.PnpDeviceID, usbDevice.Description);
                if (usbDevice.DeviceID == "SWD\\SCDEVICEENUM\\1_XIRING_USB_SMART_CARD_READER_0" || usbDevice.PnpDeviceID == "SWD\\SCDEVICEENUM\\1_XIRING_USB_SMART_CARD_READER_0")
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Liste les peripheriques USB presents sur la machine
        /// </summary>
        /// <returns>devices</returns>
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
