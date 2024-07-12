using System;
using System.Configuration;
namespace DAL.Framework
{
    public class DBHelper
    {
        private const string Gym_CONNSTRING_KEY = "AppConnectionString";
        public static string GetCreditDBConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[Gym_CONNSTRING_KEY].ConnectionString;
        }
        public static string FormatDate(DateTime txtDate)
        {
            return " CONVERT(DATETIME,'" + txtDate.ToString("dd/MM/yyyy") + "',103)";
        }
    }
}