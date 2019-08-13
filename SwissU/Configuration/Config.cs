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
        // Gets the username of the current user logged into this local PC
        static string userName = Environment.UserName;

        public static string endpoint = getEndpoint();

        /// <summary>
        /// This will return the endpoint from the textfile on the local computer
        /// </summary>
        public static string getEndpoint()
        {
            string[] lines = File.ReadAllLines(getConfig());

            string[] pathList = lines[0].Split('=');

            string path = string.Format(@"{0}", pathList[1]);

            return path;
        }

        /// <summary>
        /// This will 
        /// </summary>
        public static string getConfig()
        {
            string configPath = string.Format(@"C:\Users\{0}\AppData\Configuration.txt", userName);

            return configPath;
        }

    }
}
