using System;
using System.Management;
using System.Management.Instrumentation;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Xml;
/* local stuff */
using cpumanager;
using hardwaremanager;
using networkmanager;
using systemmanager;
using xmlmanager;
using diskmanager;
using commonmanager;
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
            //xmlstuff.getallsoft("xml.xml");

            Console.WriteLine("=== HARDWARE ===");
            if (instance.check_for_xiring_device() == true)
            {
                Console.WriteLine("XIRING plugged");
            }
            else
            {
                Console.WriteLine("XIRING not plugged");
            }
            Console.WriteLine("RAM type : {0} ", instance.ram_Type());
            instance.fan_stuff();
            instance.partition_number();
            instance.all_ram_info();
            Console.Write("\n");
             
            Console.WriteLine("=== NETWORK ===");
            netfuncts.get_ip_addr();
            Console.WriteLine("Ethernet MAC address : " + netfuncts.FindMACAddress());
            Console.Write("\n");

            Console.WriteLine("=== SYSTEM ===");
            sys.osinfo();
            sys.boot_conf();
            sys.windows_info();
            sys.bios_is_cool();
            sys.windows_info();

            Console.WriteLine("=== DISKS ===");
            disks.storage_drives();

            Console.Write("\n");

            Console.WriteLine("=== CPU ===");
            cpufuncts.GetCPUId();
            Console.WriteLine("CPU Clock speed : " + cpufuncts.GetCPUCurrentClockSpeed());
            if (common.IsAdministrator() == true)
            {
                cpufuncts.more_cpu_info();
            }
            else
            {
                Console.WriteLine("Can't get CPU, you're not admin");
            }
            Console.ReadLine();
            return 0;
        }
    }
}

