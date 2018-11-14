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
    }
}
