using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Blog.Core.Common.Helper
{
   public class Appsettings
    {
         static IConfiguration Configuration { get; set; }

        public Appsettings(string contentPath)
        {
            string Path = "appsetting.json";

            Configuration = new ConfigurationBuilder()
                .SetBasePath(contentPath)
                .Add(new JsonConfigurationSource {Path = Path, Optional = false, ReloadOnChange = true})
                .Build();
        }

        public static string app(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (Exception) { }
            return "";
        }
    }
}
