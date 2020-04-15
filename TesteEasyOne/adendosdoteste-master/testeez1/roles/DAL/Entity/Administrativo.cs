using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity {
    [Table("Administrativo")]
    public class Administrativo {
        public Administrativo() {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column ("id")]
        public int Id { get; set; }

        [Column("idUser")]
        public int IdUser { get; set; }

        /*[ForeignKey("IdUser")]*/ //nome da variavel acima (nao do campo no banco)
        public virtual User User { get; set; } = new User();

        [Required]
        [StringLength(150)]
        [Column("nome")]
        public String Nome { get; set; }
    }
}
