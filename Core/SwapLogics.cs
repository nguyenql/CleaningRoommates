using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SwapLogics
    {
        //Получаем новое расписание 
        public static List<Swap> UserSwaps (User user)
        {
            //FROM DATASASE
            List<Swap> swaps = new List<Swap>();

            List<Swap> userSwap = new List<Swap>();

            foreach (var item in swaps)
            {
                if(item.From.Id != user.Id)
                {
                    userSwap.Add(item);
                }
            }

            return userSwap;

        }
        public static List<WhoWhenClean> ChangeDays(List<WhoWhenClean> initialSchedule, int dayAsked)
        {
            int today = DateTime.Now.DayOfYear;
            int dayBetweenTodayAndDayAsked = today - dayAsked;
            int seenInGrid = 3;

            foreach (var time in initialSchedule)
            {
                if (dayBetweenTodayAndDayAsked > seenInGrid)
                {
                    time.DayId++;
                }
                else
                {
                    if (time.DayId > seenInGrid - dayBetweenTodayAndDayAsked)
                            time.DayId++;
                }
            }
            //тоже самое

            //if (dayBetweenTodayAndDayAsked > seenInGrid)
            //{
            //    foreach (var time in initialSchedule)
            //    {
            //        time.DayId++;
            //    }
            //}
            //else
            //{
            //    foreach (var time in initialSchedule)
            //    {
            //        if (time.DayId > seenInGrid - dayBetweenTodayAndDayAsked)
            //            time.DayId++;
            //    }
            //}

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