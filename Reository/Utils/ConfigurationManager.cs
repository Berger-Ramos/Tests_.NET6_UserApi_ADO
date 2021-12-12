using Microsoft.Extensions.Configuration;

namespace Library.Utils
{
    public class ConfigurationManager
    {
        public static IConfiguration Config 
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            }
        } 

        public static string GetConnection()
        {
            string connectionString = Config.GetConnectionString("DataBaseConnectionString");
            return connectionString;
        }

    }
}
