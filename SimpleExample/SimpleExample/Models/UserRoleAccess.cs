using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleExample.Models
{
    public class UserRoleAccess:IDataAccess<UserRole>
    {
        List<UserRole> items;

        public SimpleDBEntities Items { get; set; }
        public UserRoleAccess()
        {
            Items = new SimpleDBEntities();
            items = Items.UserRoles.ToList();
        }
        public List<UserRole> GetAll
        {
            get { return items; }
        }

        public void Create(UserRole instance)
        {
            Items.UserRoles.Add(instance);
            Items.SaveChanges();
        }

        public bool Update(UserRole instance)
        {
            UserRole inst = Items.UserRoles.FirstOrDefault(x => x.Id == instance.Id);
            if (inst != null)
            {
                inst = instance;
                Items.SaveChanges();
                return true;
            }
            return false;
        }

        public void Delete(UserRole instance)
        {
            Items.UserRoles.Remove(instance);
            Items.SaveChanges();
        }

        public List<UserRole> Find(string item)
        {
            throw new NotImplementedException();
        }
    }
}