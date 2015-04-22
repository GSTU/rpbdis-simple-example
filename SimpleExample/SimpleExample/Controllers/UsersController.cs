using Newtonsoft.Json;
using SimpleExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleExample.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        IDataAccess<User> Items { get; set; }

        [AcceptVerbs(HttpVerbs.Get)]
        public Object GetUsers()
        {
            Items = new UserAccess();
            return Parser();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void DeleteUser(Guid Id)
        {
            Items = new UserAccess();
            User user = Items.GetAll.FirstOrDefault(x => x.Id.CompareTo(Id) == 0);
            Items.Delete(user);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void UpdateUser(Guid id, string login, string email, string password, string avatarPath, int role)
        {
            Items = new UserAccess();
            User user = Items.GetAll.FirstOrDefault(x => x.Id.CompareTo(id) == 0);
            user.Login = login;
            user.Email = email;
            user.Password = password;
            user.LastVisitDate = DateTime.UtcNow.ToLocalTime();
            user.AvatarPath = avatarPath;
            Items.Update(user);
            IDataAccess<UserRole> items = new UserRoleAccess();
            UserRole uR = items.GetAll.FirstOrDefault(x => x.UserId.CompareTo(id) == 0);
            uR.RoleId = role;
            items.Update(uR);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void CreateUser(string login, string email, string password, string avatarPath, int role)
        {
            Items = new UserAccess();
            User user = new User();
            user.Id = Guid.NewGuid();
            user.Login = login;
            user.Email = email;
            user.Password = password;
            user.RegisterDate = DateTime.UtcNow.ToLocalTime();
            user.LastVisitDate = DateTime.UtcNow.ToLocalTime();
            user.AvatarPath = avatarPath;
            Items.Create(user);
            IDataAccess<UserRole> items = new UserRoleAccess();
            UserRole uR = new UserRole();
            uR.Id = Guid.NewGuid();
            uR.UserId = user.Id;
            uR.RoleId = role;
            items.Create(uR);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public Object GetRoles()
        {
            IDataAccess<Role> items = new RoleAccess();
            return JsonConvert.SerializeObject(items.GetAll);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public Object GetCurrentRole(Guid Id)
        {
            IDataAccess<UserRole> items = new UserRoleAccess();
            UserRole usRl = items.GetAll.FirstOrDefault(x => x.UserId.CompareTo(Id) == 0);
            return JsonConvert.SerializeObject(usRl);
        }

        public Object Parser()
        {
            string result = "[";
            int i = 0;
            foreach (User ent in Items.GetAll)
            {
                string updtBtn = "<input type='button' id = 'updateButton_" + i + "' value='Update' onclick = 'updateUser(" + i + ")' />",
                dltBtn = "<input type='button' id = 'deleteButton_" + i + "' value='Delete' onclick = 'deleteUser(" + i + ")' />";
                result += "[" + '"' + updtBtn + '"' + ','
                    + '"' + dltBtn + '"' + ','
                    + '"' + ent.Login + '"' + ','
                    + '"' + ent.Email + '"' + ','
                    + '"' + ent.Password + '"' + ','
                    + '"' + ent.RegisterDate + '"' + ','
                    + '"' + ent.LastVisitDate + '"' + ','
                    + '"' + ent.AvatarPath + '"' + ','
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
