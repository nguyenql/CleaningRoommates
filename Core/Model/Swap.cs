using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Swap
    {
        public int Id { get; set; }
        public bool DeadLine { get; set; }
        public bool Sick { get; set; }
        public bool NotInTheTown { get; set; }
        public string Reason { get; set; }
        public DateTime When { get; set; }
        public DateTime OnWhat { get; set; }
        public virtual User From { get; set; }
        public virtual User Agree { get; set; }
    }
}
