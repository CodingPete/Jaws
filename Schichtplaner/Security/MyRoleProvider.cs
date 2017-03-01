using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Schichtplaner.Security
{
    public class MyRoleProvider : RoleProvider
    {
        private ServiceReference1.Service1Client client;
        public MyRoleProvider()
        {
            client = new ServiceReference1.Service1Client();
        }

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
            var rechte = client.getRechtbyRolleId(client.getPersonalbyEmail(username).RolleId);
            foreach(var item in rechte)
            {
                userrechte.Add(item.Name);
            }
            return userrechte.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userroles = this.GetRolesForUser(username);

            return userroles.Contains(roleName);
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