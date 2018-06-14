using Core.Model;
using Core.Repositories_and_Interface;
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
            SwapRepository swapRepository = new SwapRepository();
            List<Swap> swaps = swapRepository.Swaps;

            List<Swap> userSwap = new List<Swap>();
            int daysInWeek = 7;

            foreach (var item in swaps)
            {
                int ItemDayToView = DateTime.Now.DayOfYear - item.When;

                if (item.From.Id != user.Id && ItemDayToView < daysInWeek)
                {
                    item.DateOfReceiving = ActualSchedule.TransformToDateTime(item.When);
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
                    time.UseId = another.IdForGala;
                }

                if (time.DayId == dayToChangeOfAnother)
                {
                    time.UseId = one.IdForGala;
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
                if (time.UseId == user.IdForGala)
                {
                    if (time.DayId > maxDay)
                        maxDay = time.DayId;
                }
            }
            return maxDay;
        }
    }
}