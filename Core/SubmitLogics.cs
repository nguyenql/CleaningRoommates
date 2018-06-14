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
        //определение дня дежурства пользователя для базы данных
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
    }
}