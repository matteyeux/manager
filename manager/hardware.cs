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
        //public string check_for_disk_name(string drivename)
        //{
        //    if (drivename == "" || drivename == null)
        //    {
        //        drivename = "C";
        //    }
        //    return drivename;
        //}

        /// <summary>
        /// method to retrieve the selected HDD's serial number
        /// </summary>
        /// <param name="strDriveLetter">Drive letter to retrieve serial number for</param>
        /// <returns>the HDD's serial number</returns>
        public string GetHDDSerialNumber(string drive)
        {
            //check to see if the user provided a drive letter
            //if not default it to "C"
            if (drive == "" || drive == null)
            {
                drive = "C";
            }
            //create our ManagementObject, passing it the drive letter to the
            //DevideID using WQL
            ManagementObject disk = new ManagementObject("Win32_LogicalDisk.DeviceID=\"" + drive + ":\"");
            //bind our management object
                disk.Get();
            //return the serial number
            return disk["VolumeSerialNumber"].ToString();
        }

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
   

        /// <summary>
        /// method to retrieve the HDD's freespace
        /// </summary>
        /// <param name="drive">Drive letter to get free space from (optional)</param>
        /// <returns>The free space of the selected HDD</returns>
        public double GetHDDFreeSpace(string drive)
        {
            //check to see if the user provided a drive letter
            //if not default it to "C"
            if (drive == "" || drive == null)
            {
                drive = "C";
            }
            //create our ManagementObject, passing it the drive letter to the
            //DevideID using WQL
            ManagementObject disk = new ManagementObject("Win32_LogicalDisk.DeviceID=\"" + drive + ":\"");
            //bind our management object
            disk.Get();
            //return the free space amount
            return Convert.ToDouble(disk["FreeSpace"]);
        }

       
        public double getHDDSize(string drive)
        {
            //check to see if the user provided a drive letter
            //if not default it to "C"
            if (drive == "" || drive == null)
            {
                drive = "C";
            }
            //create our ManagementObject, passing it the drive letter to the
            //DevideID using WQL
            ManagementObject disk = new ManagementObject("Win32_LogicalDisk.DeviceID=\"" + drive + ":\"");
            //bind our management object
            disk.Get();
            //return the HDD's initial size
            return Convert.ToDouble(disk["Size"]);
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
        public int bios_is_cool()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_BIOS");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("BIOS: {0}", queryObj["Caption"]); //Just check if default BIOS 

                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
            return 0;
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
                    break;
                case 21:
                    return "DDR-2";
                    break;
                case 17:
                    return "SDRAM";
                    break; 
                default:
                    if (type == 0 || type > 22)
                        return "DDR-3";
                    else
                        return "Unknown";
            }

        }
    }
}
