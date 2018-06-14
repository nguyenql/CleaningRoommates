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
using System.Windows.Shapes;

namespace CleaningRoommates
{
    /// <summary>
    /// Interaction logic for IAgreeOrDisagreeToSwapWindow.xaml
    /// </summary>
    public partial class IAgreeOrDisagreeToSwapWindow : Window
    {
        private SwapRepository swapRepository = new SwapRepository();

        User user;
        Swap swap;

        public IAgreeOrDisagreeToSwapWindow(Swap sw, User us)
        {
            InitializeComponent();

            int thisYear = DateTime.Now.Year;
            user = us;
            swap = sw;
            DateTime DateTimeOfCleaning = ActualSchedule.TransformToDateTime(swap.When);

            Who.Text = swap.From.Name;
            When.Text = DateTimeOfCleaning.ToString("MMMM dd, yyyy");

            if (swap.Sick == true)
            {
                Reason.Text = "I am sorry. I am sick.";
            }
            else if (swap.DeadLine == true)
            {
                Reason.Text = "I am sorry. I have deadline.";
            }
            else if (swap.NotInTheTown == true)
            {
                Reason.Text = "I am sorry. I will not be in town that day.";
            }
            else
                Reason.Text = swap.Reason;
        }

        private void Agree_Click(object sender, RoutedEventArgs e)
        {
            swap.Agree = user;

            swapRepository.EditSwap(swap);
            swapRepository.Save();

            Close();
        }

        private void buttonDisagree_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
