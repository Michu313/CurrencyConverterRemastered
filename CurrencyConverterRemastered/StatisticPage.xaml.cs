using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyConverterRemastered
{
    /// <summary>
    /// Interaction logic for Statistic.xaml
    /// </summary>
    public partial class StatisticPage : Page
    {
        CurrentExchangeRates currentExchangeRates = new CurrentExchangeRates(true);
        public StatisticPage()
        {
            InitializeComponent();
            GetStatistic("EUR", 40);
            Helpers.FillComboBox(currentExchangeRates, ref comboBox, false);
            comboBox.SelectedIndex = 7;
        }

        void GetStatistic(string name, int quantity)
        {
            listView.Items.Clear();
            CurrencyStatistics currencyStatistics = new CurrencyStatistics(name, quantity);
            currencyStatistics.OneCurrencyForStatistics.Reverse();
            foreach (var item in currencyStatistics.OneCurrencyForStatistics)
            {
                listView.Items.Add(item);
            }
            textBlock11.Text = currencyStatistics.Average().ToString();
            textBlock12.Text = currencyStatistics.HighestValue().ToString();
            textBlock13.Text = currencyStatistics.LowestValue().ToString();
            textBlock14.Text = currencyStatistics.LongestSeriesOfGrowth().ToString() + " dni";
            textBlock15.Text = currencyStatistics.LongestSeriesOfInheritance().ToString() + " dni";
            textBlock16.Text = currencyStatistics.BiggestIncrease().ToString();
            textBlock17.Text = currencyStatistics.BiggestDrop().ToString();
            groupBox.Header = "Statystyki z " + quantity + " dni";
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Helpers.CheckConnectInternet())
            {
                if (Helpers.ValidationTextBoxForStatistic(textBox.Text))
                    GetStatistic(Helpers.GetShortName(comboBox.Text, currentExchangeRates), Int32.Parse(textBox.Text));
                else
                {
                    MessageBox.Show("Pole puste lub złą wprowadzona liczba, możliwe liczby od 10 do 255", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Clear();
                }
            }
            else
            {
                Helpers.ErrorInternet();
                MessageBox.Show("Brak internetu. Nastąpi przejście do głównego okna aplikacji.", "Problemy z internetem", MessageBoxButton.OK, MessageBoxImage.Error);
                this.NavigationService.Navigate(new CurrentConvertPage());
            }
        }

        void Clear()
        {
            textBox.Text = "";
            listView.Items.Clear();
            textBlock11.Text = "";
            textBlock12.Text = "";
            textBlock13.Text = "";
            textBlock14.Text = "";
            textBlock15.Text = "";
            textBlock16.Text = "";
            textBlock17.Text = "";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CurrentConvertPage());
        }
    }
}
