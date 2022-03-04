using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace _4kTiles_Frontend.Services.Configuration
{
    public static class ConfigService
    {
        public static IConfiguration Configuration { get; private set; }

        static ConfigService() {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();
        }
    }
}
