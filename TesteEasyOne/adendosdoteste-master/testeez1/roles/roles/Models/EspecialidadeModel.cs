using DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace roles.Models {
    public class EspecialidadeModel {

        [Display(Name="Id da Especialidade")]
        public int Id { get; set; }

        [Required(ErrorMessage ="Informe o nome da Especialidade")]
        [Display(Name ="Nome da especialidade")]
        public String Nome { get; set; }

        [Display(Name ="Lista de Medicos desta especialidade")]
        public List<Medico> Medicos { get; set; }

        [Display(Name ="Descricao da especialidade")]
        public String Descricao { get; set; }

    }
}