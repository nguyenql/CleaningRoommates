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
            }
        }
    }
}
