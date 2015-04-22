using SimpleExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleExample.Controllers
{
    public class RolesController : Controller
    {
        //
        // GET: /Roles/
        IDataAccess<Role> Items { get; set; }

        public ActionResult Index()
        {
            Items = new RoleAccess();
            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public Object GetRoles()
        {
            Items = new RoleAccess();
            return Parser();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void DeleteRole(int Id)
        {
            Items = new RoleAccess();
            Role user = Items.GetAll.FirstOrDefault(x => x.Id.CompareTo(Id) == 0);
            Items.Delete(user);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void UpdateRole(int id, string name, string definition)
        {
            Items = new RoleAccess();
            Role role = Items.GetAll.FirstOrDefault(x => x.Id.CompareTo(id) == 0);
            role.Name = name;
            role.Definition = definition;
            Items.Update(role);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void CreateRole(string name, string definition)
        {
            Items = new RoleAccess();
            Role role = new Role();
            role.Id = Items.GetAll.Max(x => x.Id) + 1;
            role.Name = name;
            role.Definition = definition;
            Items.Create(role);
        }

        public Object Parser()
        {
            string result = "[";
            int i = 0;
            foreach (Role ent in Items.GetAll)
            {
                string updtBtn = "<input type='button' id = 'updateButton_" + i + "' value='Update' onclick = 'updateRole(" + i + ")' />",
                dltBtn = "<input type='button' id = 'deleteButton_" + i + "' value='Delete' onclick = 'deleteRole(" + i + ")' />";
                result += "[" + '"' + updtBtn + '"' + ','
                    + '"' + dltBtn + '"' + ','
                    + '"' + ent.Name + '"' + ','
                    + '"' + ent.Definition + '"' + ','
                    + '"' + ent.Id + '"' + ']';
                if (i < Items.GetAll.Count - 1)
                {
                    result += ','; i++;
                }
            }
            return result += ']';
        }

    }
}
