using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Interaction logic for UserRegistrationWindow.xaml
    /// </summary>
    public partial class UserRegistrationWindow : Window
    {
        public UserRegistrationWindow()
        {
            InitializeComponent();
        }

        private void ButtonClickOk(object sender, RoutedEventArgs e)
        {
            int a = 0;

            if (string.IsNullOrWhiteSpace(textBoxFullName.Text))
            {
                MessageBox.Show("Full Name cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textBoxFullName.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBoxLogin.Text))
            {
                MessageBox.Show("Login cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textBoxLogin.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(passwordBoxPassword.Password))
            {
                MessageBox.Show("Enter your password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordBoxPassword.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(passwordBoxRepeatPas.Password))
            {
                MessageBox.Show("Enter your password second time!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordBoxRepeatPas.Focus();
                return;
            }
            else if (passwordBoxPassword.Password != passwordBoxRepeatPas.Password)
            {
                MessageBox.Show("Your password has to be the same in both sections!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordBoxRepeatPas.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(passwordBoxRoomKey.Password))
            {
                MessageBox.Show("You have to enter your room key!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordBoxRepeatPas.Focus();
                return;
            }

            // этот метод надо дописать!!!
        }

        private void ButtonClickCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
