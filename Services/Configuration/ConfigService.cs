using System;

using Microsoft.Extensions.Configuration;

namespace _4kTiles_Frontend.Services.Configuration
{
    public static class ConfigService
    {
        public static IConfiguration Configuration { get; private set; }

        static ConfigService()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();
        }
    }
}
