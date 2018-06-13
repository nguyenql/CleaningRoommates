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
    /// Interaction logic for IAgreeOrDisagreeToSwapWindow.xaml
    /// </summary>
    public partial class IAgreeOrDisagreeToSwapWindow : Window
    {
        Swap swap = new Swap();

        public IAgreeOrDisagreeToSwapWindow(Swap sw, User user)
        {
            InitializeComponent();

            int thisYear = DateTime.Now.Year;
            swap = sw;
            DateTime DateTimeOfCleaning = new DateTime(thisYear, 1, 1).AddDays(swap.When - 1);

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
            //swap.Agree = 
        }
    }
}
