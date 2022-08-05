using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace NevaTelecomAuthorization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CodeWindow codeWindow;
        DispatcherTimer timer;
        /// <summary>
        /// true if SMS code valid and false if SMS code is expired
        /// </summary>
        bool isCodeExpired = false;
        public MainWindow()
        {
            InitializeComponent();
            txtNumber.Focus();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show(WorkWithDB.GetEmployeePosition(txtNumber.Text));
            
        }

        private void txtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                if (WorkWithDB.IsEmployeeNumberExists(txtNumber.Text))
                {
                    txtPassword.IsEnabled = true;
                    txtPassword.Focus();
                }
                else
                {
                    MessageBox.Show("Номер не найден");
                }
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (WorkWithDB.IsPasswordMatch(txtNumber.Text, txtPassword.Text))
                {
                    OpenCodeWindow();

                }
                else
                {
                    MessageBox.Show("Неверный пароль");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow
            {
                Left = this.Left,
                Top = this.Top
            };
            m.Show();
            
            Close();
            
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(Key.Enter == e.Key)
            {
                if (isCodeExpired)
                {
                    MessageBox.Show("Срок действия кода истек");
                    return;
                }
                if (txtCode.Text == codeWindow.txtCodeFromSMS.Text)
                {
                    btnSignIn.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Неверный код");
                }
            }
        }

        private void btnRefreshCode_Click(object sender, RoutedEventArgs e)
        {
            OpenCodeWindow();
        }
        private void timer_Tick(object sender,EventArgs e)
        {
            timer.Stop();
            btnSignIn.IsEnabled = false;
            isCodeExpired = true;
        }
        public void OpenCodeWindow()
        {
            codeWindow = new CodeWindow() { Left = this.Left + this.Width, Top = this.Top };
            codeWindow.ShowDialog();

            txtCode.IsEnabled = true;
            btnRefreshCode.IsEnabled = true;
            btnSignIn.IsEnabled = false;
            isCodeExpired = false;

            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0,0,10);
            timer.Start();
            
        }
    }
}
