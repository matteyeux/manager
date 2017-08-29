using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace clientmanager
{
    class networkclient
    {
        /// <summary>
        /// client réseau pour communiquer avec le serveur
        /// </summary>
        /// <param name="ip">ip du serveur</param>
        /// <param name="port">port sur lequel communiquer</param>
        /// <returns>void</returns>
        public void run_client(string ip, int port)
        {
            int bytelen = 256;
            try
            {
                TcpClient tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");

                try
                {
                    tcpclnt.Connect(ip, port);
                    Console.WriteLine("Connected");
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("[ERROR] unable to connect to {0} on port {1}", ip, port);
                    return;
                }

                Stream stm = tcpclnt.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes("hello it's me ~matteyeux\n"); // here is the message to transmit
                Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);
                byte[] bb = new byte[bytelen];

                int k = stm.Read(bb, 0, bytelen);
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(bb[i]));
                tcpclnt.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : {0}", e.StackTrace);
            }
        }
    }
}