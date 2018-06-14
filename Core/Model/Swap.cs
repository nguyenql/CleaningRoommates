using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped]
        public DateTime DateOfReceiving { get; set; }
        public int When { get; set; }
        public int OnWhat { get; set; }
        public User From { get; set; }
        public User Agree { get; set; }
    }
}
