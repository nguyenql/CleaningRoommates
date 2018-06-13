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
using System.Windows.Shapes;

namespace CleaningRoommates
{
    /// <summary>
    /// Interaction logic for SubmiteWorkWindow.xaml
    /// </summary>
    public partial class SubmiteWorkWindow : Window
    {
        List<WhoWhenClean> results;
        User user;
        int DateOfCleaning;

        public SubmiteWorkWindow(List<WhoWhenClean> list, User us)//пользователь который в системе
        {
            InitializeComponent();

            results = list;
            user = us;
            DateOfCleaning = SubmitLogics.GetDayOfCleaning(results, us);
            //Date.Text = DateOfCleaning;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var submit = new Submit();

            //!!!! в базе данных изменить формат даты на число. Номер дня в году
            //submit.WhenDone = ;
            //submit.From = user;
           
            if (Wash.IsChecked == true)
            {
                submit.Wash = true;
            }
            else
                submit.Wash = false;

            if (Sweep.IsChecked == true)
            {
                submit.Sweep = true;
            }
            else
                submit.Sweep = false;

            if (Trash.IsChecked == true)
            {
                submit.Trash = true;
            }
            else
                submit.Trash = false;
            MessageBox.Show("Please, change reason!");
                return;

            //usersFromDatabase.Add(swap);
        }
        
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

