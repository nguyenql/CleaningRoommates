﻿using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories_and_Interface
{
    public class UserRepository : Interface
    {
        public List<User> Users { get; set; }
        Context context = new Context();

        public UserRepository()
        {
            Restore();
            Save();
        }

        private void Restore() // reading data from database
        {
            Users = Read(context);
        }

        static List<User> Read(Context context)
        {
            var users = context.Users.Include("Room").ToList();
            int a = 0;
            foreach (var user in users)
            {
                user.IdForGala = a;
                a += 1;
            }
            return users ;
        }

        public void Save()
        {
            using (var context = new Context())
            {
                context.SaveChanges();
                Restore();
            }
        }

        static void AddUser(User user)
        {
            using (var context = new Context())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        static void EditUser(User user)
        {
            using (var context = new Context())
            {
                var us = context.Users.Where(g => g.Id == user.Id).FirstOrDefault();
                us.Name = user.Name;
                us.Login = user.Login;
                us.Password = user.Password;
                us.Room = user.Room;
                context.SaveChanges();
            }
        }
    }
}
