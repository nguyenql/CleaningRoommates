using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories_and_Interface
{
    public class SwapRepository : Interface
    {
        public List<Swap> Swaps { get; set; }
        Context context = new Context();

        public SwapRepository()
        {
            Restore();
            Save();
        }

        private void Restore() // reading data from database
        {
            Swaps = Read(context);
        }

        static List<Swap> Read(Context context)
        {
            return context.Swaps.ToList();
        }

        public void Save()
        {
            using (var context = new Context())
            {
                context.SaveChanges();
                Restore();
            }
        }

        static void AddSwap(Swap swap)
        {
            using (var context = new Context())
            {
                context.Swaps.Add(swap);
                context.SaveChanges();
            }
        }

        static void EditSwap(Swap swap)
        {
            using (var context = new Context())
            {
                var sw = context.Swaps.Where(g => g.Id == swap.Id).FirstOrDefault();
                sw.DeadLine = swap.DeadLine;
                sw.Sick = swap.Sick;
                sw.NotInTheTown = swap.NotInTheTown;
                sw.Reason = swap.Reason;
                sw.OnWhat = swap.OnWhat;
                sw.Agree = swap.Agree;
                context.SaveChanges();
            }
        }
    }
}
