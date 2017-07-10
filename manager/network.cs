using System; using System.Collections.Generic; using System.Linq; using System.Management; using System.Net.NetworkInformation; using System.Text; using System.Threading.Tasks;    namespace networkmanager {     public class networkinfo     {         /// <summary>         /// Returns MAC Address from first Network Card in Computer         /// </summary>         /// <returns>MAC Address in string format</returns>         public string FindMACAddress()         {             //create out management class object using the             //Win32_NetworkAdapterConfiguration class to get the attributes             //of the network adapter             ManagementClass mgmt = new ManagementClass("Win32_NetworkAdapterConfiguration");             //create our ManagementObjectCollection to get the attributes with             ManagementObjectCollection objCol = mgmt.GetInstances();             string address = String.Empty;             //loop through all the objects we find             foreach (ManagementObject obj in objCol)             {                 if (address == String.Empty)  // only return MAC Address from first card                 {                     //grab the value from the first network adapter we find                     //you can change the string to an array and get all                     //network adapters found as well                     //check to see if the adapter's IPEnabled                     //equals true                     if ((bool)obj["IPEnabled"] == true)                     {                         address = obj["MacAddress"].ToString();                     }                 }                 //dispose of our object                 obj.Dispose();             }             //replace the ":" with an empty space, this could also             //be removed if you wish             //address = address.Replace(":", "");             //return the mac address             return address;         }
        public void network_disks()
        {
            //Console.WriteLine("test");
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_NetworkConnection");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Win32_NetworkConnection instance");
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Name: {0}", queryObj["Name"]);
                    Console.WriteLine("RemoteName: {0}", queryObj["RemoteName"]);
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }
        }     } } 