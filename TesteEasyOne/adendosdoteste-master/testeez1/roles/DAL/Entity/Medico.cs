using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity {
    [Table("medico")]
    public class Medico {

        public Medico() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Column("nome")]
        public String Nome { get; set; }

        [Required]
        [Column("crm")]
        public int Crm { get; set; }

        [Column("idEspecialidade"), ForeignKey("Especialidade")]
        public int IdEspecialidade { get; set; }

        //[ForeignKey("IdEspecialidade")]
        public virtual Especialidade Especialidade { get; set; } = new Especialidade();

        public virtual ICollection<Consulta> Consultas { get; set; }
    }
}
