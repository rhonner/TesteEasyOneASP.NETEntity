using DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace roles.Models {
    public class MedicoModel {

        [Display(Name = "Id do Medico")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome do Medico")]
        [StringLength(150)]
        [Display(Name = "Nome do Medico")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Informe o crm do Medico, adm lixo")]
        [Display(Name = "Crm do Medico")]
        public int Crm { get; set; }

        [Required(ErrorMessage = "Informe a Especialidade do Medico")]
        [Display(Name = "Especialidade do Médico")]
        public Especialidade Especialidade { get; set; } = new Especialidade();

        [Display(Name ="idEspecialidade do Médico")]
        public int IdEspecialidade { get; set; }


        [Display(Name = "Lista de Consultas")]
        public List<Consulta> Consultas { get; set; }


    }
}