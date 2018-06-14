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
using System.Windows.Shapes;

namespace CleaningRoommates
{
    /// <summary>
    /// Interaction logic for IWantToSwapWindow.xaml
    /// </summary>
    public partial class IWantToSwapWindow : Window
    {
        SwapRepository swapRepository = new SwapRepository();
        User user;
        DateTime dateOfCleaningDateTime;

        public IWantToSwapWindow(User us, DateTime dateTime)//данные пользователя, чье окно открыто
        {
            InitializeComponent();

            dateOfCleaningDateTime = dateTime;
            user = us;

            Who.Text = user.Name;
            When.Text = dateOfCleaningDateTime.ToString("MMMM dd, yyyy");
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var swap = new Swap();

            //!!!! в базе данных изменить формат даты на число. Номер дня в году
            swap.When = dateOfCleaningDateTime.DayOfYear;
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

            //ДОБАВИТЬ В СПИСОК 
            swapRepository.Swaps.Add(swap);
            this.Close();
            /*
            List<Swap> usersFromDatabase = new List<Swap>();// лист пользователей заменить на тот, что буде получать из базы данных
            usersFromDatabase.Add(swap);*/
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
