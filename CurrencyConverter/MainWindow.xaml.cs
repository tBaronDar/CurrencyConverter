using System;
using System.Collections.Generic;
using System.Data;
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

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindData();
        }

        private void BindData()
        {
            DataTable dtCurrency = new DataTable();
            dtCurrency.Columns.Add("Text");
            dtCurrency.Columns.Add("Value");

            dtCurrency.Rows.Add("--Select--", 0);
            dtCurrency.Rows.Add("INR", 1);
            dtCurrency.Rows.Add("USD", 75);
            dtCurrency.Rows.Add("EUR", 85);
            dtCurrency.Rows.Add("SAR", 87);
            dtCurrency.Rows.Add("POUND", 5);
            dtCurrency.Rows.Add("DEM", 43);

            cmbFromCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "Text";
            cmbFromCurrency.SelectedValuePath = "Value";
            cmbFromCurrency.SelectedIndex = 0;

            cmbToCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text";
            cmbToCurrency.SelectedValuePath = "Value";
            cmbToCurrency.SelectedIndex = 0;
        }

        private void OnConvertClicked(object sender, RoutedEventArgs e)
        {
            double convertedValue;

            if(txtCurrency.Text==null || txtCurrency.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a valid value.", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCurrency.Focus();
                return;
            }
            else if(cmbFromCurrency.SelectedValue==null||cmbFromCurrency.SelectedIndex==0)
             {
                MessageBox.Show("Please select a Currency to convert FROM", "Information",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbFromCurrency.Focus();
                return;
            }
            else if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a Currency to convert TO", "Information",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbToCurrency.Focus();
                return;
            }

            //Calculations
            if (cmbFromCurrency.SelectedValue == cmbToCurrency.SelectedValue) {
                convertedValue = double.Parse(txtCurrency.Text.ToString());
               
            }
            else
            {
                convertedValue = (double.Parse(txtCurrency.Text)*
                    double.Parse(cmbFromCurrency.SelectedValue.ToString()))/
                    double.Parse(cmbToCurrency.SelectedValue.ToString());

            }

            lblOutput.Content= $"{cmbToCurrency.Text} {convertedValue.ToString("N3")}";


        }

        private void OnClearClicked(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            txtCurrency.Text = null;
            if(cmbFromCurrency.Items.Count>0)cmbFromCurrency.SelectedIndex = 0;
            if(cmbToCurrency.Items.Count>0)cmbToCurrency.SelectedIndex = 0;
            lblOutput.Content = null;
            txtCurrency.Focus();
        }

        private void NuberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled=regex.IsMatch(e.Text);

        }
    }
}
