﻿
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

        /// <summary>
        /// méthode pour vérifier si un disque existe
        /// </summary>
        /// <param name="currdrive">Lettre du dique</param>
        /// <returns>0 ou -1 </returns>
        public int check_if_disk_exists(string currdrive)
        {
            string realdrive = currdrive + ":";
            string drive = Path.GetPathRoot(realdrive);

            if (Directory.Exists(drive))
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// méthode lister les partitions d'un dique
        /// </summary>
        /// <param name="currdrive">Lettre du dique</param>
        /// <returns>0 ou -1</returns>
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
        /// methode pour recuperer le SN
        /// </summary>
        /// <param name="drive">Lettre du disque dont on veut récuperer le SN</param>
        /// <returns>Le SN du disque</returns>
        static string GetHDDSerialNumber(string drive)
        {
            ManagementObject disk = new ManagementObject("Win32_LogicalDisk.DeviceID=\"" + drive[0] + ":\"");
            disk.Get();
            // j'ajoute un check, si ça retourne null c'est un lecteur de CD-ROM
            if (disk["VolumeSerialNumber"] == null)
            {
                return null;
            }
            return disk["VolumeSerialNumber"].ToString();
        }

        /// <summary>
        /// permet de convertir en octets -> GO (ou GB en anglais)
        /// </summary>
        /// <param name="bytesentry">valeur en octets (bytes)</param>
        /// <returns>Le SN du disque</returns>
        static double byte2gb(double bytesentry)
        {
            double result = bytesentry / 1073741824;
            return result;
        }

        /// <summary>
        /// Permet de recuperer dynamiquement les disques + les infos de ceux-ci
        /// </summary>
        /// <returns>0</returns>
        public int storage_drives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                Console.WriteLine("\nDrive:         {0}", d.Name);
                Console.WriteLine("Drive type:    {0}", d.DriveType);
                if (GetHDDSerialNumber(d.Name) != null)
                {
                    Console.WriteLine("Serial Number: {0}", GetHDDSerialNumber(d.Name));
                }
                if (d.IsReady == true)
                {
                    Console.Write("Volume label:  {0}", d.VolumeLabel);
                    if (d.VolumeLabel == "OSDisk")
                        Console.WriteLine("\t temperature : {0}°C", gethhdtemp());
                    Console.WriteLine("File system:   {0}", d.DriveFormat);
                    Console.WriteLine("HDD Size:      {0} Go", Math.Round(byte2gb(d.TotalSize), 2));
                    Console.WriteLine("Free Space:    {0} Go  \t{1}%", Math.Round(byte2gb(d.TotalFreeSpace) ,2), Math.Round(common.convert2percent(d.TotalSize, d.TotalFreeSpace), 2));
                }
            }
            return 0;
        }

        public string gethhdtemp()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSStorageDriver_ATAPISmartData");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                if (queryObj["VendorSpecific"] != null)
                {
                    byte[] arrVendorSpecific = (byte[])(queryObj["VendorSpecific"]);
                    string temp = arrVendorSpecific[115].ToString();
                    return temp;
                }
            }
            return null;
        }
    }
}
