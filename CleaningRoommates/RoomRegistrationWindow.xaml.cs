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
    /// Interaction logic for RoomRegistrationWindow.xaml
    /// </summary>
    public partial class RoomRegistrationWindow : Window
    {
        public RoomRepository room_repo { get; set; }

        public RoomRegistrationWindow(RoomRepository room)
        {
            InitializeComponent();
            room_repo = room;
        }

        private void ButtonClickOk(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRoomNumber.Text))
            {
                MessageBox.Show("Enter the room number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textBoxRoomNumber.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                MessageBox.Show("Enter the room password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordBox.Focus();
                return;
            }
            else if (room_repo.Rooms.Where(r => r.Number == int.Parse(textBoxRoomNumber.Text)).FirstOrDefault() != null)
            {
                MessageBox.Show("This room is already registrated!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textBoxRoomNumber.Focus();
                return;
            }
            else
            {
                room_repo.AddRoom(new Room()
                {
                    Number = int.Parse(textBoxRoomNumber.Text),
                    Key = GetHash(passwordBox.Password)
                });
                room_repo.Save();
                DialogResult = true;
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
