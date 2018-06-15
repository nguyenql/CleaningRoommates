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
        private SwapRepository swapRepository = new SwapRepository();
        User user;
        DateTime dateOfCleaningDateTime;

        public IWantToSwapWindow(User us, DateTime dateTime)
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
            swap.When = dateOfCleaningDateTime.DayOfYear;
            swap.From = user;
            swap.Room = user.Room;

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

            swapRepository.AddSwap(swap);
            swapRepository.Save();
            Close();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
