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
        /// <summary>
        /// verifie si un fichier existe
        /// </summary>
        /// <param name="datfile">chemin vers le fichier</param>
        /// <returns>0 ou -1</returns>
        public int check4file(string datfile)
        {   
            if (File.Exists(datfile))
                return 0;
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// verifie si le programme se lance en tant qu'admin
        /// utile pour les infos CPU
        /// </summary>
        /// <returns>new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator)</returns>
        public bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }
        /// <summary>
        /// pour convertir en pourcentage des valeurs pour les espaces des dd
        /// </summary>
        /// <param name="max_size">taille max du disque</param>
        /// <param name="curr_size">espace occupé</param>
        /// <returns>0 ou -1</returns>
        public double convert2percent(double max_size, double curr_size)
        {
            double result = curr_size / max_size;
            double percent = result * 100;
            return percent;
        }
    }
}
