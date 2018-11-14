using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterRemastered.Model
{
    class OneCurrencyForStatistics
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public double Price { get; set; }
        public double Change { get; set; }
        public string ChangeImage { get; set; }

        public OneCurrencyForStatistics(string name, string date, double price, double change, string changeImage)
        {
            this.Name = name;
            this.Date = date;
            this.Price = price;
            this.Change = change;
            this.ChangeImage = changeImage;
        }
    }
}
