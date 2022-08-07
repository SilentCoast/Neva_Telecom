using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string currentPage;
        public string CurrentPage
        {
            get => currentPage; set
            {
                currentPage = value;
                OnProperyChanged();
            }
        }
        

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            comboBoxUsers.ItemsSource = WorkWithDB.GetEmployeesFIOs();
            btnCustomers.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));

        }
       



        /**************************************************/
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnProperyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        /**************************************************/

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            //CurrentPage = "Абоненты";
            var customers = new Customers(comboBoxUsers.SelectedItem.ToString());
            DataContext = customers;
            mainFrame.Navigate(customers);
        }

        private void btnManagingDevices_Click(object sender, RoutedEventArgs e)
        {
           // CurrentPage = "Управление оборудованием";
        }

        private void btnActives_Click(object sender, RoutedEventArgs e)
        {
            //CurrentPage = "Активы";
        }

        private void btnBilling_Click(object sender, RoutedEventArgs e)
        {
           // CurrentPage = "Биллинг";
        }

        private void btnCustomersSupport_Click(object sender, RoutedEventArgs e)
        {
           // CurrentPage = "Поддержка пользователей";
        }

        private void btnCRM_Click(object sender, RoutedEventArgs e)
        {
           // CurrentPage = "CRM";
            var crm = new CRM();
            DataContext = crm;
            mainFrame.Navigate(crm);
        }

        /// <summary>
        /// sets available modules
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCustomers.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            var modules = WorkWithDB.GetAvailableModulesForFIO(comboBoxUsers.SelectedItem.ToString());
            //set all disabled
            btnCustomers.IsEnabled = false;
            btnCustomersSupport.IsEnabled = false;
            btnCRM.IsEnabled = false;
            btnBilling.IsEnabled = false;
            btnManagingDevices.IsEnabled = false;
            btnActives.IsEnabled = false;
            //set enabled only available for current user
            foreach (var i in modules)
            {
                switch (i)
                {
                    case "Абоненты":
                        btnCustomers.IsEnabled = true;
                        break;
                    case "Управление оборудованием":
                        btnManagingDevices.IsEnabled= true;
                        break;
                    case "Активы":
                        btnActives.IsEnabled= true;
                        break;
                    case "Биллинг":
                        btnBilling.IsEnabled = true;
                        break;
                    case "Поддержка пользователей":
                        btnCustomersSupport.IsEnabled = true;
                        break;
                    case "CRM":
                        btnCRM.IsEnabled = true;
                        break;
                }
            }
            
        }

        
    }
}
