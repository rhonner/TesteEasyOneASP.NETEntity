using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity {
    public class Role {

        [Key]
        public Int16 RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

    }

}

