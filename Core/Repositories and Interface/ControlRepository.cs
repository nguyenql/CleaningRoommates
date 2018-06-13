using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories_and_Interface
{
    public class ControlRepository : Interface
    {
        public List<Control> Users { get; set; }
        Context context = new Context();

        public ControlRepository()
        {
            Restore();
        }

        private void Restore() // reading data from database
        {
            Users = Read(context);
        }

        static List<Control> Read(Context context)
        {
            return context.Controls.ToList();
        }

        public void Save(Context context)
        {
            context.SaveChanges();
        }
    }
}
