using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleExample.Models
{
    public class RoleAccess: IDataAccess<Role>
    {
        List<Role> items;
        public SimpleDBEntities Items { get; set; }
        public RoleAccess()
        {
            Items = new SimpleDBEntities();
            items = Items.Roles.ToList();
        }
        public List<Role> GetAll
        {
            get { return items; }
        }

        public void Create(Role instance)
        {
            Items.Roles.Add(instance);
            Items.SaveChanges();
        }

        public bool Update(Role instance)
        {
            Role inst = Items.Roles.FirstOrDefault(x => x.Id == instance.Id);
            if (inst != null)
            {
                inst = instance;
                Items.SaveChanges();
                return true;
            }
            return false;
        }

        public void Delete(Role instance)
        {
            Items.Roles.Remove(instance);
            Items.SaveChanges();
        }

        public List<Role> Find(string item)
        {
            throw new NotImplementedException();
        }
    }
}