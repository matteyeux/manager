using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Principal;

namespace commonmanager
{
    class commonstuff
    {
        public int check4file(string datfile)
        {   
            if (File.Exists(datfile))
                return 0;
            else
            {
                return -1;
            }
            //Console.WriteLine(File.Exists(datfile) ? "File exists." : "File does not exist.");
        }
        public bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }

    }
}
