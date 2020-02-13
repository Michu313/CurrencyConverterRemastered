using CurrencyConverterRemastered.Interfaces;
using CurrencyConverterRemastered.Model;
using CurrencyConverterRemastered.Offline;
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


        public string Data { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<CurrencieName> CurrencieNames { get; set; }

        public CurrentExchangeRates(bool connection)
        {
            if (connection)
            {
                this.Currencies = GetListCurrency(true);
                this.CurrencieNames = GetListCurrencyNames();
            }
            else
            {
                this.Data = GetDate();
                this.Currencies = GetListCurrency(false);
                this.CurrencieNames = GetListCurrencyNames();
            }
                
        }

        public double GetCourse(string nameCurrency)
        {
            Currency x= Currencies.Find(a=>a.code.Equals(nameCurrency));
            double value = x.mid;
            return value;
        }
        
        private List<CurrencieName> GetListCurrencyNames()
        {
            var list = Currencies;
            List<CurrencieName> list2 = new List<CurrencieName>();
            foreach (var item in list)
            {
                list2.Add(new CurrencieName(item.code, item.currency));
            }
            return list2;
        }

        private List<Currency> GetListCurrency(bool connection)
        {
            string text="";
            if (connection)
            {
                var client = new WebClient
                {
                    Encoding = Encoding.UTF8
                };

                text = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/a/?format=json");
            }
            else
            {
                text = ReadAndWriteJson.ReadJson();
            }
            
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
        
        private string GetDate()
        {
            var text = ReadAndWriteJson.ReadJson();
            text = text.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
            JObject serch = JObject.Parse(text);
            return Helpers.ReversDate(serch["effectiveDate"].ToString());
        }
    }
}
