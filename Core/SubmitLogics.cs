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
        public static DateTime GetDayOfCleaning(List<WhoWhenClean> results, int dayToAdd)
        {
            int todayInYear = DateTime.Now.DayOfYear;
            int firstDayInGrid = todayInYear - 3;
            int dayNextClean = firstDayInGrid + dayToAdd;

            DateTime DateTimeOfCleaning = ActualSchedule.TransformToDateTime(dayNextClean);
            return DateTimeOfCleaning;
        } 

        public static User DetermiteChecker(User user, List<User> usersInRoom)
        {
            User checker = new User();

            int checkerId = user.IdForGala + 1;

            if (checkerId > 2)
                checkerId = 0;

            foreach (var item in usersInRoom)
            {
                if (item.IdForGala == checkerId)
                {
                    checker = item;
                }
            }
            return checker;
        }

        public static int GetMinDayId(List<WhoWhenClean> initialSchedule, User user, List<User> usersInRoom)
        {
            int minDay = 100;

            foreach (var time in initialSchedule)
            {
                if (time.UseId == user.IdForGala)
                {
                    if (time.DayId < minDay)
                        minDay = time.DayId;
                }
            }
            return minDay;
        }

        public static List<User> MakeList(User us, List<User> users)
        {
            List<User> neighbors = new List<User>();
            int a = 0;

            foreach (var user in users)
            {
                if (user.Room.Id == us.Room.Id)
                {
                    user.IdForGala = a;
                    a += 1;
                    neighbors.Add(user);
                }
            }
            return neighbors;
        }

        public static User GetUserWitnSpecialId(User user, List<User> users)
        {
            foreach (var item in users)
            {
                if (user.Id == item.Id)
                {
                    user.IdForGala = item.IdForGala;
                }
            }
            return user;
        }
    }
}