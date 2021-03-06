﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace networkmanager
{
    public class networkinfo
    {
        /// <summary>
        /// recup de l'addr MAC de la carte ethernet
        /// </summary>
        /// <returns>address</returns>
        public string FindMACAddress()
        {
            ManagementClass mgmt = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objCol = mgmt.GetInstances();
            string address = String.Empty;
            foreach (ManagementObject obj in objCol)
            {
                if (address == String.Empty)
                {
                    if ((bool)obj["IPEnabled"] == true)
                        address = obj["MacAddress"].ToString();
                }

                obj.Dispose();
            }
            return address;
        }
        /// <summary>
        /// recuperation des addresses IP locales (IPV4 et IPv6)
        /// </summary>
        /// <returns>void</returns>
        public void get_ip_addr()
        {
            String strHostName = string.Empty;
            strHostName = Dns.GetHostName();
            Console.WriteLine("hostname: {0}", strHostName);
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            for (int i = 0; i < addr.Length; i++)
            {
                string thisip = addr[i].ToString();
                if (thisip[0] == 'f') // I assume this a local IPv6 addr
                {
                    Console.WriteLine("IPv6 Address : {1} ", i, addr[i].ToString());
                }
                else
                {
                    Console.WriteLine("IPv4 Address : {1} ", i, addr[i].ToString());
                }
            }
        }
    }
}