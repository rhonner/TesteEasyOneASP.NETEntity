using DAL.Entity;
using DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace roles{
    public class CustomRoleProvider : RoleProvider{
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override bool IsUserInRole(string username, string roleName) {
            UserDal uDal = new UserDal();
            var user = uDal.GetUser(username);
            if (user == null)
                    return false;
                return user.UserRoles != null && user.UserRoles.Select(
                     u => u.Role).Any(r => r.RoleName == roleName);
        }

        public override string[] GetRolesForUser(string username) {
            UserDal uDal = new UserDal();
                var user = uDal.GetUser(username);
                if (user == null)
                    return new string[] { };
                return user.UserRoles == null ? new string[] { } :
                  user.UserRoles.Select(u => u.Role).Select(u => u.RoleName).ToArray();
        }

        // -- Snip --

        public override string[] GetAllRoles() {
            using (var usersContext = new UsersContext()) {
                return usersContext.Roles.Select(r => r.RoleName).ToArray();
            }
        }

        public override void CreateRole(string roleName) {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName) {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames) {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName) {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch) {
            throw new NotImplementedException();
        }
    }
}
