using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CurrencyConverterRemastered
{
    class Helpers
    {
        public static string GetShortName(string a)
        {
            return ""+a[0] + a[1] + a[2];
        }

        public static bool CheckTextBox(string a)
        {
            Regex regex = new Regex("^(-?)(0|([1-9][0-9]*))(\\,[0-9]+)?$");
            if (regex.IsMatch(a) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
