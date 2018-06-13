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
using System.Windows.Shapes;

namespace CleaningRoommates
{
    /// <summary>
    /// Interaction logic for IWantToSwapWindow.xaml
    /// </summary>
    public partial class IWantToSwapWindow : Window
    {
        User user = new User();

        public IWantToSwapWindow(User us)//данные пользователя, чье окно открыто
        {
            InitializeComponent();

            user = us;
            Who.Text = user.Name;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var swap = new Swap();

            //!!!! в базе данных изменить формат даты на число. Номер дня в году
            swap.When = DateTime.Today;
            swap.From = user;

            if (Deadline.IsChecked == true)
            {
                swap.DeadLine = true;
                swap.Sick = false;
                swap.NotInTheTown = false;
            }
            else if (Sick.IsChecked == true)
            {
                swap.DeadLine = false;
                swap.Sick = true;
                swap.NotInTheTown = false;
            }
            else if (Out.IsChecked == true)
            {
                swap.DeadLine = false;
                swap.Sick = false;
                swap.NotInTheTown = true;
            }
            else if (Other.IsChecked == true)
            {
                swap.DeadLine = false;
                swap.Sick = false;
                swap.NotInTheTown = false;
                swap.Reason = Reason.Text;
            }
            else
            {
                MessageBox.Show("Please, change reason!");
                return;
            }

            List<Swap> usersFromDatabase = new List<Swap>();// лист пользователей заменить на тот, что буде получать из базы данных
            usersFromDatabase.Add(swap);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
