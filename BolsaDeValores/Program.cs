﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BolsaDeValores
{
    public class Program
    {
        public static int currentUser = 1002;

        public static void Main(string[] args)
        {
            //var config = new ConfigurationBuilder()
            //   .SetBasePath(Directory.GetCurrentDirectory())
            //   .AddJsonFile("appsettings.json", optional: false)
            //   .Build();

            //var host = new WebHostBuilder()
            //    .UseKestrel()
            //    .UseConfiguration(config)
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .Build();

            //host.Run();

            CreateWebHostBuilder(args).Build().Run();
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
