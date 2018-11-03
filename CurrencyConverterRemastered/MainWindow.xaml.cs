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

namespace CurrencyConverterRemastered
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CurrentExchangeRates currentExchangeRates = new CurrentExchangeRates();

        public MainWindow()
        {
            InitializeComponent();
            CompletAll();
        }

        private void CompletAll()
        {
            labeCourseUsd.Content = labeCourseUsd.Content + "" + Math.Round(currentExchangeRates.GetCourse("USD"),2) + "zł";
            labeCourseEur.Content = labeCourseEur.Content + "" + Math.Round(currentExchangeRates.GetCourse("EUR"),2) + "zł";
            labeCourseGbp.Content = labeCourseGbp.Content + "" + Math.Round(currentExchangeRates.GetCourse("GBP"),2) + "zł";
            labeCourseChf.Content = labeCourseChf.Content + "" + Math.Round(currentExchangeRates.GetCourse("CHF"),2) + "zł";
            FillComboBox();
        }

        private void FillComboBox()
        {
            comboBox1.Items.Add("PLN");
            comboBox2.Items.Add("PLN");
            foreach (var item in currentExchangeRates.CurrencieNames)
            {
                comboBox1.Items.Add(item.ShortName + " " + item.Name);
                comboBox2.Items.Add(item.ShortName + " " + item.Name);
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
        }

        private void ConvertOnline()
        {
            if (Helpers.CheckTextBox(textBox1.Text))
            {
                textBox2.Text = "";
                if (Helpers.GetShortName(comboBox1.Text)=="PLN")
                {
                    double a = (1 / currentExchangeRates.GetCourse(Helpers.GetShortName(comboBox2.Text))) * Convert.ToDouble(textBox1.Text);
                    textBox2.AppendText((Math.Round(a, 2)).ToString());
                }
                if (Helpers.GetShortName(comboBox2.Text) == "PLN")
                {
                    double a = ((currentExchangeRates.GetCourse(Helpers.GetShortName(comboBox1.Text)))*(Convert.ToDouble(textBox1.Text)));
                    textBox2.AppendText((Math.Round(a, 2)).ToString());
                }
                if(Helpers.GetShortName(comboBox2.Text) != "PLN" && Helpers.GetShortName(comboBox1.Text) != "PLN")
                {
                    double a = (currentExchangeRates.GetCourse(Helpers.GetShortName(comboBox1.Text))) * (currentExchangeRates.GetCourse(Helpers.GetShortName(comboBox2.Text)));
                    textBox2.AppendText((Math.Round(a, 2)).ToString());
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
            ConvertOnline();
        }
    }
}
