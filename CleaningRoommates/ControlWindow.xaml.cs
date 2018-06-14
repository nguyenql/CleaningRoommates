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
    /// Interaction logic for ControlWindow.xaml
    /// </summary>
    public partial class ControlWindow : Window
    {
        SubmitRepository submitRepository = new SubmitRepository();

        User user;
        Submit submit;

        public ControlWindow(Submit sub, User us)//пользователь который в системе
        {
            InitializeComponent();

            int thisYear = DateTime.Now.Year;
            user = us;
            submit = sub;
            DateTime DateTimeOfCleaning = new DateTime(thisYear, 1, 1).AddDays(submit.WhenDone - 1);

            Who.Text = submit.Executer.Name;
            When.Text = DateTimeOfCleaning.ToString("MMMM dd, yyyy");
            WhoControl.Text = user.Name;

            if (user.Id == submit.Executer.Id)
            {
                if (submit.Wash == true)
                    Wash.IsChecked = true;
                if (submit.Sweep == true)
                    Sweep.IsChecked = true;
                if (submit.Trash == true)
                    Trash.IsChecked = true;

                Wash.IsEnabled = false;
                Sweep.IsEnabled = false;
                Trash.IsEnabled = false;
            }
            else
            {
                if (submit.Sweep == true)
                {
                    res_Sweep.Text = "+";
                };

                if (submit.Wash == true)
                {
                    res_Wash.Text = "+";
                };

                if (submit.Trash == true)
                {
                    res_Trash.Text = "+";
                };
            }

        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if(user.Id == submit.Executer.Id)
            {
                int DateIntOfCleaning = DateTime.Now.DayOfYear;

                //!!!! в базе данных изменить формат даты на число. Номер дня в году
                submit.WhenChecked = DateIntOfCleaning;
                //submit.Checker = user;

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
                //ДОБАВИТЬ В СПИСОК- СОХРАНИТЬ ИЗМЕНЕНИЯ
                submitRepository.Submits.Add(submit);

                Close();
            }
            else
            {
                Close();
            }

        }

    }
}
