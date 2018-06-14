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
        [NotMapped]
        public int IdForGala { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Room Room { get; set; }
        [NotMapped]
        public List<Submit> Submits { get; set; }
        [NotMapped]
        public List<Swap> Swaps { get; set; }
    }
}
