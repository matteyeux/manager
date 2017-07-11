using System;
using System.Management;
using System.Management.Instrumentation;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.IO;
/* local stuff */
using cpumanager;
using hardwaremanager;
using networkmanager;
using systemmanager;
using xmlmanager; 
/* end local stuff */
namespace manager
{
    class Class1
    {
        [STAThread]
        public static int Main(string[] args)
        {   

            hardwareinfo instance = new hardwareinfo();
            networkinfo netfuncts = new networkinfo();
            systeminfo sys = new systeminfo();
            cpu cpufuncts = new cpu();
            xmlparser xmlstuff = new xmlparser();

            //instance.GetHDDSerialNumber("");
            //test comment
            //instance.voltage_info();
            //Console.ReadLine();
            /*Auto check disks */
            //string[] disks = new string[] { "C", "E" }; // only local HDD hardcoded is not the best way
            string[] disks = new string[10];
            //int i = 0;
            netfuncts.network_disks(); // marche pas
            instance.partition_number();
            instance.all_ram_info();
            Console.WriteLine("RAM type : {0} ", instance.ram_Type());
            sys.sysname();
            instance.bios_is_cool();
            /*foreach (var drive in DriveInfo.GetDrives())
                { 
                   double freeSpace = drive.TotalFreeSpace;
                   double totalSpace = drive.TotalSize;
                   double percentFree = (freeSpace / totalSpace) * 100;
                   float num = (float)percentFree;

                   Console.WriteLine("Drive:{0} With  % free", drive.Name);
                   disks[i] = drive.Name;
                   Console.WriteLine("{0} {1}", i, disks[i]);
                   i = i + 1;
                   for (i = 0; i < disks.Length; i++)
                   {
                       Console.WriteLine("Serial Number : " + instance.GetHDDSerialNumber(disks[i]));
                   }
                   string drivename = drive.Name;
                   Console.WriteLine("Disk " + disks[i]);
                   Console.WriteLine("Serial Number : "+  instance.GetHDDSerialNumber(disks[i]));
                   Console.WriteLine("Free Space : " + instance.GetHDDFreeSpace(disks[i]) + " bits");
                   Console.WriteLine("HDD Size : {0}" + instance.getHDDSize(drivename)+ "bits");
                   Console.Write("\n");
                   Debug.WriteLine(i);

                       Console.WriteLine("Space Remaining:{0}", drive.AvailableFreeSpace);
                       Console.WriteLine("Percent Free Space:{0}", percentFree);
                       Console.WriteLine("Space used:{0}", drive.TotalSize);
                       Console.WriteLine("Type: {0}", drive.DriveType);

                }


                for (int i = 0; i < disks.Length; i++)
                {   
                   Console.WriteLine("Disk "+disks[i]);
                   Console.WriteLine("Serial Number : "+  instance.GetHDDSerialNumber(disks[i]));
                   Console.WriteLine("Free Space : " + instance.GetHDDFreeSpace(disks[i]) + " bits");
                   Console.WriteLine("HDD Size : " + instance.getHDDSize(disks[i])+ "bits");
                   Console.Write("\n");
                   Debug.WriteLine(i);
                }*/
            
            /* Network stuff */
            //broken ATM

            //Console.WriteLine("Ethernet MAC address : " + netfuncts.FindMACAddress());
            //Console.Write("\n");


            /* CPU stuff */
            cpufuncts.GetCPUId();
            //Console.WriteLine("CPU Clock speed : " + cpufuncts.GetCPUCurrentClockSpeed());
            //Console.ReadLine();
            //make sure to run is as admin or it won't work
            cpufuncts.more_cpu_info();
            Console.ReadLine();
            return 0;
        }
    }
}

