using Core;
using Core.Model;
using Core.Repositories_and_Interface;
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

namespace CleaningRoommates
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RoomRepository room_repo = new RoomRepository();
        private UserRepository user_repo = new UserRepository();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClickEnterYourRoom(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow(user_repo, room_repo);
            window.ShowDialog();
            this.Close();
        }

        private void ButtonClickRegistrateUser(object sender, RoutedEventArgs e)
        {
            UserRegistrationWindow window = new UserRegistrationWindow(user_repo, room_repo);
            window.ShowDialog();
            this.Close();

        }

        private void ButtonClickRegistrateRoom(object sender, RoutedEventArgs e)
        {
            RoomRegistrationWindow window = new RoomRegistrationWindow(room_repo);
            window.ShowDialog();
        }
    }
}
