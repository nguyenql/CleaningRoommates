using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories_and_Interface
{
    public class RoomRepository : Interface
    {
        public List<Room> Rooms { get; set; }
        Context context = new Context();

        public RoomRepository()
        {
            Restore();
            Save();
        }

        private void Restore() // reading data from database
        {
            Rooms = Read(context);
        }

        static List<Room> Read(Context context)
        {
            return context.Rooms.ToList();
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
