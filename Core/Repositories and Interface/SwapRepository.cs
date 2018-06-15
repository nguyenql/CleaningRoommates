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
        }

        private void Restore() // reading data from database
        {
            Swaps = Read(context);
        }

        public List<Swap> Read(Context context)
        {
            return context.Swaps
                .Include("From")
                .Include("Room")
                .Include("Agree")
                .ToList();
        }

        public void Save()
        {
            using (var context = new Context())
            {
                context.SaveChanges();
                Restore();
            }
        }

        public void AddSwap(Swap sw)
        {
            using (var context = new Context())
            {
                var swap = new Swap()
                {
                    DeadLine = sw.DeadLine,
                    Sick = sw.Sick,
                    NotInTheTown = sw.NotInTheTown,
                    Reason = sw.Reason,
                    When = sw.When,
                    From = context.Users.Single(x => x.Id == sw.From.Id),
                    Room = context.Rooms.Single(x => x.Id == sw.Room.Id)
                };

                context.Swaps.Add(swap);
                context.SaveChanges();
            }
        }

        public void EditSwap(Swap swap)
        {
            using (var context = new Context())
            {
                Swap sw = context.Swaps.Single(g => g.Id == swap.Id);
                sw.Agree = context.Users.Single(x => x.Id == swap.Agree.Id);
                sw.OnWhat = swap.OnWhat;
                context.SaveChanges();
            }
        }
    }
}
