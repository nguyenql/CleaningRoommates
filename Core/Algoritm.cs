﻿using Core.Model;
using Core.Repositories_and_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Algoritm
    {
        public static List<WhoWhenClean> WhoWillCleanToday(int countUsers)
        {
            //алгоритм на на основе номера сегодняшнего дня в году 
            int todayInYear = DateTime.Now.DayOfYear;
            int firstDayInGrid = todayInYear - 3;
            int beginToCount = 0;
            int timesInWeek = 4;//нулевая колонка грида будет заполнена. Стоюцов в массиве будет 4 

            if (firstDayInGrid % 2 == 0)//все нечетные дни в году один из пользователей будет дежурить =
            {
                firstDayInGrid++;
                beginToCount++;
                timesInWeek = 3;
            }

            List<WhoWhenClean> results = new List<WhoWhenClean>();
            results = AlgorithmOfCleaning(firstDayInGrid, beginToCount, timesInWeek, results, countUsers);

            return results;
        }

        public static List<WhoWhenClean> AlgorithmOfCleaning(int firstDayInGrid, int beginToCount, int timesInWeek, List<WhoWhenClean> results, int numberUs)
        {
            int intervalDutiesOfOneUser = GetIntervalBerweenUserCleaning(numberUs);
            int daysToNextDuty = intervalDutiesOfOneUser / numberUs;
            int firstDayWhenDutiesBegun = 1; //конкретно день дежурства, к которому потом прибавим интервал и получим день следующего дежурства
            int Who;

            //Дежурит Первый пользователь
            if ((firstDayInGrid - firstDayWhenDutiesBegun) % intervalDutiesOfOneUser == 0) //если будет дежурить первый пользователей, то номер сегодняшнего дня в году - 1 = кратен 6                                                                                         //потому что этот пользователей дежурил в первый день в году (дни дежурств будут: 1, 7, 13 и так далее). Аналогично для других
            {
                Who = 0;
            }
            //Дежурит Второй пользователь
            else if ((firstDayInGrid - (firstDayWhenDutiesBegun + daysToNextDuty)) % intervalDutiesOfOneUser == 0)
            {
                Who = 1;
            }
            //Дежурит Второй пользователь
            else
                Who = 2;

            for (var i = 0; i < timesInWeek; i++)
            {
                var time = new WhoWhenClean();

                time.UseId = Who;
                time.DayId = beginToCount;

                Who++;
                if (Who > numberUs - 1)
                    Who = 0;
                beginToCount = beginToCount + 2;

                results.Add(time);
            }
            return results;
        }

        public static int GetIntervalBerweenUserCleaning(int numberOfusers)
        {
            int numberOfUsers = numberOfusers; //количество пользователей (3)
            int daysBetweenDuties = 1; //дежурство через день
            int daysToNextDuty = daysBetweenDuties + 1;
            return numberOfUsers * daysToNextDuty; //один человек дежурит через 6 дней. То есть сегодняшняя дата плюс 6 = дата следующего дежурства
        }
    }
}
