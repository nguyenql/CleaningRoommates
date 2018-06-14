using Core.Model;
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
            int thisYear = DateTime.Now.Year;
            int todayInYear = DateTime.Now.DayOfYear;
            int dayToAdd = SwapLogics.GetMaxDayId(results, user);
            int firstDayInGrid = todayInYear - 3;
            int dayNextClean = firstDayInGrid + dayToAdd;

            DateTime DateTimeOfCleaning = new DateTime(thisYear, 1, 1).AddDays(dayNextClean - 1);
            return DateTimeOfCleaning;
        } 

        public static User DetermiteChecker(User user)
        {
            User checker = new User();
            int checkerId = user.Id + 1;

            if (checkerId < 2)
                checker.Id = checkerId;
            else
                checker.Id = 0;

            return checker;
        }

        public static List<Submit> UserSubmits(User user)
        {
            //FROM DATASASE
            List<Submit> submits = new List<Submit>();

            List<Submit> userSubmit = new List<Submit>();

            foreach (var item in submits)
            {
                if (item.Checker.Id == user.Id)
                {
                    userSubmit.Add(item);
                }
            }

            return userSubmit;

        }
    }
}