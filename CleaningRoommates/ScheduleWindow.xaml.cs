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
using Core.Repositories_and_Interface;
using System.Globalization;

namespace CleaningRoommates
{
    /// <summary>
    /// Interaction logic for ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window
    {
        DateTime dateOfCleaningDateTime;
        User user;
        List<User> PeopleWhoLiveInOneRoom;
        List<WhoWhenClean> results;
        int today = DateTime.Now.DayOfYear;
        int countUsers;
        private UserRepository user_repo = new UserRepository();


        public ScheduleWindow(User us)
        {
            InitializeComponent();
            user = us;

            PeopleWhoLiveInOneRoom = SubmitLogics.MakeList(user, user_repo.Users);
            countUsers = PeopleWhoLiveInOneRoom.Count;

            results = ActualSchedule.GetActualSchedule(countUsers, PeopleWhoLiveInOneRoom);
            int dayToAdd = SwapLogics.GetMaxDayId(results, user);
            dateOfCleaningDateTime = SubmitLogics.GetDayOfCleaning(results, dayToAdd);

            CreateButtons(results);

            DateTime date = DateTime.Now;

            mThree.Text = date.AddDays(-3).ToString("MMM dd", CultureInfo.CreateSpecificCulture("en-US"));
            mTwo.Text = date.AddDays(-2).ToString("MMM dd", CultureInfo.CreateSpecificCulture("en-US"));
            mOne.Text = date.AddDays(-1).ToString("MMM dd", CultureInfo.CreateSpecificCulture("en-US"));
            pOne.Text = date.AddDays(+1).ToString("MMM dd", CultureInfo.CreateSpecificCulture("en-US"));
            pTwo.Text = date.AddDays(+2).ToString("MMM dd", CultureInfo.CreateSpecificCulture("en-US"));
            pThree.Text = date.AddDays(+3).ToString("MMM dd", CultureInfo.CreateSpecificCulture("en-US"));

            uOne.Text = PeopleWhoLiveInOneRoom[0].Name;
            if (PeopleWhoLiveInOneRoom.Count == 2)
            {
                uTwo.Text = PeopleWhoLiveInOneRoom[1].Name;
            }
            if (PeopleWhoLiveInOneRoom.Count == 3)
            {
                uTwo.Text = PeopleWhoLiveInOneRoom[1].Name;
                uThree.Text = PeopleWhoLiveInOneRoom[2].Name;
            }

            RenewSwapsSubmits();
        }


        public void RenewSwapsSubmits()
        {
            //ЛИСТ SWAPS СООБЩЕНИЙ
            var swaps = SwapLogics.UserSwaps(user);
            dataGridSwap.ItemsSource = swaps;

            //ЛИСТ SUBMITS СООБЩЕНИЙ
            var submits = SubmitLogics.UserSubmits(user);
            dataGridSubmit.ItemsSource = submits;
        }

        public void RenewButtons()
        {
            List<WhoWhenClean> results = ActualSchedule.GetActualSchedule(countUsers, PeopleWhoLiveInOneRoom);
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

        private void buttonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void Click_MySubmit(object sender, RoutedEventArgs e)
        {
            int nextMyCleaning = dateOfCleaningDateTime.DayOfYear;
            int interval = Algoritm.GetIntervalBerweenUserCleaning(countUsers);
            int previousCleaning = SubmitLogics.GetMinDayId(results,user,PeopleWhoLiveInOneRoom);
            int dayCheck = SubmitLogics.GetDayOfCleaning(results, previousCleaning).DayOfYear;

            if (today == dayCheck || today == dayCheck + 1)
            {
                SubmiteWorkWindow window = new SubmiteWorkWindow(user, dayCheck, PeopleWhoLiveInOneRoom);
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
            if (dateOfCleaningDateTime >= DateTime.Now.Date)
            {
                IWantToSwapWindow window = new IWantToSwapWindow(user, dateOfCleaningDateTime);
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Too early!");
                return;
            };
            
        }

        private void buttonSwap_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGridSwap.SelectedItem as Swap;

            if (selectedItem == null)
            {
                MessageBox.Show("Select message");
            }
            else if (selectedItem.Agree != null)
            {
                MessageBox.Show(selectedItem.Agree.Name + "has already agreed!");
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
            else if (selectedItem.AlreadyChecked == 1 
                && user.Id == selectedItem.Checker.Id)
            {
                MessageBox.Show("Already Checked!");
            }
            else
            {
               ControlWindow window = new ControlWindow(selectedItem, user);
               window.ShowDialog();
            }
        }
    }
}
