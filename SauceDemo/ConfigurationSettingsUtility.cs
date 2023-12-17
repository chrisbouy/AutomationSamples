using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace SauceDemo
{
    public static class ConfigurationSettingsUtility
    {
        private static IConfiguration Configuration => InitConfiguration();
        public static string BaseUrl => Configuration["BaseUrl"];
        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
    }
}
