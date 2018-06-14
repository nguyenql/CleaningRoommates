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
    /// Interaction logic for SubmiteWorkWindow.xaml
    /// </summary>
    public partial class SubmiteWorkWindow : Window
    {
        private SubmitRepository submitRepository = new SubmitRepository();

        User user;
        DateTime DateOfCleaning;
        User checker = new User();

        public SubmiteWorkWindow(User us, DateTime dayCleaning)//пользователь который в системе
        {
            InitializeComponent();

            user = us;
            checker = SubmitLogics.DetermiteChecker(user);
            DateOfCleaning = dayCleaning;

            Perf.Text = user.Name;
            Date.Text = DateOfCleaning.ToString("MMMM dd, yyyy");
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var submit = new Submit();
            int DateIntOfCleaning = DateOfCleaning.DayOfYear;

            //!!!! в базе данных изменить формат даты на число. Номер дня в году
            submit.WhenDone = DateIntOfCleaning;
            submit.Executer = user;
            submit.Checker = checker;

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

            submitRepository.AddSubmit(submit);
            submitRepository.Save();

            Close();
        }
    }
}

