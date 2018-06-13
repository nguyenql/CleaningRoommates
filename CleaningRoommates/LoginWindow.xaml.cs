using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CleaningRoommates
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ButtonClickOk(object sender, RoutedEventArgs e)
        {
            int a = 0;

            if (string.IsNullOrWhiteSpace(textBoxLogin.Text))
            {
                MessageBox.Show("Login cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textBoxLogin.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                MessageBox.Show("Enter your password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordBox.Focus();
                return;
            }
            else
            {
                using (var context = new Context())
                {
                    foreach (var user in context.Users)
                    {
                        if ((user.Login == textBoxLogin.Text) && (user.Password == GetHash(passwordBox.Password)))
                        {
                            a = 1;
                            ScheduleWindow window = new ScheduleWindow();
                            window.ShowDialog();
                            this.Close();
                        }
                    }
                }
            }

            if (a == 0)
            {
                MessageBox.Show("You entered something wrong! Please, check the data or registrate! ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void ButtonClickCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(
               password));
            return Convert.ToBase64String(hash);
        }

        private void textBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
