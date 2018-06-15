using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Submit
    {
        public int Id { get; set; }
        public bool Sweep { get; set; }
        public bool Wash { get; set; }
        public bool Trash { get; set; }
        [NotMapped]
        public DateTime DateOfReceiving { get; set; }
        [NotMapped]
        public DateTime DateOfChecking { get; set; }
        public int WhenDone { get; set; }
        public int WhenChecked { get; set; }
        public User Executer { get; set; }
        public User Checker { get; set; }
        public int AlreadyChecked { get; set; }
    }
}
