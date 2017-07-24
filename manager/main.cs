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
using commonmanager;
using System.Security.Principal;
using System.Threading;
/* end local stuff */
namespace manager
{
    class Class1
    {
        [STAThread]
        public static int Main(string[] args)
        {
            commonstuff common = new commonstuff();
            hardwareinfo instance = new hardwareinfo();
            networkinfo netfuncts = new networkinfo();
            systeminfo sys = new systeminfo();
            cpu cpufuncts = new cpu();
            xmlparser xmlstuff = new xmlparser();

            //instance.GetHDDSerialNumber("");
            instance.voltage_info();
            //Console.ReadLine();
            /*Auto check disks */
            string[] disks = new string[] { "C", "E" }; // only local HDD hardcoded is not the best way
            //string[] disks = new string[10];
            //int i = 0;
            netfuncts.network_disks(); // marche pas
            instance.partition_number();
            instance.all_ram_info();
            Console.WriteLine("RAM type : {0} ", instance.ram_Type());
            sys.sysname();
            instance.bios_is_cool();

            for (int i = 0; i < disks.Length; i++)
            {
                Console.WriteLine("Disk "+disks[i]);
                Console.WriteLine("Serial Number : "+  instance.GetHDDSerialNumber(disks[i]));
                Console.WriteLine("Free Space : " + instance.GetHDDFreeSpace(disks[i]) + " bits");
                Console.WriteLine("HDD Size : " + instance.getHDDSize(disks[i])+ " bits");
                Console.Write("\n");
                Debug.WriteLine(i);
            }
            
            /* Network stuff */
            //broken ATM
            Console.WriteLine("Ethernet MAC address : " + netfuncts.FindMACAddress());

            /* CPU stuff */
            cpufuncts.GetCPUId();
            //Console.WriteLine("CPU Clock speed : " + cpufuncts.GetCPUCurrentClockSpeed());
            //Console.ReadLine();
            // added check instead of shitty comment

            if (common.IsAdministrator() == true)
            {
                Console.WriteLine("hi");
                //for (int i = 0; i < 1000; i++)
                //{
                   // cpufuncts.more_cpu_info();
                  //  Thread.Sleep(5000);
                //}
            } else
            {
                Console.WriteLine("Can't get CPU, you're not admin");
            }
            Console.ReadLine();
            return 0;
        }
    }
}

