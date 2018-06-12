using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual Room Room { get; set; }
        [NotMapped]
        public virtual List<Control> Controls { get; set; }
        [NotMapped]
        public virtual List<Swap> Swaps { get; set; }
    }
}
