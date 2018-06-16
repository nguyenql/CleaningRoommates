using Core.Model;
using Core.Repositories_and_Interface;
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
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        public UserRepository user_repo { get; set; }
        public RoomRepository room_repo { get; set; }
        public User user { get; set; }

        public ProfileWindow(User us, UserRepository us_repo, RoomRepository r)
        {
            InitializeComponent();

            user_repo = us_repo;
            room_repo = r;
            user = us;
            textBoxRoomNumber.Text = user.Room.Number.ToString();
            textBoxFullName.Text = user.Name;
            textBoxLogin.Text = user.Login;
        }

        public string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(
               password));
            return Convert.ToBase64String(hash);
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
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
            else if (string.IsNullOrWhiteSpace(passBoxPassword.Password))
            {
                MessageBox.Show("Enter your password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passBoxPassword.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(passBoxRepeatPassword.Password))
            {
                MessageBox.Show("Enter your password second time!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passBoxRepeatPassword.Focus();
                return;
            }
            else if (passBoxPassword.Password != passBoxRepeatPassword.Password)
            {
                MessageBox.Show("Your password has to be the same in both sections!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passBoxRepeatPassword.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(passBoxRoomKey.Password))
            {
                MessageBox.Show("You have to enter your room key!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passBoxRoomKey.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBoxRoomNumber.Text))
            {
                MessageBox.Show("You have to enter your room number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passBoxRoomKey.Focus();
                return;
            }
            else if ((user_repo.Users.Where(u => u.Login == textBoxLogin.Text).FirstOrDefault() != null) & (user_repo.Users.Where(u => u.Login == textBoxLogin.Text).FirstOrDefault() != user))
            {
                MessageBox.Show("This login is already used! Think of a new one!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textBoxLogin.Focus();
                return;
            }
            else if (room_repo.Rooms.Where(r => r.Key == GetHash(passBoxRoomKey.Password)).FirstOrDefault() == null)
            {
                MessageBox.Show("There is no room with this key!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passBoxRoomKey.Focus();
                return;
            }
            else if (room_repo.Rooms.Where(r => r.Number == int.Parse(textBoxRoomNumber.Text)).FirstOrDefault() == null)
            {
                MessageBox.Show("There is no room with this number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passBoxRoomKey.Focus();
                return;
            }
            else if (room_repo.Rooms.Where(r => r.Number == int.Parse(textBoxRoomNumber.Text)).FirstOrDefault().Key != GetHash(passBoxRoomKey.Password))
            {
                MessageBox.Show("This room has another key! Try to enter the key second time!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passBoxRoomKey.Focus();
                return;
            }
            else
            {
                user.Name = textBoxFullName.Text;
                user.Login = textBoxLogin.Text;
                user.Password = GetHash(passBoxPassword.Password);
                user.Room = room_repo.Rooms.Where(r => r.Key == GetHash(passBoxRoomKey.Password)).FirstOrDefault();

                user_repo.EditUser(user);
                user_repo.Save();

                DialogResult = true;
                this.Close();
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
