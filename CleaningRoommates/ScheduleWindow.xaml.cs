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

namespace CleaningRoommates
{
    /// <summary>
    /// Interaction logic for ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window
    {
        public ScheduleWindow()
        {
            InitializeComponent();

            //Изначальный алгоритм

            User us1 = new User() { Id = 0 };
            User us2 = new User() { Id = 1 };
            List<WhoWhenClean> results = Algoritm.WhoWillCleanToday();
            CreateButtons(results);

            //Передвишаем расписание на один день вперед
            int maxDay = GetMaxDayId(results, us1);
            List<WhoWhenClean> changedDayScheduleDays = ChangeDays(results, maxDay);
            RenewButtons(changedDayScheduleDays);


            //меняем пользователя

            List<WhoWhenClean> changedDayScheduleUsers = ChangeUsers(results, us2,us1);
            RenewButtons(changedDayScheduleUsers);
            
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

                Grid.SetRow(newButton, time.UseId);

                if (time.DayId <= idOfMaxDayInGrid)
                    Grid.SetColumn(newButton, time.DayId);

                schGrid.Children.Add(newButton);
            }
        }

        //Получаем новое расписание 
        public List<WhoWhenClean> ChangeDays(List<WhoWhenClean> initialSchedule, int maxDay)
        {
            foreach (var time in initialSchedule)
            {
                if (time.DayId >= maxDay)
                {
                    time.DayId++;
                }
            }
            return initialSchedule;
        }

        public List<WhoWhenClean> ChangeUsers(List<WhoWhenClean> initialSchedule, User one, User another)
        {
            int dayToChangeOfOne = GetMaxDayId(initialSchedule, one);
            int dayToChangeOfAnother = GetMaxDayId(initialSchedule, another);

            int daysToNextUserClean = 6;

            if (dayToChangeOfAnother < 3)
                dayToChangeOfAnother = dayToChangeOfAnother + daysToNextUserClean;

            foreach (var time in initialSchedule)
            {
                if (time.DayId == dayToChangeOfOne)
                {
                   time.UseId = another.Id;
                }

                if (time.DayId == dayToChangeOfAnother)
                {
                   time.UseId = one.Id;
                }
            }
            return initialSchedule;
        }

        //вычленить у пользователя день с максимальным номером
        //День следующего дежурства
        public int GetMaxDayId(List<WhoWhenClean> initialSchedule, User user)
        {
            int maxDay = 0;

            foreach (var time in initialSchedule)
            {
                if (time.UseId == user.Id)
                {
                    if (time.DayId > maxDay)
                        maxDay = time.DayId;
                }
            }
            return maxDay;
        }

        private void buttonProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow window = new ProfileWindow();
            window.ShowDialog();
        }
    }
}
