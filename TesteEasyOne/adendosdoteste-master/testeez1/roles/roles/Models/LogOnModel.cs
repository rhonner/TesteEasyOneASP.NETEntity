
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace roles.Models {
    public class LogOnModel {

        [Required (ErrorMessage = "Por Favor informe o UserName")]
        [Display(Name ="UserName do usuario")]
        public String UserName { get; set; }

        [Required (ErrorMessage = "Por favor informe a senha")]
        [Display(Name ="Senha do Usuário")]
        public String Password { get; set; }
        
        [Display(Name ="ID do usuario")]
        public int UserId { get; set; }

        public int IdPerson { get; set; }
    }
}