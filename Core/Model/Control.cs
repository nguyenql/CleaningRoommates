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
        public int WhenDone { get; set; }
        public int WhenChecked { get; set; }
        public User Executer { get; set; }
        public User Checker { get; set; }
    }
}
