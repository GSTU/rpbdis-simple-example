using SimpleExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleExample.Controllers
{
    public class UserRolesController : Controller
    {
        //
        // GET: /UserRoles/
        IDataAccess<UserRole> Items { get; set; }

        public ActionResult Index()
        {
            Items = new UserRoleAccess();
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public Object GetUserRoles()
        {
            Items = new UserRoleAccess();
            return Parser();
        }

        public Object Parser()
        {
            string result = "[";
            int i = 0;
            foreach (UserRole ent in Items.GetAll)
            {
                result += "[" + '"' + ent.User.Login + '"' + ','
                    + '"' + ent.Role.Name + '"' + ']';
                if (i < Items.GetAll.Count - 1)
                {
                    result += ','; i++;
                }
            }
            return result += ']';
        }

    }
}
