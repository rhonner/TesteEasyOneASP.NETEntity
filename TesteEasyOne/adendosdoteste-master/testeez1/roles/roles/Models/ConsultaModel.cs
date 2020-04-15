using DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace roles.Models
{
    public class ConsultaModel
    {

        [Display(Name = "Id da consulta")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecione um dos médicos disponiveis (MODELCONSULTA)")]
        [Display(Name = "Médico da consulta")]
        public Medico Medico { get; set; } = new Medico(); //se der erro, é loop de instanciação

        [Required(ErrorMessage = "Selecione o paciente desta consulta (MODELCONSULTA)")]
        [Display(Name = "Paciente da consulta")]
        public Paciente Paciente { get; set; } = new Paciente();

        [Display(Name = "Descrição da consulta")]
        public String Descricao { get; set; }

        [Required(ErrorMessage = "Informe a data da consulta (MODELCONSULTA)")]
        [Display(Name = "Data da consulta")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "qual o medico ?")]
        public int IdMedico { get; set; }

        [Required(ErrorMessage = "qual o especialidade ?")]
        public int IdEspecialidade { get; set; }

    }
}