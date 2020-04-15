using DAL.Persistence;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using roles.Models;
using roles.Relatorios;
using System.Data;
using System.IO;
using CrystalDecisions.Shared;
using roles.Util;

namespace roles.Controllers {
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller {
        // GET: User
        public ActionResult Index() {
            return View();
        }


        [HttpGet]
        public JsonResult ListagemConsulta() {
            List<Consulta> listagem = new List<Consulta>();

            

            ConsultaDal cDal = new ConsultaDal();
            listagem = cDal.GetAll();
            var asas = ConsultaDal.FindAll();

            var oConsulta = listagem.Select(x => new ConsultaModel() {
                Data = x.Data,
                Id = x.Id,
                Descricao = x.Descricao,
                Medico = x.Medico,
                Paciente = x.Paciente

            }).ToList();
            //ViewBag.Message = "Buscou todas as consultas ou não";

            return Json(oConsulta, JsonRequestBehavior.AllowGet);
        }


        //[HttpGet]
        //public JsonResult MostraDescricao() {
        //    String descricao;
        //}
        [HttpGet]
        public ActionResult MostrarConsulta(int id) {

            Consulta c = new Consulta();
            ConsultaModel model = new ConsultaModel();
            ConsultaDal cDal = new ConsultaDal();
            if (ModelState.IsValid) {
                try {
                    c = ConsultaDal.FindByKey(id);
                    c.Paciente = PacienteDal.FindByKey(c.IdPaciente);
                    c.Paciente.Sangue = SangueDal.FindByKey(c.Paciente.TipoSanguineo);
                    c.Medico = MedicoDal.FindByKey(c.IdMedico);
                    c.Medico.Especialidade = EspecialidadeDal.FindByKey(c.Medico.IdEspecialidade);
                    model.Id = c.Id;
                    model.Data = c.Data;
                    model.Descricao = c.Descricao;
                    model.Medico.Nome = c.Medico.Nome;
                    model.Medico.Id = c.Medico.Id;
                    model.Medico.Crm = c.Medico.Crm;
                    model.Medico.IdEspecialidade = c.Medico.IdEspecialidade;
                    model.Paciente.Id = c.Paciente.Id;
                    model.Paciente.Nome = c.Paciente.Nome;
                    model.Paciente.Sangue = c.Paciente.Sangue;
                    model.Paciente.Sexo = c.Paciente.Sexo;
                }
                catch (Exception e) {
                    ViewBag.Message = "Nao buscou UMA consulta" + e.ToString();
                }
            }
            return PartialView("_MostraUmaConsulta", model);
        }

        [HttpGet]
        public ActionResult MostrarPaciente(int id) {

            Paciente p = new Paciente();
            PacienteModel model = new PacienteModel();
            PacienteDal pDal = new PacienteDal();
            if (ModelState.IsValid) {
                try {
                    p = PacienteDal.FindByKey(id);
                    p.Sangue = SangueDal.FindByKey(p.TipoSanguineo);

                    model.Id = p.Id;
                    model.Nome = p.Nome;
                    model.Cpf = p.Cpf;
                    model.Email = p.Email;
                    model.Sangue = p.Sangue;
                    model.Sexo = p.Sexo;
                }
                catch (Exception e) {
                    ViewBag.Message = "Nao buscou UMA Paciente" + e.ToString();
                }
            }
            return PartialView("_MostraUmPaciente", model);
        }
        [HttpGet]
        public ActionResult MostrarMedico(int id) {

            Medico m = new Medico();
            MedicoModel model = new MedicoModel();
            MedicoDal mDal = new MedicoDal();
            if (ModelState.IsValid) {
                try {
                    m = MedicoDal.FindByKey(id);
                    m.Especialidade = EspecialidadeDal.FindByKey(m.IdEspecialidade);
                    model.Id = m.Id;
                    model.Nome = m.Nome;
                    model.Crm = m.Crm;
                    model.Especialidade.Id = m.IdEspecialidade;
                    model.Especialidade.Nome = m.Especialidade.Nome;
                    model.IdEspecialidade = m.IdEspecialidade;
                }
                catch (Exception e) {
                    ViewBag.Message = "Nao buscou UMA Medico" + e.ToString();
                }
            }
            return PartialView("_MostraUmMedico", model);
        }

        [HttpGet]
        public ActionResult MostrarEspecialidade(int id) {

            Especialidade es = new Especialidade();
            EspecialidadeModel model = new EspecialidadeModel();
            EspecialidadeDal eDal = new EspecialidadeDal();
            if (ModelState.IsValid) {
                try {
                    es = EspecialidadeDal.FindByKey(id);

                    model.Id = es.Id;
                    model.Nome = es.Nome;
                    model.Descricao = es.Descricao;
                }
                catch (Exception e) {
                    ViewBag.Message = "Nao buscou UMA Medico" + e.ToString();
                }
            }
            return PartialView("_MostraUmaEspecialidade",model);
        }

        [HttpGet]
        public ActionResult InitAlterarEspecialidadeMedico(int id) {

            Medico m = new Medico();
            MedicoModel model = new MedicoModel();
            //MedicoDal mDal = new MedicoDal();
            if (ModelState.IsValid) {
                try {
                    m = MedicoDal.FindByKey(id);
                    m.Especialidade = EspecialidadeDal.FindByKey(m.IdEspecialidade);
                    model.Id = m.Id;
                    model.Nome = m.Nome;
                    model.Crm = m.Crm;
                    model.Especialidade.Id = m.IdEspecialidade;
                    model.Especialidade.Nome = m.Especialidade.Nome;
                    model.IdEspecialidade = m.IdEspecialidade;
                    //EspecialidadeDal eDal = new EspecialidadeDal();
                    List<Especialidade> especialidades = new List<Especialidade>();
                    especialidades = EspecialidadeDal.FindAll();
                    var list = new SelectList(especialidades, "Id", "Nome");
                    ViewBag.Especialidades = list;
                }
                catch (Exception e) {
                    ViewBag.Message = "Nao buscou UMA Medico" + e.ToString();
                }
            }
            return PartialView("_AlterarEspecialidadeDoMedico", model);
        }

        [HttpPost]
        public ActionResult AlterarEspecialidadeMedico(MedicoModel model) {


            Medico m = new Medico();
            m = MedicoDal.FindByKey(model.Id);
            m.IdEspecialidade = model.IdEspecialidade;
            m.Especialidade = null;
            MedicoDal mDal = new MedicoDal();
            if (ModelState.IsValid) {
                try {
                    //mDal.AlterarEspecialidade(model.Id, model.IdEspecialidade);
                    MedicoDal.Atualizar(m);
                }
                catch (Exception e) {
                    ViewBag.Message = "Nao alterou UM Medico" + e.ToString();
                }
            }

            return RedirectToAction("Medicos");
        }


        [HttpGet]
        public ActionResult InitAlterarEspecialidade(int id) {

            Especialidade es = new Especialidade();
            EspecialidadeModel model = new EspecialidadeModel();
            EspecialidadeDal eDal = new EspecialidadeDal();
            if (ModelState.IsValid) {
                try {
                    es = EspecialidadeDal.FindByKey(id);
                    model.Id = es.Id;
                    model.Nome = es.Nome;
                    model.Descricao = es.Descricao;
                }
                catch (Exception e) {
                    ViewBag.Message = "Nao buscou UMA Especialidade" + e.ToString();
                }
            }
            return PartialView("_AlterarEspecialidade", model);

        }



        [HttpPost]
        public ActionResult AlterarEspecialidade(EspecialidadeModel model) {


            Especialidade es = new Especialidade();
            EspecialidadeDal eDal = new EspecialidadeDal();
            if (ModelState.IsValid) {
                try {
                    es.Id = model.Id;
                    es.Nome = model.Nome;
                    es.Descricao = model.Descricao;
                    EspecialidadeDal.Atualizar(es);
                }
                catch (Exception e) {
                    ViewBag.Message = "Nao alterou UM Medico" + e.ToString();
                }
            }

            return View("Especialidades");
        }


        public ActionResult CadastrarNovoPaciente() {

            SangueDal sDal = new SangueDal();
            List<Sangue> sangues = new List<Sangue>();
            sangues = SangueDal.FindAll();
            var list = new SelectList(sangues, "Id", "Tipo");
            ViewBag.Sangues = list;

            return PartialView("_CadastroPaciente");
        }


        public ActionResult CadastrarNovoMedico() {
            EspecialidadeDal eDal = new EspecialidadeDal();
            List<Especialidade> especialidades = new List<Especialidade>();
            especialidades = EspecialidadeDal.FindAll();
            var list = new SelectList(especialidades, "Id", "Nome");
            ViewBag.Especialidades = list;

            return PartialView("_CadastroMedico");
        }

        public ActionResult InitCadastrarEspecialidade() {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index", "Error");
            
            return PartialView("_CadastroEspecialidade");
        }


        [HttpPost]
        public ActionResult CadastrarPaciente(PacienteModel model) {
            if (ModelState.IsValid) {
                try {
                    Paciente p = new Paciente();
                    SangueDal sDal = new SangueDal();
                    //Sangue sn = sDal.GetOne(model.Sangue)
                    p.Nome = model.Nome;
                    p.Sexo = model.Sexo;
                    p.Cpf = model.Cpf;
                    p.Email = model.Email;
                    p.TipoSanguineo = model.TipoSanguineo;
                    p.Sangue = null;
                    PacienteDal.Inserir(p);
                }
                catch (Exception e) {
                    throw new Exception(e.ToString());
                }
            }
            return RedirectToAction("Pacientes");
        }
        [HttpPost]
        public ActionResult CadastrarMedico(MedicoModel model) {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index", "Error");
            if (ModelState.IsValid) {
                try {
                    Medico m = new Medico();
                    EspecialidadeDal eDal = new EspecialidadeDal();
                    //Sangue sn = sDal.GetOne(model.Sangue)
                    m.Nome = model.Nome;
                    m.Crm = model.Crm;
                    m.IdEspecialidade = model.IdEspecialidade;
                    m.Especialidade = null;

                    MedicoDal.Insertt(m);

                }
                catch (Exception e) {
                    throw new Exception(e.ToString());
                }
            }
            return RedirectToAction("Medicos");
        }

        [HttpPost]
        public ActionResult CadastrarEspecialidade(EspecialidadeModel model) {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index", "Error");

            if (ModelState.IsValid) {
                try {
                    Especialidade e = new Especialidade();
                    EspecialidadeDal eDal = new EspecialidadeDal();
                    e.Nome = model.Nome;
                    e.Descricao = model.Descricao;
                    EspecialidadeDal.Insertt(e);
                }
                catch (Exception e) {
                    throw new Exception(e.ToString());
                }
            }
            return RedirectToAction("Especialidades");
        }

        [HttpGet]
        public JsonResult DeletarConsulta(int id) {
            if (!Request.IsAuthenticated)
                return Json(new { Result = false, Msg = "Não autenticado!" });


            try {

                ConsultaDal cDal = new ConsultaDal();
                Consulta c = ConsultaDal.FindByKey(id);
                c.Medico = null;
                c.Paciente = null;
                ConsultaDal.Deletar(c);
            }
            catch (Exception e) {
                return Json(new { Result = false, Msg = "Erro ao excluir! - " + e.Message });

            }
            return Json(new { Result = true, Msg = "Apagado com sucesso!" }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult DeletarPaciente(int id) {
            if (!Request.IsAuthenticated)
                return Json(new { Result = false, Msg = "Não autenticado!" });
            try {
                PacienteDal pDal = new PacienteDal();

                pDal.Delete(PacienteDal.FindByKey(id));
            }
            catch (Exception e) {
                return Json(new { Result = false, Msg = "Erro ao excluir! - " + e.Message }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { Result = true, Msg = "Apagado com sucesso!" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletarMedico(int id) {
                if (!Request.IsAuthenticated)
                    return Json(new { Result = false, Msg = "Não autenticado!" });
            try {
                MedicoDal mDal = new MedicoDal();
                mDal.Deletar(id);
            }
            catch (Exception e) {
                return Json(new { Result = false, Msg = "Não autenticado!" });
            }
            return Json(new {Result = true, Msg = "Apagado Com Sucesso!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeletarEspecialidade(int id) {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index", "Error");
            try {
                //NAO TEM NECESSIDADE DE DELETAR ESPECIALIDADES
                //EspecialidadeDal eDal = new EspecialidadeDal();
                //eDal.Deletar(id);
            }
            catch (Exception e) {
                ViewBag.Message = "Nao deletou o medico " + e.ToString();
            }
            return View("Especialidades");
        }

        [HttpGet]
        public JsonResult ListagemPaciente() {
            List<Paciente> listagem = new List<Paciente>();
            try {

                PacienteDal pDal = new PacienteDal();
                listagem = PacienteDal.FindAll();
                listagem.ForEach(l => l.Sangue = SangueDal.FindByKey(l.TipoSanguineo));
                //ViewBag.Message = "Buscou todas as consultas ou não";
            }
            catch (Exception e) {
                ViewBag.Message = "Não buscou todas as consultas" + e.ToString();
            }
            return Json(listagem, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listagemMedicos() {
            List<Medico> listagem = new List<Medico>();

            MedicoDal mDal = new MedicoDal();
            listagem = mDal.GetAll();
            var oConsulta = listagem.Select(x => new MedicoModel() {
                Nome = x.Nome,
                Id = x.Id,
                Crm = x.Crm,
                Especialidade = x.Especialidade

            }).ToList();
            //ViewBag.Message = "Buscou todas as consultas ou não";

            return Json(oConsulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListagemEspecialidade() {
            List<Especialidade> listagem = new List<Especialidade>();
            EspecialidadeDal eDal = new EspecialidadeDal();
            listagem = eDal.GetAll();
            var oConsulta = listagem.Select(x => new EspecialidadeModel() {
                Id = x.Id,
                Nome = x.Nome,
                Descricao = x.Descricao
            }).ToList();
            //ViewBag.Message = "Buscou todas as consultas ou não";
            return Json(oConsulta, JsonRequestBehavior.AllowGet);
        }

        public FileStreamResult GerarAgendaDiaria() {
            try {
                //Passo 1) Executar a consulta na base de dados
                AgendaDiariaDal agDal = new AgendaDiariaDal(); //classe de persistencia
                List<AgendaDiaria> lista = agDal.getAll();
                //executar e retornar uma lista...

                //Passo 2) Carregar o conteudo no DataSet
                DataSet1 ds = new DataSet1();
                DataTable dt = ds.Tables["DataTable1"]; //acessando a DataTable

                //percorrer a lista de Clientes obtida do banco
                foreach (AgendaDiaria c in lista) {
                    DataRow registro = dt.NewRow();
                    registro["nomeMedico"] = c.NomeMedico;
                    registro["nomePaciente"] = c.NomePaciente;
                    registro["sexoPaciente"] = c.SexoPaciente;
                    registro["sanguePaciente"] = c.SanguePaciente;
                    registro["especialidade"] = c.EspecialidadeConsulta;
                    registro["descricaoConsulta"] = c.DescricaoConsulta;
                    registro["dataConsulta"] = c.DataConsulta;
                    dt.Rows.Add(registro); //gravo o registro na datatable
                }
                AgendinhaMedica rel = new AgendinhaMedica();
                rel.SetDataSource(dt); //populando o relatorio com a DataTable
                Stream arquivo = null; //download
                arquivo = rel.ExportToStream(ExportFormatType.PortableDocFormat);

                return File(arquivo, "application/pdf", "agendaDiaria.pdf");
                
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }

        public ActionResult LogOut() {
            Helper.GetUserId().IdPerson = 0;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Pacientes() {
            return View("Pacientes");
        }
        public ActionResult Medicos() {
            return View("Medicos");
        }
        public ActionResult Especialidades() {
            
            return View("Especialidades");
        }

    }
}