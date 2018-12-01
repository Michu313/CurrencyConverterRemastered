using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterRemastered.Model
{
    class CurrencyOffline
    {
        public string effectiveDate { get; set; }
        public List<Currency> rates { get; set; }

        public CurrencyOffline()
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            var text = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/a/?format=json");
            text = text.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
            JObject serch = JObject.Parse(text);
            this.effectiveDate = serch["effectiveDate"].ToString();
            IList<JToken> results = serch["rates"].Children().ToList();

            List<Currency> searchResults = new List<Currency>();
            foreach (var result in results)
            {
                Currency searchResult = result.ToObject<Currency>();
                searchResults.Add(searchResult);
            }
            this.rates = searchResults;
        }

    }
}
