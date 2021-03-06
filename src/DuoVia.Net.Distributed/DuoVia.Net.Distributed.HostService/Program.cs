﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace DuoVia.Net.Distributed.HostService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //if not configured with <add key="RunAsConsole" value="false"/>, we will run as console
            bool runAsConsole = !string.Equals(ConfigurationManager.AppSettings["RunAsConsole"], "false", StringComparison.InvariantCultureIgnoreCase);
            if (runAsConsole)
            {
                var sr = new ServiceRunner();
                sr.Start(args);
                Console.WriteLine("DuoVia.Net.Distributed.ServiceHost console started... Hit enter to stop...");
                Console.ReadLine();
                sr.Stop();
            }
            else
            {
                //Run it as a service
                ServiceBase[] servicesToRun;
                try
                {
                    servicesToRun = new ServiceBase[] { new DistService()  };
                    ServiceBase.Run(servicesToRun);
                }
                catch (Exception e)
                {
                    //do your exception handling thing
                    e.ProcessUnhandledException("DuoVia.Net.Distributed.ServiceHost");
                }
            }
        }
    }
}
