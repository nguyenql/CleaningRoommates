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
            return context.Users.Include("Room").ToList();
        }

        public void Save()
        {
            using (var context = new Context())
            {
                context.SaveChanges();
                Restore();
            }
        }

        public void AddUser(User us)
        {
            using (var context = new Context())
            {
                var user = new User()
                {
                    Name = us.Name,
                    Login = us.Login,
                    Password = us.Password,
                    Room = context.Rooms.Single(x => x.Key == us.Room.Key),

                };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void EditUser(User user)
        {
            using (var context = new Context())
            {
                context.Users.Where(g => g.Id == user.Id).FirstOrDefault().Name = user.Name;
                context.Users.Where(g => g.Id == user.Id).FirstOrDefault().Login = user.Login;
                context.Users.Where(g => g.Id == user.Id).FirstOrDefault().Password = user.Password;
                context.Users.Where(g => g.Id == user.Id).FirstOrDefault().Room = context.Rooms.Where(x => x.Number == user.Room.Number).FirstOrDefault();
                context.SaveChanges();
            }
        }
    }
}
