using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DAL.Entity
{
    public class UsersContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public UsersContext() : base(ConfigurationManager.ConnectionStrings["aula"].ConnectionString)
        {
        }

        object placeHolderVariable;

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }

        public void AddUser(User user) {
            Users.Add(user);
            SaveChanges();
        }

        public User GetUser(string userName) {
            var user = Users.SingleOrDefault(u => u.UserName == userName);
            return user;
        }

        public User GetUser(string userName, string password) {
            var user = Users.SingleOrDefault(u => u.UserName ==
                           userName && u.Password == password);
            return user;
        }

        public void AddUserRole(UserRoles userRole) {
            var roleEntry = UserRoles.SingleOrDefault(r => r.UserId == userRole.UserId);
            if (roleEntry != null) {
                UserRoles.Remove(roleEntry);
                SaveChanges();
            }
            UserRoles.Add(userRole);
            SaveChanges();
        }

    }
}

