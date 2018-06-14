using Core;
using Core.Model;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClickEnterYourRoom(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            window.ShowDialog();
            this.Close();
        }

        private void ButtonClickRegistrateUser(object sender, RoutedEventArgs e)
        {
            UserRegistrationWindow window = new UserRegistrationWindow();
            window.ShowDialog();
            this.Close();

        }

        private void ButtonClickRegistrateRoom(object sender, RoutedEventArgs e)
        {
            RoomRegistrationWindow window = new RoomRegistrationWindow();
            window.ShowDialog();
            this.Close();
        }
    }
}
