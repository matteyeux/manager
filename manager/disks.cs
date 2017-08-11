
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;
using commonmanager;

namespace diskmanager
{
    class disksinfo
    {
        commonstuff common = new commonstuff();
        public int check_if_disk_exists(string currdrive)
        {
            string realdrive = currdrive + ":";
            string drive = Path.GetPathRoot(realdrive);   // e.g. K:\

            if (Directory.Exists(drive))
            {
                return 0;
            }
            else
            {
                return -1;
            }
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
            ManagementObject disk = new ManagementObject("Win32_LogicalDisk.DeviceID =\"" + drive + ":\"");
            //bind our management object
            disk.Get();
            //return the serial number
            return disk["VolumeSerialNumber"].ToString();
        }


        static double byte2gb(double bytesentry)
        {
            double result = bytesentry / 1073741824;
            return result;
        }

        public int storage_drives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                Console.WriteLine("\nDrive:         {0}", d.Name);
                Console.WriteLine("Drive type:    {0}", d.DriveType);
                //Console.WriteLine("Serial Number {0}", GetHDDSerialNumber(d.Name));
                if (d.IsReady == true)
                {
                    Console.WriteLine("Volume label:  {0}", d.VolumeLabel);
                    Console.WriteLine("File system:   {0}", d.DriveFormat);
                    Console.WriteLine("HDD Size:      {0} Go", Math.Round(byte2gb(d.TotalSize), 2));
                    Console.WriteLine("Free Space:    {0} Go  \t{1}%", Math.Round(byte2gb(d.TotalFreeSpace) ,2), Math.Round(common.convert2percent(d.TotalSize, d.TotalFreeSpace), 2));
                    
                }
            }
            return 0;
        }
    }
}
