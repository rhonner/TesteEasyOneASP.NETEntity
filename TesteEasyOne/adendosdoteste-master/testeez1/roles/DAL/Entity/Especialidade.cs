using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity {
    [Table("especialidade")]
    public class Especialidade {

        public Especialidade() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nome")]
        public String Nome { get; set; }

        [StringLength(255)]
        [Column("descricao")]
        public String Descricao { get; set; }


        public ICollection<Medico> Medicos { get; set; }
    }
}
