using DAL.Entity;
using DAL.Persistence;
using roles.Models;
using roles.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace roles.Controllers {
    [Authorize(Roles = "Paciente")]
    public class PacienteController : Controller {
        //pas = Session["pas"];
        // GET: Paciente
        public ActionResult Index() {
            PacienteDal pDal = new PacienteDal();
            Paciente p = PacienteDal.FindByKey(Helper.GetUserId().IdPerson);
            ViewBag.IdPaciente = p.Id;
            ViewBag.NomePaciente = p.Nome;
            return View();

        }

        public ActionResult InitIndex() {
            return RedirectToAction("Index", "Paciente");
        }





        [HttpGet]
        public JsonResult ListagemConsulta(int id) {
            List<Consulta> listagem = new List<Consulta>();
            try {

                ConsultaDal cDal = new ConsultaDal();
                listagem = cDal.GetAllOfOne(id);
                //ViewBag.Message = "Buscou todas as consultas ou não";
            }
            catch (Exception e) {
                ViewBag.Message = "Não buscou todas as consultas" + e.ToString();
            }
            return Json(listagem, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult MostrarConsulta(int id) {

            Consulta c = new Consulta();
            ConsultaDal cDal = new ConsultaDal();
            ConsultaModel model = new ConsultaModel();
            if (ModelState.IsValid) {
                try {
                    c = ConsultaDal.FindByKey(id);
                    c.Medico = MedicoDal.FindByKey(c.IdMedico);
                    c.Medico.Especialidade = EspecialidadeDal.FindByKey(c.Medico.IdEspecialidade);
                    c.Paciente = PacienteDal.FindByKey(c.IdPaciente);
                    c.Paciente.Sangue = SangueDal.FindByKey(c.Paciente.TipoSanguineo);
                    model.Id = c.Id;
                    model.Data = c.Data;
                    model.Descricao = c.Descricao;
                    model.Medico.Nome = c.Medico.Nome;
                    model.Medico.Id = c.Medico.Id;
                    model.Medico.Crm = c.Medico.Crm;
                    model.Medico.IdEspecialidade = c.Medico.IdEspecialidade;
                    model.Medico.Especialidade.Nome = c.Medico.Especialidade.Nome;
                    model.Paciente.Id = c.Paciente.Id;
                    model.Paciente.Nome = c.Paciente.Nome;
                    model.Paciente.Sangue = c.Paciente.Sangue;
                    model.Paciente.Sexo = c.Paciente.Sexo;
                }
                catch (Exception e) {
                    ViewBag.Message = "Nao buscou UMA Consulta" + e.ToString();
                }
                return PartialView("_MostraUmaConsulta", model);
            }
            else {
                return RedirectToAction("Index", "Error");
            }

        }
        //cDal.Delete(cDal.GetOne(id));
        [HttpGet]
        public JsonResult DeletarConsulta(int id) {
            if (!Request.IsAuthenticated)
                return Json(new { Result = false, Msg = "Não autenticado!" });
            try {
                ConsultaDal cDal = new ConsultaDal();
                Consulta c = cDal.GetOne(id);
                c.Paciente = null;
                c.Medico = null;
                ConsultaDal.Deletar(c);
            }
            catch (Exception e) {
                return Json(new { Result = false, Msg = "Erro ao excluir! - " + e.Message });

            }
            return Json(new { Result = true, Msg = "Apagado com sucesso!" }, JsonRequestBehavior.AllowGet);
            //try {
            //    ConsultaDal cDal = new ConsultaDal();
            //    cDal.Delete(cDal.GetOne(id));
            //}
            //catch (Exception e) {
            //    ViewBag.Message = "Nao deletou o cliente " + e.ToString();
            //}
            //return RedirectToAction("Index", "Paciente");
        }



        public ActionResult InitCadastrarConsulta() {


            MedicoDal sDal = new MedicoDal();
            List<Medico> medicos = new List<Medico>();
            medicos = MedicoDal.FindAll();
            var list = new SelectList(medicos, "Id", "Nome");
            ViewBag.medicos = list;

            return PartialView("_CadastrarConsulta");



        }

        //public ActionResult InitCadastrarConsultaData() {
        //    if (TempData["paciente"] == null) {
        //        return RedirectToAction("Index", "Error");
        //    }else {
        //        int idPaciente = (int)TempData["idPaciente"];
        //        int idd = (int)TempData["id"];
        //        TempData["id"] = idd;
        //        TempData["idPaciente"] = idPaciente;
        //        if (idPaciente != idd) {
        //            return RedirectToAction("Index", "Error");
        //        }
        //        else {
        //            TempData["paciente"] = "entrou";
        //            TempData["idPaciente"] = idPaciente;
        //            return View("CadastrarConsultaData");

        //        }
        //    }
        //    //MedicoDal sDal = new MedicoDal();
        //    //List<Medico> medicos = new List<Medico>();
        //    //medicos = sDal.GetAll();
        //    //var list = new SelectList(medicos, "Id", "Nome");
        //    //ViewBag.medicos = list;
        //}
        public ActionResult InitCadastrarConsultaEspecialidade() {

            EspecialidadeDal eDal = new EspecialidadeDal();
            List<Especialidade> especialidades = new List<Especialidade>();
            especialidades = EspecialidadeDal.FindAll();
            var list = new SelectList(especialidades, "Id", "Nome");
            ViewBag.Especialidades = list;


            return View("CadastrarConsultaEspecialidade");



        }
        //[HttpGet]
        //public ActionResult Init_CadastrarConsultaData(DateTime d)
        //{
        //    int idd = (int)TempData["id"];
        //    TempData["id"] = idd;
        //    MedicoDal sDal = new MedicoDal();
        //    string sqlFormattedDate = d.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //    List<Medico> medicos = new List<Medico>();
        //    medicos = sDal.GetAll();
        //    var list = new SelectList(medicos, "Id", "Nome");
        //    ViewBag.medicos = list;

        //    return View("_CadastrarConsultaData");
        //}

        [HttpGet]
        public ActionResult Init_CadastrarConsultaData() {



            //DateTime dt = DateTime.Parse(data);
            //string sqlFormattedDate = d.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //List<Medico> medicos = new List<Medico>();
            //MedicoDal sDal = new MedicoDal();
            //medicos = sDal.GetAllOfDate();
            //var list = new SelectList(medicos, "Id", "Nome");
            //ViewBag.medicos = list;

            return View("_CadastrarConsultaData");

        }

        [HttpGet]
        public JsonResult DropMedico(DateTime data) {
            List<Medico> medicos = new List<Medico>();
            try {
                MedicoDal sDal = new MedicoDal();
                medicos = sDal.GetAllOfDate(data);
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
            return Json(medicos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Init_CadastrarConsultaEspecialidade() {


            EspecialidadeDal eDal = new EspecialidadeDal();
            List<Especialidade> especialidades = new List<Especialidade>();
            especialidades = eDal.GetAll();
            var liste = new SelectList(especialidades, "Id", "Nome");
            ViewBag.Especialidades = liste;
            //MedicoDal mDal = new MedicoDal();
            //List<Medico> medicos = new List<Medico>();
            //medicos = mDal.GetAll();
            //ViewBag.Medicos = new SelectList(medicos, "Id", "Nome");
            return PartialView("_CadastrarConsultaEspecialidade");

        }

        [HttpGet]
        public JsonResult ObterMedicos(int id) {
            //if (TempData["paciente"] == null) {
            //    return RedirectToAction("Index", "Error");
            //}
            //TempData["paciente"] = "entrou";
            //MedicoDal mDal = new MedicoDal();
            //List<Medico> medicos = new List<Medico>();
            //medicos = mDal.GetAllOfEsp(id);
            //ViewBag.Medicos = new SelectList(medicos, "Id", "Nome");
            //return Json(medicos.ToList(), JsonRequestBehavior.AllowGet);
            List<Medico> medicos = new List<Medico>();
            try {
                MedicoDal mDal = new MedicoDal();
                medicos = mDal.GetAllOfEsp(id);
                var list = new SelectList(medicos, "Id", "Nome");
                ViewBag.Medicos = list;
            }
            catch (Exception e) {

                throw new Exception(e.ToString());
            }


            return Json(medicos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CadastrarConsulta(ConsultaModel model) {

            var oPaciente = Helper.GetUserId();
            //int idd = (int)TempData["id"];
            //TempData["id"] = idd;
            //if (idPaciente != idd) {
            //    return RedirectToAction("Index", "Error");
            //}
            //else {
            //TempData["paciente"] = "entrou";
            //TempData["id"] = idd;
            //TempData["idPaciente"] = idPaciente;
            if (ModelState.IsValid) {
                try {
                    Consulta c = new Consulta();
                    ConsultaDal cDal = new ConsultaDal();
                    //Sangue sn = sDal.GetOne(model.Sangue)
                    c.IdMedico = model.IdMedico;
                    c.Descricao = model.Descricao;
                    c.Data = model.Data;
                    MedicoDal mDal = new MedicoDal();
                    c.Medico = MedicoDal.FindByKey(c.IdMedico);
                    PacienteDal pDal = new PacienteDal();
                    c.Paciente = pDal.GetOne(oPaciente.IdPerson);
                    c.IdPaciente = oPaciente.IdPerson;
                    c.Paciente = null;
                    c.Medico = null;
                    ConsultaDal.Insertt(c);
                    return RedirectToAction("Index", "Paciente", new { id = oPaciente.IdPerson });
                }
                catch (Exception e) {
                    throw new Exception(e.ToString());
                }
            }
            return RedirectToAction("Index", "Paciente", new { id = oPaciente.IdPerson });




        }

        public ActionResult LogOut() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }

        //public ActionResult Medicos() {
        //    if (TempData["paciente"] == null) {
        //        return RedirectToAction("Index", "Error");
        //    }
        //    TempData["paciente"] = "entrou";
        //    int idd = (int)TempData["id"];
        //    TempData["id"] = idd;
        //    return View("Medicos");
        //}
    }
}
