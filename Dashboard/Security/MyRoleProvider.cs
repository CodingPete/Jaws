using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Dashboard.Security
{
    public class MyRoleProvider : RoleProvider
    {
        private DataContainer db = new DataContainer();
       
        public override string ApplicationName
        {
            get;
            set;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<String> userrechte = new List<String>();
            try
            {
                var person = db.PersonalSet.First((x) => x.email.Equals(username));
                var urechte = (from a in db.RechtSet
                            join c in db.RolleRechtSet on a.Id equals c.RechtId
                            where c.RolleId == person.RolleId
                            select a).ToList();

                foreach (var item in urechte)
                {
                    userrechte.Add(item.Name);
                }
                return userrechte.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userroles = this.GetRolesForUser(username);

            return userroles!=null && userroles.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }

}