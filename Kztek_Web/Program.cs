﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kztek_Data;
using Kztek_Model.Models;
using Kztek_Security;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Kztek_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var req = LicenseGenerator.CreateLicenseRequest("", "0cb9a862-5acc-4bfc-aa8b-ab2acc71c897");
            var reqstr = LicenseGenerator.CreateUserCode(req);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


    }
}
