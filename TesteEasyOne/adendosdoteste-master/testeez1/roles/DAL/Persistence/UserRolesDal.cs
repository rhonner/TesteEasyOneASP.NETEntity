using DAL.Entity;
using DAL.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence {
    class UserRolesDal : GenericDal<UserRoles, int>{
        public static UserRoles FindByUserId(int iduser) {
            try {
                using(Conexao Con = new Conexao()) {
                    return Con.UserRoles.AsNoTracking().FirstOrDefault(ur => ur.UserId == iduser);
                }
            }
            catch (Exception e) {
                throw new Exception (e.ToString());
            }
        }
    }
}
