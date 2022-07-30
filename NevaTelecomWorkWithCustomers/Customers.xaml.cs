using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
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

namespace NevaTelecomWorkWithCustomers
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : Page
    {
        private string employeeFIO { get; set; }
        public Customers(string employeeFIO)
        {
            InitializeComponent();

            doFilters();
            this.employeeFIO = employeeFIO;
            listBoxEvents.ItemsSource = WorkWithDB.getEventsForFIO(employeeFIO);
        }

        

        private void btnEventsDown_Click(object sender, RoutedEventArgs e)
        {
            scrollViewerEvents.LineDown();
            scrollViewerEvents.LineDown();
            scrollViewerEvents.LineDown();
            
        }

        
        /// <summary>
        /// refresh dataGridCustomers based on Active and NotActive checkBoxes
        /// </summary>
        private void doFilters()
        {
            if (checkBoxShowNotActiveCustomers == null || checkBoxShowActiveCustomers == null)
            {
                return;
            }
            if (checkBoxShowActiveCustomers.IsChecked == true && checkBoxShowNotActiveCustomers.IsChecked == true)
            {
                dataGridCustomers.ItemsSource = WorkWithDB.getCustomers(txtFilterFIO.Text, txtFilterAdress.Text, txtFilterPersonalAccount.Text);
                return;
            }
            if (checkBoxShowActiveCustomers.IsChecked == true)
            {
                dataGridCustomers.ItemsSource = WorkWithDB.getActiveCustomers(txtFilterFIO.Text, txtFilterAdress.Text, txtFilterPersonalAccount.Text);
                return;
            }
            if (checkBoxShowNotActiveCustomers.IsChecked == true)
            {
                dataGridCustomers.ItemsSource = WorkWithDB.getNotActiveCustomers(txtFilterFIO.Text, txtFilterAdress.Text, txtFilterPersonalAccount.Text);
                return;
            }
            if (checkBoxShowActiveCustomers.IsChecked == false && checkBoxShowNotActiveCustomers.IsChecked == false)
            {
                dataGridCustomers.ItemsSource = null;
                return;
            }
        }
        private void filtersChanged(object sender, RoutedEventArgs e)
        {
            doFilters();
        }

        private void txtFilterFIO_TextChanged(object sender, TextChangedEventArgs e)
        {
            doFilters();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
