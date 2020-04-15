using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity {
    [Table("consulta")]
    public class Consulta {
        public Consulta() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("idMedico"), ForeignKey("Medico")]
        public int IdMedico { get; set; }

        public virtual Medico Medico { get; set; } = new Medico();

        [Column("idPaciente"), ForeignKey("Paciente")]
        public int IdPaciente { get; set; }

        public virtual Paciente Paciente { get; set; } = new Paciente();

        [Required]
        [Column("descricao")]
        public String Descricao { get; set; }

        [Required]
        [Column("dataa")]
        public DateTime Data { get; set; }

        //public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
