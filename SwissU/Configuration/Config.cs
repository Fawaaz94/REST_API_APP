using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissU.Configuration
{
    static class Config
    {
        // Gets the Endpoint from a Config File
        static string userName = Environment.UserName;

        public static string endpoint = getEndpoint();
        public static string ticket = Authentication();


        public static string getEndpoint()
        {
            string[] lines = File.ReadAllLines(getConfig());

            string[] pathList = lines[0].Split('=');

            string path = string.Format(@"{0}", pathList[1]);

            return path;
        }

        public static string getConfig()
        {
            string configPath = string.Format(@"C:\Users\{0}\AppData\Configuration.txt", userName);

            return configPath;
        }

        public static string Authentication()
        {
            string stringTicket = string.Empty;


            return stringTicket;
        }

    }
}
