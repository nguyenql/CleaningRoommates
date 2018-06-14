using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ActualSchedule
    {
        public static List<WhoWhenClean> GetActualSchedule()
        {
            //SWAPS FROM DATABASE
            List<Swap> swaps = new List<Swap>();

            int today = DateTime.Now.DayOfYear;
            List<WhoWhenClean> initialSchedule = Algoritm.WhoWillCleanToday();

            foreach (var item in swaps)
            {
                if(item.When< today && item.Agree == null)
                {
                    initialSchedule = SwapLogics.ChangeDays(initialSchedule, item.When);
                }

                if(item.When > today && item.Agree != null)
                {
                    initialSchedule = SwapLogics.ChangeUsers(initialSchedule, item.From, item.Agree);
                }
            }   

            return initialSchedule;
        }
    }
}
