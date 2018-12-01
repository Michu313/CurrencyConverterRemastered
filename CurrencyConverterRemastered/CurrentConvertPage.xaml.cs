using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using CurrencyConverterRemastered.Offline;

namespace CurrencyConverterRemastered
{
    /// <summary>
    /// Interaction logic for CurrentConvert.xaml
    /// </summary>
    public partial class CurrentConvertPage : Page
    {
        CurrentExchangeRates currentExchangeRates;
        bool OfflineMode;
        public CurrentConvertPage()
        {
            InitializeComponent();
            CompletAll();
        }

        private void CompletAll()
        {
            if (Helpers.CheckConnectInternet())
            {
                currentExchangeRates = new CurrentExchangeRates(true);
                labeCourseUsd.Content = "USD = " + Math.Round(currentExchangeRates.GetCourse("USD"), 2) + "zł";
                labeCourseEur.Content = "EUR = " + Math.Round(currentExchangeRates.GetCourse("EUR"), 2) + "zł";
                labeCourseGbp.Content = "GBP = " + Math.Round(currentExchangeRates.GetCourse("GBP"), 2) + "zł";
                labeCourseChf.Content = "CHF = " + Math.Round(currentExchangeRates.GetCourse("CHF"), 2) + "zł";
                Helpers.FillComboBox(currentExchangeRates, ref comboBox1, true);
                Helpers.FillComboBox(currentExchangeRates, ref comboBox2, true);
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 1;
                OfflineDataCurrency.Content = "";
                control.Fill = Brushes.Green;
                ReadAndWriteJson.WriteJson();
                OfflineMode = false;
            }
            else
            {
                currentExchangeRates = new CurrentExchangeRates(false);
                labeCourseUsd.Content = "USD = " + Math.Round(currentExchangeRates.GetCourse("USD"), 2) + "zł";
                labeCourseEur.Content = "EUR = " + Math.Round(currentExchangeRates.GetCourse("EUR"), 2) + "zł";
                labeCourseGbp.Content = "GBP = " + Math.Round(currentExchangeRates.GetCourse("GBP"), 2) + "zł";
                labeCourseChf.Content = "CHF = " + Math.Round(currentExchangeRates.GetCourse("CHF"), 2) + "zł";
                Helpers.FillComboBox(currentExchangeRates, ref comboBox1, true);
                Helpers.FillComboBox(currentExchangeRates, ref comboBox2, true);
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 1;
                OfflineDataCurrency.Content = "Kursy walut z dnia: " + currentExchangeRates.Data;
                control.Fill = Brushes.Red;
                OfflineMode = true;
            }
        }


        private void ConvertOnline()
        {
            if (Helpers.CheckTextBox(textBox1.Text))
            {
                textBox2.Text = "";
                if (comboBox1.Text==comboBox2.Text)
                    textBox2.AppendText(textBox1.Text);
                else
                {
                    if (Helpers.GetShortName(comboBox1.Text) == "PLN")
                    {
                        double a = (1 / currentExchangeRates.GetCourse(Helpers.GetShortName(comboBox2.Text))) * Convert.ToDouble(textBox1.Text);
                        textBox2.AppendText((Math.Round(a, 2)).ToString());
                    }
                    if (Helpers.GetShortName(comboBox2.Text) == "PLN")
                    {
                        double a = ((currentExchangeRates.GetCourse(Helpers.GetShortName(comboBox1.Text))) * (Convert.ToDouble(textBox1.Text)));
                        textBox2.AppendText((Math.Round(a, 2)).ToString());
                    }
                    if (Helpers.GetShortName(comboBox2.Text) != "PLN" && Helpers.GetShortName(comboBox1.Text) != "PLN")
                    {
                        double a = (currentExchangeRates.GetCourse(Helpers.GetShortName(comboBox1.Text))) * (currentExchangeRates.GetCourse(Helpers.GetShortName(comboBox2.Text)));
                        textBox2.AppendText((Math.Round(a, 2)).ToString());
                    }
                }
                
            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                MessageBox.Show("Nie poprawny format ciągu wejściowego. Przykładowe formaty: 100 | 12,23 | 1245,54", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_ClickPrzelicz(object sender, RoutedEventArgs e)
        {
            if (OfflineMode)
                ConvertOnline();
            else
            {
                if (Helpers.CheckConnectInternet())
                    ConvertOnline();
                else
                {
                    Helpers.ErrorInternet();
                    OfflineMode = true;
                    CompletAll();
                }
            }
        }

        private void BStatistic_Click(object sender, RoutedEventArgs e)
        {
            if (Helpers.CheckConnectInternet())
                this.NavigationService.Navigate(new StatisticPage());
            else
            {
                Helpers.ErrorInternet();
                CompletAll();
            }
        }

        private void BRefresh_Click(object sender, RoutedEventArgs e)
        {
            CompletAll();
        }
    }
}
