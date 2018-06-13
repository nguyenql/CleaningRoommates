using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SubmitLogic
    {
        //определение дня дежурства пользователя для базы данных
        public static string GetDayOfCleaning(List<WhoWhenClean> results, User user)
        {
            int thisYear = DateTime.Now.Year;
            int todayInYear = DateTime.Now.DayOfYear;
            int dayToAdd = SwapLogic.GetMaxDayId(results, user);
            int firstDayInGrid = todayInYear - 3;
            int dayNextClean = firstDayInGrid + dayToAdd;

            DateTime theDate = new DateTime(thisYear, 1, 1).AddDays(dayNextClean - 1);

            return theDate.ToString();
        }
    }
}
