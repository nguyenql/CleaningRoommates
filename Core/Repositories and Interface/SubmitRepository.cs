using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories_and_Interface
{
    public class SubmitRepository : Interface
    {
        public List<Submit> Submits { get; set; }
        
        public SubmitRepository()
        {
            Restore();
            Save();
        }

        private void Restore() // reading data from database
        {
            Submits = Read();
        }

        static List<Submit> Read()
        {
            using (var context = new Context())
            {
                return context.Submits.ToList();
            }
        }

        public void Save()
        {
            using (var context = new Context())
            {
                context.SaveChanges();
                Restore();
            }
        }

        static void AddSubmit(Submit submit)
        {
            using (var context = new Context())
            {
                context.Submits.Add(submit);
                 context.SaveChanges();
            }
        }

        static void EditSubmit(Submit submit)
        {
            using (var context = new Context())
            {
                var sub = context.Submits.Where(g => g.Id == submit.Id).FirstOrDefault();
                sub.Sweep = submit.Sweep;
                sub.Wash = submit.Wash;
                sub.Trash = submit.Trash;
                sub.WhenChecked = submit.WhenChecked;
                context.SaveChanges();
            }
        }
    }
}
