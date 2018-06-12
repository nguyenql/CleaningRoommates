using Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Control> Controls { get; set; }
        public DbSet<Swap> Swaps { get; set; }

        public Context() : base("RoomCleaning")
        {

        }
    }
}
