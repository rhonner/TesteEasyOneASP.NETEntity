using DAL.Entity;
using DAL.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace roles.Models {
    public class PacienteModel {
        [Display(Name ="Id do Paciente")]
        public int Id { get; set; }
        [Required(ErrorMessage ="Informe o nome do Paciente")]
        [StringLength(150)]
        [Display(Name ="Nome do Paciente")]
        public String Nome { get; set; }

        [Required(ErrorMessage ="Informe o cpf do Paciente, adm lixo")]
        [Display(Name ="Cpf do Paciente")]
        [StringLength(15)]
        public String Cpf { get; set; }

        [Required(ErrorMessage ="Informe o email do Paciente")]
        [Display(Name ="Email do Paciente")]
        [StringLength(100)]
        public String Email { get; set; }

        [Required(ErrorMessage = "Informe o tipo sanguineo do Paciente")]
        [Display(Name = "Tipo sanguineo do Paciente")]
        public Sangue Sangue { get; set; } = new Sangue();

        [Display(Name ="Tipo Sanguineo")]
        public int TipoSanguineo { get; set; }

        [Display(Name ="Lista de Consultas")]
        public List<Consulta>Consultas { get; set; }

        [Display(Name ="Sexo do Paciente")]
        public String Sexo { get; set; }

    }

    //public class EmailExistente : ValidationAttribute {
    //    //sobrescrever um método da classe ValidationAttribute para
    //    //programar a regra de validação (IsValid)
    //    public override bool IsValid(object value) {
    //        //object value -> representa o valor do campo que será validado
    //        //converter o parametro 'value' de object para string...
    //        string cpf = (string)value;

    //        //verificar na base de dados se este email está disponivel...
    //        //se este método IsValid retornar true, significa que 
    //        //a validação 'passou'
    //        //se retornar falso, gera um erro de validação...
    //        PacienteDal pDal = new PacienteDal();
    //        //se o email existir no banco, a validação deverá retornar falso
    //        //se o email nao existir no banco, a validação retorna verdadeiro
    //        return !pDal.hasCpf(cpf);
    //    }
    }
