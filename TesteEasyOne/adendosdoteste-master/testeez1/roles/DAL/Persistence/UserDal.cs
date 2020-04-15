using DAL.Entity;
using DAL.Generic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace DAL.Persistence {
   public class UserDal : GenericDal<User, int> {

        //public void Insert(User u) {
        //    try {
        //        using(Conexao Con = new Conexao()) {
        //            Con.Entry(u).State = EntityState.Added;
        //            Con.SaveChanges();
        //        }
        //    }catch(Exception e) {
        //        throw new Exception(e.ToString());
        //    }
        //}

        

        public bool validateUser(String UserName, String Password) {
            try {
                using(Conexao Con = new Conexao()) {
                   return Con.Users.Where(l => l.UserName.Equals(UserName) && l.Password.Equals(Password)).Any();
                }
            }catch(Exception e) {
                throw new Exception(e.ToString());
            }
        }

        public User GetUser(string userName) {
            using (Conexao Con = new Conexao()) {
                var user = Con.Users.SingleOrDefault(u => u.UserName == userName);

                if (user != null) {
                    user.UserRoles = Con.UserRoles.AsNoTracking().Where(x => x.UserId == user.UserId).ToList();
                        user.UserRoles.ToList().ForEach(a => a.Role = Con.Roles.AsNoTracking().ToList().Where(x => x.RoleId == Convert.ToInt32(a.RoleId)).FirstOrDefault());
                    if (user.UserRoles.Any(x => x.RoleId == 2))
                        user.IdPerson = Con.Pacientes.AsNoTracking().FirstOrDefault(x => x.IdUser == user.UserId).Id;
                    else
                        user.IdPerson = Con.Administrativos.AsNoTracking().FirstOrDefault(x => x.IdUser == user.UserId).Id;
                }
                //foreach (var item in user.UserRoles) {
                //    item.Role = Con.Roles.Where(x => x.RoleId == item.RoleId).FirstOrDefault();
                //}

                return user;
            }
        }

        public User GetUser(string userName, string password) {
            using (Conexao Con = new Conexao()) {
                return Con.Users.SingleOrDefault(u => u.UserName ==
                               userName && u.Password == password);
            }
                
          
        }

        public void AddUserRole(UserRoles userRole) {

            using (Conexao Con = new Conexao()) {
                var roleEntry = Con.UserRoles.SingleOrDefault(r => r.UserId == userRole.UserId);
                if (roleEntry != null) {
                    Con.UserRoles.Remove(roleEntry);
                    Con.SaveChanges();
                }
                Con.UserRoles.Add(userRole);
                Con.SaveChanges();
            }

        }
    }
}
