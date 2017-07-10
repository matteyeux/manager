using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace systemmanager
{
    class systeminfo
    {
        public void sysname()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_DesktopMonitor WHERE SystemName = 'W0191454'");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Win32_DesktopMonitor instance");
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("SystemName: {0}", queryObj["SystemName"]);
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
        }

    }
}
