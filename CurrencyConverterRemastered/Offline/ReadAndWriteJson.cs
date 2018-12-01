using CurrencyConverterRemastered.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterRemastered.Offline
{
    class ReadAndWriteJson
    {
        public static void WriteJson()
        {
            CurrencyOffline currencyOffline = new CurrencyOffline();
            string json = JsonConvert.SerializeObject(currencyOffline);

            System.IO.File.WriteAllText((AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\", "") + "\\Offline\\CourseOffline.json"), json);
        }

        public static string ReadJson()
        {
            return System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\", "") + "\\Offline\\CourseOffline.json");
        }
    }
}
