using Core.Model;
using Core.Repositories_and_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SubmitLogics
    {
        //определение крайнего дня дежурства пользователя для базы данных
        public static DateTime GetDayOfCleaning(List<WhoWhenClean> results, User user)
        {
            int todayInYear = DateTime.Now.DayOfYear;
            int dayToAdd = SwapLogics.GetMaxDayId(results, user);
            int firstDayInGrid = todayInYear - 3;
            int dayNextClean = firstDayInGrid + dayToAdd;

            DateTime DateTimeOfCleaning = ActualSchedule.TransformToDateTime(dayNextClean);
            return DateTimeOfCleaning;
        } 

        public static User DetermiteChecker(User user)
        {
            User checker = new User();
            int checkerId = user.Id + 1;

            if (checkerId < 2)
                checker.IdForGala = checkerId;
            else
                checker.IdForGala = 0;

            return checker;
        }

        public static List<Submit> UserSubmits(User user)
        {
            SubmitRepository submitRepository = new SubmitRepository();
            List<Submit> submits = submitRepository.Submits;

            List<Submit> userSubmit = new List<Submit>();
            int daysInWeek = 7;

            foreach (var item in submits)
            {
                int ItemDayToView = DateTime.Now.DayOfYear - item.WhenDone;

                if (ItemDayToView < daysInWeek)
                {
                //Пользователь проверяющий
                if (item.Checker.Id == user.Id)
                {
                    item.DateOfReceiving = ActualSchedule.TransformToDateTime(item.WhenDone);
                    item.DateOfChecking = ActualSchedule.TransformToDateTime(item.WhenChecked);
                    userSubmit.Add(item);
                }
                //Пользователя проверили
                if (item.Executer.Id == user.Id&&item.WhenChecked !=0)
                {
                    item.DateOfChecking = ActualSchedule.TransformToDateTime(item.WhenChecked);
                    item.DateOfReceiving = ActualSchedule.TransformToDateTime(item.WhenDone);
                    userSubmit.Add(item);
                }
                }

            }

            return userSubmit;

        }
    }
}