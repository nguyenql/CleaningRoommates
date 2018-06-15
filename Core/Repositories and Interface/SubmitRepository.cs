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
        }

        private void Restore() // reading data from database
        {
            Submits = Read();
        }

        static List<Submit> Read()
        {
            using (var context = new Context())
            {
                return context.Submits
                    .Include("Checker")
                    .Include("Executer")
                    .ToList();
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

        public void AddSubmit(Submit sub)
        {
            using (var context = new Context())
            {
                var submit = new Submit()
                {
                    Sweep = sub.Sweep,
                    Wash = sub.Wash,
                    Trash = sub.Trash,
                    WhenDone = sub.WhenDone,
                    Executer = context.Users.Single(x => x.Id == sub.Executer.Id),
                    Checker = context.Users.Single(x => x.Id == sub.Checker.Id)
                };

                context.Submits.Add(submit);
                context.SaveChanges();
            }
        }

        public void EditSubmit(Submit submit)
        {
            using (var context = new Context())
            {
                Submit sub = context.Submits.Single(g => g.Id == submit.Id);
                sub.Sweep = submit.Sweep;
                sub.Wash = submit.Wash;
                sub.Trash = submit.Trash;
                sub.WhenChecked = submit.WhenChecked;
                sub.AlreadyChecked = 1;
                context.SaveChanges();
            }
        }
    }
}
