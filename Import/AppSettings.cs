using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Import
{
    public class AppSettings
    {
        public string Conn { get; set; }
        public string ImportFile { get; set; }
        public int Lowerbound { get; set; }
        public int Upperbound { get; set; }
        public int PlatformLine { get; set; }
        public int PlatformStartColumn { get; set; }
        public List<int> Category { get; set; }
        public List<int> SubCategory { get; set; }

        public AppSettings()
        {
            // nuget:
            // Microsoft.Extensions.Configuration.Json
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            Conn = config["conn"];
            ImportFile = config["import_file"];
            Lowerbound = Convert.ToInt32(config["lowerbound"]);
            Upperbound = Convert.ToInt32(config["upperbound"]);
            PlatformLine = Convert.ToInt32(config["platform_line"]);
            PlatformStartColumn = Convert.ToInt32(config["platform_start_column"]);

            // nuget:
            // Microsoft.Extensions.Configuration
            // Microsoft.Extensions.Configuration.Binder
            Category = config.GetSection("category").Get<List<int>>();
            SubCategory = config.GetSection("sub_category").Get<List<int>>();
        }
    }
}
