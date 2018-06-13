using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories_and_Interface
{
    public class UserRepository : Interface
    {
        public List<User> Users { get; set; }
        Context context = new Context();

        public UserRepository()
        {
            Restore();
        }

        private void Restore() // reading data from database
        {
            Users = Read(context);
        }

        static List<User> Read(Context context)
        {
            return context.Users.Include("Room").ToList() ;
        }

        public void Save(Context context)
        {
            context.SaveChanges();
        }
    }
}
