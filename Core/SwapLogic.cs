using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SwapLogic
    {
        //Получаем новое расписание 
        public static List<WhoWhenClean> ChangeDays(List<WhoWhenClean> initialSchedule, int maxDay)
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

        public static List<WhoWhenClean> ChangeUsers(List<WhoWhenClean> initialSchedule, User one, User another)
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
        public static int GetMaxDayId(List<WhoWhenClean> initialSchedule, User user)
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
    }
}
