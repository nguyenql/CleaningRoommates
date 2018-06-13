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
        User user;
        DateTime DateOfCleaning;

        public SubmiteWorkWindow(User us, DateTime dayCleaning)//пользователь который в системе
        {
            InitializeComponent();

            user = us;
            DateOfCleaning = dayCleaning;

            string DateStringOfCleaning = DateOfCleaning.ToString("MMMM dd, yyyy");

            Date.Text = DateStringOfCleaning;
            Perf.Text = user.Name;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var submit = new Submit();
            int DateIntOfCleaning = DateOfCleaning.DayOfYear;

            //!!!! в базе данных изменить формат даты на число. Номер дня в году
            submit.WhenDone = DateIntOfCleaning;
            submit.Executer = user;

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

            if(submit.Wash==false && submit.Trash==false && submit.Sweep ==false)
            {
                MessageBox.Show("Please, change reason!");
                return;
            }
            //добавляем в базу данных
            //submitsFromDatabase.Add(submit);
        }
    }
}

