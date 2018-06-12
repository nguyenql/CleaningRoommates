using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Control
    {
        public int Id { get; set; }
        public bool Sweep { get; set; }
        public bool Wash { get; set; }
        public bool Trash { get; set; }
        public DateTime When { get; set; }
        public virtual User Executer { get; set; }
        public virtual User Checker { get; set; }
    }
}
