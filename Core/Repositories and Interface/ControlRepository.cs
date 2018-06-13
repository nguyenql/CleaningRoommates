using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories_and_Interface
{
    public class SubmitRepository : Interface
    {
        public List<Submit> Submits { get; set; }
        Context context = new Context();

        public SubmitRepository()
        {
            Restore();
        }

        private void Restore() // reading data from database
        {
            Submits = Read(context);
        }

        static List<Submit> Read(Context context)
        {
            return context.Submits.ToList();
        }

        public void Save(Context context)
        {
            context.SaveChanges();
        }
    }
}
