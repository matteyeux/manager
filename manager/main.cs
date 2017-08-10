﻿using System;
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
using diskmanager;
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
            disksinfo disks = new disksinfo(); 
            xmlparser xmlstuff = new xmlparser();

            /*Auto check disks */
            string[] disklist = new string[] {
            "A",  "B",  "C", "D",  "E",  "F",  "G",
            "H",  "I", "J",  "K",  "L",  "M",  "N",
            "O",  "P",  "Q",  "R",  "S", "T",  "U",
            "V",  "W",  "X",  "Y",  "Z" }; // help to improve it
            //string[] disks = new string[10];
            //int i = 0;

            Console.WriteLine("=== HARDWARE ===");
            Console.WriteLine("RAM type : {0} ", instance.ram_Type());
            instance.fan_stuff();
            instance.partition_number();
            instance.all_ram_info();
            Console.Write("\n");

            /* Network stuff */
            Console.WriteLine("=== NETWORK ===");
            Console.WriteLine("Ethernet MAC address : " + netfuncts.FindMACAddress());
            Console.Write("\n");

            Console.WriteLine("=== SYSTEM ===");
            sys.osinfo();
            sys.boot_conf();
            sys.windows_info();
            sys.bios_is_cool();
            sys.windows_info();
            Console.ReadLine();

            Console.WriteLine("=== DISKS ===");
            for (int i = 0; i < disklist.Length; i++)
            {
                if (disks.check_if_disk_exists(disklist[i]) == 0)
                {
                    Console.WriteLine("Disk {0} : ", disklist[i]);
                    Console.WriteLine("Serial Number : " + disks.GetHDDSerialNumber(disklist[i]));
                    Console.WriteLine("Free Space : " + disks.GetHDDFreeSpace(disklist[i]) + " bits");
                    Console.WriteLine("HDD Size : " + disks.getHDDSize(disklist[i]) + " bits");
                    Debug.WriteLine(i);
                } else
                {
                    Debug.WriteLine("{0} not found", disklist[i]);  
                }
                
            }
            Console.ReadLine();
            Console.Write("\n");
            //netfuncts.network_disks(); // marche pas

            Console.WriteLine("=== CPU ===");
            cpufuncts.GetCPUId();
            Console.WriteLine("CPU Clock speed : " + cpufuncts.GetCPUCurrentClockSpeed());
            if (common.IsAdministrator() == true)
            {
                cpufuncts.more_cpu_info();
            } else
            {
                Console.WriteLine("Can't get CPU, you're not admin");
            }
            Console.ReadLine();
            return 0;
        }
    }
}

