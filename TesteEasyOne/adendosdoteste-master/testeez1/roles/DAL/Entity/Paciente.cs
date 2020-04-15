using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity {
    [Table("paciente")]
    public class Paciente {

        public Paciente() { }


        [Column("idUser"), ForeignKey("User")]
        public int IdUser { get; set; }

        public virtual User User { get; set; } = new User();//pode ficar em loop

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("email")]
        public String Email { get; set; }


        [Required]
        [StringLength(150)]
        [Column("nome")]
        public String Nome { get; set; }

        [Required]
        [StringLength(11)]
        [Column("cpf")]
        public String Cpf { get; set; }

        [Required]
        [Column("TipoSanguineo"), ForeignKey("Sangue")]
        public int TipoSanguineo { get; set; }

        //[ForeignKey("TipoSanguineo")]
        public virtual Sangue Sangue { get; set; } = new Sangue();

        [Required]
        [StringLength(2)]
        [Column("sexo")]
        public String Sexo { get; set; }

        //public List<Consulta> Consultas { get; set; } /*= new List<Consulta>();*/

        public virtual ICollection<Consulta> Consultas{ get; set; }


    }

}
