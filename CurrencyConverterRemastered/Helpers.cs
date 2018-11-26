using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

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

        public static string GetImageToList(double a)
        {
            double b = Math.Round(a, 4);
            if (b > 0)
                return "/Image/image1.png";
            if (b == 0)
                return "/Image/image2.png";
            else 
                return "/Image/image3.png";
        }

        public static bool ValidationTextBoxForStatistic(string tmp)
        {
            if (tmp== "")
            {
                return false;
            }
            else
            {
                int a = Int32.Parse(tmp);
                if (a>=10 && a<= 255)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }

        public static void FillComboBox(CurrentExchangeRates currentExchangeRates, ref ComboBox comboBox, bool tmp)
        {
            if (tmp)
            {
                comboBox.Items.Add("PLN");
                foreach (var item in currentExchangeRates.CurrencieNames)
                {
                    comboBox.Items.Add(item.ShortName + " " + item.Name);
                }
            }
            else
            {
                foreach (var item in currentExchangeRates.CurrencieNames)
                {
                    comboBox.Items.Add(item.ShortName + " " + item.Name);
                }
            }
        }

        public static string GetShortName(string comboBoxItem, CurrentExchangeRates currentExchangeRates)
        {
            foreach (var item in currentExchangeRates.CurrencieNames)
            {
                if (comboBoxItem==item.ShortName+" "+item.Name)
                {
                    return item.ShortName;
                }
            }
            return "EUR";
        }

        public static bool CheckConnectInternet()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    using (var stream = client.OpenRead("http://api.nbp.pl"))
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static void ErrorInternet()
        {
            System.Windows.MessageBox.Show("Problem z internetem. Sprawdź połączenie z internetem", "Brak internetu", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }

        public static string ReversDate(string tmp)
        {
            string tmp1 = tmp[0] + "" + tmp[1] + "" + tmp[2] + "" + tmp[3];
            string tmp2 = tmp[5] + "" + tmp[6];
            string tmp3 = tmp[8] + "" + tmp[9];
            return tmp3 + "-" + tmp2 + "-" + tmp1;
        }
    }
}
