using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterRemastered.Model
{
    public class Currency
    {
        public string currency { get; set; }
        public string code { get; set; }
        public double mid { get; set; }
    }

    class CurrencieName
    {
        public string ShortName { get; set; }
        public string Name { get; set; }

        public CurrencieName(string shortNanme, string name)
        {
            this.ShortName = shortNanme;
            this.Name = name;
        }
    }

}
