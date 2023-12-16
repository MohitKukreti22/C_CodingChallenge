using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.util
{
    public static class DBUtil
    {
        static IConfiguration _config;

        static DBUtil()
        {
            GetAppSetting();
        }

        private static void GetAppSetting()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("AppSetting.json");
            _config = builder.Build();
        }
        public static string GetConnectionString()
        {
            return _config.GetConnectionString("localConnectionString");
        }
    }
}
