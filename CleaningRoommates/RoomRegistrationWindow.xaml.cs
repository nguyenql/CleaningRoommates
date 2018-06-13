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
using System.Windows.Shapes;

namespace CleaningRoommates
{
    /// <summary>
    /// Interaction logic for RoomRegistrationWindow.xaml
    /// </summary>
    public partial class RoomRegistrationWindow : Window
    {
        public RoomRegistrationWindow()
        {
            InitializeComponent();
        }

        private void ButtonClickOk(object sender, RoutedEventArgs e)
        {
            Close();

        }
        
        private void ButtonClickCancel(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}
