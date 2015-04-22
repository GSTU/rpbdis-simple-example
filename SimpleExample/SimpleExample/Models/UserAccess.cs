using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleExample.Models
{
    public class UserAccess:IDataAccess<User>
    {
        List<User> items;

        public SimpleDBEntities Items { get; set; }
        public UserAccess()
        {
            Items = new SimpleDBEntities();
            items = Items.Users.ToList();
        }
        public List<User> GetAll
        {
            get { return items; }
        }

        public void Create(User instance)
        {
            Items.Users.Add(instance);
            Items.SaveChanges();
        }

        public bool Update(User instance)
        {
            User inst = Items.Users.FirstOrDefault(x => x.Id == instance.Id);
            if (inst != null)
            {
                inst = instance;
                Items.SaveChanges();
                return true;
            }
            return false;
        }

        public void Delete(User instance)
        {
            Items.Users.Remove(instance);
            Items.SaveChanges();
        }

        public List<User> Find(string item)
        {
            throw new NotImplementedException();
        }
    }
}