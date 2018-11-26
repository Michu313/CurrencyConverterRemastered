using CurrencyConverterRemastered.Interfaces;
using CurrencyConverterRemastered.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterRemastered
{
    class CurrencyStatistics : IStatistics
    {
        public List<OneCurrency> OneCurrencies { get; private set; }
        public List<OneCurrencyForStatistics> OneCurrencyForStatistics { get; private set; }


        public CurrencyStatistics(string name, int quantity)
        {
            this.OneCurrencyForStatistics = GetOneCurrencyForStatistics(GetListOneCurrencies(name, quantity), name);
        }

        public double Average()
        {
            double tmp = 0;
            int i = 0;
            foreach (var item in OneCurrencyForStatistics)
            {
                tmp += item.Price;
                i++;
            }
            tmp = tmp / i;
            return Math.Round(tmp, 4);
        }

        public double HighestValue()
        {
            double tmp = 0;
            foreach (var item in OneCurrencyForStatistics)
            {
                if (item.Price>tmp)
                {
                    tmp = item.Price;
                }
            }
            return Math.Round(tmp, 4);
        }

        public double LowestValue()
        {
            double tmp = 1000;
            foreach (var item in OneCurrencyForStatistics)
            {
                if (item.Price < tmp)
                {
                    tmp = item.Price;
                }
            }
            return Math.Round(tmp, 4);
        }

        public int LongestSeriesOfGrowth()
        {
            int i = 0;
            List<int> list = new List<int>();
            foreach (var item in OneCurrencyForStatistics)
            {
                if (item.Change > 0)
                    i++;
                else
                {
                    list.Add(i);
                    i = 0;
                }
            }
            i = 0;
            foreach (var item in list)
            {
                if (i < item)
                    i = item;
            }
            return i;
        }

        public int LongestSeriesOfInheritance()
        {
            int i = 0;
            List<int> list = new List<int>();
            foreach (var item in OneCurrencyForStatistics)
            {
                if (item.Change < 0)
                    i++;
                else
                {
                    list.Add(i);
                    i = 0;
                }
            }
            i = 0;
            foreach (var item in list)
            {
                if (i < item)
                    i = item;
            }
            return i;
        }

        public double BiggestDrop()
        {
            double tmp = 1000;
            foreach (var item in OneCurrencyForStatistics)
            {
                if (item.Change < tmp)
                {
                    tmp = item.Change;
                }
            }
            return Math.Round(tmp, 4);
        }

        public double BiggestIncrease()
        {
            double tmp = 0;
            foreach (var item in OneCurrencyForStatistics)
            {
                if (item.Change > tmp)
                {
                    tmp = item.Change;
                }
            }
            return Math.Round(tmp, 4);
        }


        private List<OneCurrency> GetListOneCurrencies(string name, int quantity)
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            var text = client.DownloadString("http://api.nbp.pl/api/exchangerates/rates/a/"+name.ToLower() +"/last/"+quantity+"/?format=json");
            text = text.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
            JObject serch = JObject.Parse(text);
            IList<JToken> results = serch["rates"].Children().ToList();

            List<OneCurrency> searchResults = new List<OneCurrency>();
            foreach (var result in results)
            {
                OneCurrency searchResult = result.ToObject<OneCurrency>();
                searchResults.Add(searchResult);
            }
            return searchResults;
        }

        private List<OneCurrencyForStatistics> GetOneCurrencyForStatistics(List<OneCurrency> oneCurrencies, string name)
        {
            var list = new List<OneCurrencyForStatistics>();
            bool firstItem = true;
            double tmp = 0;
            foreach (var item in oneCurrencies)
            {
                if (firstItem!=true)
                {
                    list.Add(new Model.OneCurrencyForStatistics(name, Helpers.ReversDate(item.effectiveDate), Math.Round(item.mid,4), Math.Round(item.mid-tmp, 4), Helpers.GetImageToList(item.mid-tmp)));
                    tmp = Math.Round(item.mid, 4);
                }
                else
                {
                    list.Add(new Model.OneCurrencyForStatistics(name, Helpers.ReversDate(item.effectiveDate), Math.Round(item.mid, 4), 0, "/Image/image2.png"));
                    tmp = Math.Round(item.mid, 4);
                    firstItem = false;
                }
            }
            return list;
        }

    }
}
