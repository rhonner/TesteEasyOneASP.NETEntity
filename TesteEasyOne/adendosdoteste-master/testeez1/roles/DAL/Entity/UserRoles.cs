using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity {
    public class UserRoles {

        [Key]
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public Int16 RoleId { get; set; }

        public virtual Role Role { get; set; }

    }
}
