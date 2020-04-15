    using DAL.Entity;
using DAL.Generic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence {
    public class MedicoDal : GenericDal<Medico, int> {

        //public void Inserir(Medico m) {
        //    try {
        //        using (Conexao Con = new Conexao()) {

        //            Con.Medicos.Add(m);
        //            Con.SaveChanges();
        //        }
        //    }
        //    catch (Exception e) {
        //        throw new Exception(e.ToString());
        //    }
        //}

        public void Deletar(int id) {
            try {
                using (Conexao Con = new Conexao()) {

                    Medico md = new Medico();
                    List<Consulta> consultis = new List<Consulta>(); 
                    consultis = Con.Consultas.Where(c => c.IdMedico == id).ToList(); //pega todas as consultas do medico x
                    consultis.ForEach(p => p.Medico = null);
                    consultis.ForEach(p => p.Paciente = null);//coloca nas consultas o objeto medico
                    //PacienteDal xisDal = new PacienteDal();
                    //List<Paciente> pacientis = xisDal.GetAllOfConsult(consultis.First().IdMedico); //cria uma lista de pacientes que tenham consulta com o medico x para colocar nas consultas da consulta.
                    //consultis.ForEach(p => p.Pacientes = pacientis);//adiciona 
                    Con.Consultas.RemoveRange(consultis);
                    Con.SaveChanges();
                    md = Con.Medicos.FirstOrDefault(t => t.Id == id);
                    md.Especialidade = null;

                    Con.Medicos.Remove(md);
                    Con.SaveChanges();
                }
            }
            catch (Exception e) {
                throw new Exception("Erro ao Deletar Consulta: " + e.ToString());
            }
        }

        public List<Medico> GetAll() {
            List<Medico> lista = new List<Medico>();
            try {
                using (Conexao Con = new Conexao()) {
                    lista = Con.Medicos.ToList();
                    lista.ForEach(x => x.Especialidade = Con.Especialidades.FirstOrDefault(a => a.Id == x.IdEspecialidade));

                    return lista;
                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }

        public List<Medico> GetAllOfDate(DateTime d) {
            List<Medico> lista = new List<Medico>();
            try {
                using (Conexao Con = new Conexao()) {
                    String sql = @"select DISTINCT m.* from medico m, especialidade e, consulta c 
                        where (m.id = c.idMedico or m.id not in (select c.idMedico from consulta c)) and c.dataa != '{0}'
                        and m.idEspecialidade = e.id 
                        and c.idMedico not in (select m.id from medico m, consulta c where m.id = c.idMedico and c.dataa 
                        between dateadd(MINUTE, -30, '{0}') and dateadd(MINUTE, +30, '{0}')  )";

                    lista = Con.Database.SqlQuery<Medico>(string.Format(sql, d.ToString("yyyy-MM-dd HH:mm:ss"))).ToList();

                    lista.ForEach(x => x.Especialidade = Con.Especialidades.AsNoTracking().FirstOrDefault(e => e.Id == x.IdEspecialidade));

                    return lista;
                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }
        public List<Medico> GetAllOfEsp(int idd) {
            List<Medico> lista = new List<Medico>();
            try {
                using (Conexao Con = new Conexao()) {
                    lista = Con.Medicos.Where(m => m.IdEspecialidade == idd).ToList();

                    return lista;
                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }

        //public Medico GetOne(int m) {
        //    Medico md = new Medico();
        //    try {
        //        using (Conexao Con = new Conexao()) {
        //            try {
        //                md = Con.Medicos.FirstOrDefault(x => x.Id == m);
        //                md.Especialidade = Con.Especialidades.FirstOrDefault(z => z.Id == md.IdEspecialidade);
        //            }
        //            catch (Exception e) {
        //                throw new Exception(e.ToString());
        //            }
        //            return md;
        //        }
        //    }
        //    catch (Exception e) {
        //        throw new Exception(e.ToString());
        //    }
        //}

        public void AlterarEspecialidade(int m,int e) {
            Medico md = new Medico();
            try {
                using (Conexao Con = new Conexao()) {
                    
                    var result = Con.Medicos.SingleOrDefault(x => x.Id == m);
                    if (result != null) {
                        try {
                            result.IdEspecialidade = e;
                            result.Especialidade = Con.Especialidades.FirstOrDefault(f => f.Id == e);
                            Con.Entry(result).State = EntityState.Modified;
                            Con.SaveChanges();
                        }
                        catch (Exception z) {

                            throw new Exception (z.ToString());
                        }
                    }
                }
            }
            catch (Exception) {

                throw;
            }
        }
    }
}
