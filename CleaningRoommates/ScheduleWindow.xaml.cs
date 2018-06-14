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
        DateTime dateOfCleaningDateTime;
        int today = DateTime.Now.DayOfYear;
        User user = new User() { Id = 1 };
        List<WhoWhenClean> results = ActualSchedule.GetActualSchedule();


        public ScheduleWindow(User us)
        {
            InitializeComponent();
            user = us;
            //RenewButtons();

            dateOfCleaningDateTime = SubmitLogics.GetDayOfCleaning(results, user);

            //User us1 = new User() { Id = 0 };
            //results = Algoritm.WhoWillCleanToday();
            
            CreateButtons(results);

            //ЛИСТ SWAPS СООБЩЕНИЙ
            var swaps = SwapLogics.UserSwaps(user);
            dataGridSwap.ItemsSource = swaps;

            //ЛИСТ SUBMITS СООБЩЕНИЙ
            var submits = SubmitLogics.UserSubmits(user);
            dataGridSubmit.ItemsSource = submits;

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
        
        public void RenewButtons()
        {
            List<WhoWhenClean> results = ActualSchedule.GetActualSchedule();
            schGrid.Children.Clear();
            CreateButtons(results);
        }

        public void CreateButtons(List<WhoWhenClean> results)
        {
            int idOfMaxDayInGrid = 6;
            foreach (var time in results)
            {                
                Image newButton = new Image();

                newButton.Height = 30;
                newButton.Width = 30;

                newButton.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/broom_color.png"));

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

        private void Click_MySubmit(object sender, RoutedEventArgs e)
        {
            int nextMyCleaning = dateOfCleaningDateTime.DayOfYear;
            
            if(today==nextMyCleaning|| today == nextMyCleaning + 1)
            {
                SubmiteWorkWindow window = new SubmiteWorkWindow(user, dateOfCleaningDateTime);
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wait your turn to clean!");
                return;
            }
        }

        private void Click_MySwap(object sender, RoutedEventArgs e)
        {
            IWantToSwapWindow window = new IWantToSwapWindow(user, dateOfCleaningDateTime);
            window.ShowDialog();
        }

        private void buttonSwap_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGridSubmit.SelectedItem as Swap;

            if (selectedItem == null)
            {
                MessageBox.Show("Select message");
            }
            else
            {
                IAgreeOrDisagreeToSwapWindow window = new IAgreeOrDisagreeToSwapWindow(selectedItem, user);
                window.ShowDialog();
            }

            RenewButtons();
        }

        private void buttonSubmits_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGridSubmit.SelectedItem as Submit;

            if (selectedItem == null)
            {
                MessageBox.Show("Select message");
            }
            else
            {
               ControlWindow window = new ControlWindow(selectedItem, user);
               window.ShowDialog();
            }
        }

        private void dataGridSwap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
