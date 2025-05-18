using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Configurations
{
    static class DbConfiguration
    {
        public static string GetConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new ConfigurationManager();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../FinishBlog"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("DefaultConnection");
            }

        }
    }
}
