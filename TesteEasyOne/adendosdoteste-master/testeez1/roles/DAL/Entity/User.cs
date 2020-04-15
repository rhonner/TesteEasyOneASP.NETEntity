using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity {
    public class User {

        public User() { }

        [Key]//primria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserId")]//nome da coluna no banco
        public int UserId { get; set; }

        [Required]// not null
        [StringLength(50)]// nvarchar(50)
        [Column("UserName")]
        public String UserName { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Password")]
        public String Password { get; set; }

        [Column("UserEmailAddress")]
        public String UserEmailAdress { get; set; }

        [NotMapped]
        public int IdPerson { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
        public virtual ICollection<Administrativo> Administrativos { get; set; }
        public virtual ICollection<Paciente> Paciente { get; set; }

    }

}
