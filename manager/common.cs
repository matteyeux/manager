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
        /// <returns>(new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator)</returns>
        public bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
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
            return Math.Round(percent, 2);
        }

        /// <summary>
        /// fonction pour convertir les bytes en gigabytes (=octets)
        /// </summary>
        /// <param name="total">taille totale de l'entier (=0) si y'en pas</param>
        /// <param name="val">la valeur que vous souhaitez convertir</param>
        /// <returns>total ou newval</returns>
        public double convertb2gb(double total, double val)
        {   double newval;
            if (total < 0) {
                Console.WriteLine("[ERROR] total is value is : {0}", total);
                return total;
            } else if (total == 0) {
                newval = val / Math.Pow(2, 30);
                return Math.Round(newval, 2);
            } else {
                newval = total / Math.Pow(2, 30) - val / Math.Pow(2, 30);
                return Math.Round(newval, 2);
            }
        }
    }
}
