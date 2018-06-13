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
using Core;
using Core.Model;
using System.Drawing;


namespace CleaningRoommates
{
    /// <summary>
    /// Interaction logic for ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window
    {
        List<WhoWhenClean> results = Algoritm.WhoWillCleanToday();

        public ScheduleWindow()
        {
            //Изначальный алгоритм

            User us1 = new User() { Id = 0 };
            User us2 = new User() { Id = 1 };
            results = Algoritm.WhoWillCleanToday();
            CreateButtons(results);

            //Передвишаем расписание на один день вперед
            /*int maxDay = SwapLogic.GetMaxDayId(results, us1);
            List<WhoWhenClean> changedDayScheduleDays = SwapLogic.ChangeDays(results, maxDay);
            RenewButtons(changedDayScheduleDays);
            */

            //меняем пользователя
            /*
            List<WhoWhenClean> changedDayScheduleUsers = SwapLogic.ChangeUsers(results, us2,us1);
            RenewButtons(changedDayScheduleUsers);
            */
            //RenewButtons(results);
            
            
        }
        
        public void RenewButtons(List<WhoWhenClean> changedDaySchedule)
        {
            schGrid.Children.Clear();
            CreateButtons(changedDaySchedule);
        }

        public void CreateButtons(List<WhoWhenClean> results)
        {
            int idOfMaxDayInGrid = 6;
            foreach (var time in results)
            {
                Button newButton = new Button();

                newButton.Height = 30;
                newButton.Width = 30;
                newButton.BorderBrush = Brushes.DeepSkyBlue;
                newButton.Background = Brushes.LightBlue;



                Grid.SetRow(newButton, time.UseId);

                if (time.DayId <= idOfMaxDayInGrid)
                    Grid.SetColumn(newButton, time.DayId);

                schGrid.Children.Add(newButton);
            }
        }

        private void buttonProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow window = new ProfileWindow();
            window.Show();
        }

        private void buttonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void Click_Submit(object sender, RoutedEventArgs e)
        {
            SubmiteWorkWindow window = new SubmiteWorkWindow(results);
            window.ShowDialog();
        }
    }
}
