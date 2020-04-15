using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity {
    [Table("agendaMedica")]
    public class AgendaDiaria {
        [Key]
        [Column ("nomeMedico")]
        public String NomeMedico { get; set; }

        [Column("nomePaciente")]
        public String NomePaciente { get; set; }

        [Column("sexoPaciente")]
        public String SexoPaciente { get; set; }

        [Column("sanguePaciente")]
        public String SanguePaciente { get; set; }

        [Column("consultaDescricao")]
        public String DescricaoConsulta { get; set; }

        [Column("especialidade")]
        public String EspecialidadeConsulta { get; set; }

        [Column("dataConsulta")]
        public DateTime DataConsulta { get; set; }

    }
}
