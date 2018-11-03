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
    class CurrentExchangeRates : ICourse
    {
        private bool Internetstatus { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<CurrencieName> CurrencieNames { get; set; }

        public CurrentExchangeRates()
        {
            this.Currencies = GetListCurrency();
            this.CurrencieNames = GetListCurrencyNames();
        }

        public double GetCourse(string nameCurrency)
        {
            Currency x= Currencies.Find(a=>a.code.Equals(nameCurrency));
            double value = x.mid;
            return value;
        }
        
        private List<CurrencieName> GetListCurrencyNames()
        {
            var list = GetListCurrency();
            List<CurrencieName> list2 = new List<CurrencieName>();
            foreach (var item in list)
            {
                list2.Add(new CurrencieName(item.code, item.currency));
            }
            return list2;
        }

        private List<Currency> GetListCurrency()
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            var text = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/a/?format=json");
            text = text.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
            JObject serch = JObject.Parse(text);
            IList<JToken> results = serch["rates"].Children().ToList();
            
            List<Currency> searchResults = new List<Currency>();
            foreach (var result in results)
            {
                Currency searchResult = result.ToObject<Currency>();
                searchResults.Add(searchResult);
            }
            return searchResults;
        }
    }
}
