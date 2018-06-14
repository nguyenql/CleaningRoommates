using Core.Model;
using Core.Repositories_and_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Interaction logic for UserRegistrationWindow.xaml
    /// </summary>
    public partial class UserRegistrationWindow : Window
    {
        private UserRepository user_repo = new UserRepository();
        private RoomRepository room_repo = new RoomRepository();

        public UserRegistrationWindow()
        {
            InitializeComponent();
        }

        private void ButtonClickOk(object sender, RoutedEventArgs e)
        {
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
                passwordBoxRoomKey.Focus();
                return;
            }
            else if (user_repo.Users.Where(u => u.Login == textBoxLogin.Text).FirstOrDefault() != null)
            {
                MessageBox.Show("This login is already used! Think of a new one!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textBoxLogin.Focus();
                return;
            }
            else if (room_repo.Rooms.Where(r => r.Key == passwordBoxRoomKey.Password).FirstOrDefault() == null)
            {
                MessageBox.Show("You have to registrate a room first!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordBoxRoomKey.Focus();
                return;
            }

            var room = room_repo.Rooms.Where(r => r.Key == passwordBoxRoomKey.Password).FirstOrDefault();
            room_repo.Save();
            var peopleInRoom = user_repo.Users.Where(u => u.Room == room).ToList();

            if (peopleInRoom.Count > 3)
            {
                MessageBox.Show("This room is full of people! Please, check the room key!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordBoxRoomKey.Focus();
                return;
            }
            else
            {
                var user = new User()
                {
                    Name = textBoxFullName.Text,
                    Login = textBoxLogin.Text,
                    Password = GetHash(passwordBoxPassword.Password),
                    Room = room
                };
                //user_repo.Users.Add(user);
                user_repo.AddUser(user);
                user_repo.Save();
                DialogResult = true;
                var logInWindow = new MainWindow();
            }
        }

        public string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(
               password));
            return Convert.ToBase64String(hash);
        }

        private void ButtonClickCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
