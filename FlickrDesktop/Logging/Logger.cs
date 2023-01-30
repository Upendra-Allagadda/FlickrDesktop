using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrDesktop.Logging
{
    public class Logger
    {
        private static Logger instance;
        private static object lockObj;
        private string logFile; //logfile location
        public static Logger Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null) instance = new Logger();
                    return instance;
                }                
            }
        }

        private Logger()
        {
        }

        /// <summary>
        /// need to initialize logfilename
        /// </summary>
        /// <param name="message"></param>
        public void Log(string[] messages)
        {
            if(!File.Exists(logFile))  File.Create(logFile);
            //string message = string.Format("{0}| log message {1}",DateTime.Now,messages); Format the log message
            File.AppendAllLines(logFile, messages);
        }
    }
}
