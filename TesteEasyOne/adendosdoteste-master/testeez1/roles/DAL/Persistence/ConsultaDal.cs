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
    public class ConsultaDal : GenericDal<Consulta, int> {

        //public void Inserir(Consulta c)
        //{
        //    try
        //    {
        //        using (Conexao Con = new Conexao())
        //        {

        //            Con.Entry(c).State = EntityState.Added;
        //            Con.SaveChanges();

        //        }
        //    }catch(Exception e)
        //    {
        //        throw new Exception(e.ToString());
        //    }
        //}

        public List<Consulta> GetAll() {
            List<Consulta> lista = new List<Consulta>();
            try {
                using (Conexao Con = new Conexao()) {
                    lista = Con.Consultas.OrderBy(c => c.Data).ToList();
                    lista.ForEach(p => p.Paciente = Con.Pacientes.AsNoTracking().FirstOrDefault(pc => pc.Id == p.IdPaciente));
                    lista.ForEach(p => p.Paciente.Sangue = Con.Sangues.AsNoTracking().FirstOrDefault(s => s.Id == p.Paciente.TipoSanguineo));
                    lista.ForEach(p => p.Medico = Con.Medicos.AsNoTracking().FirstOrDefault(m => m.Id == p.IdMedico));
                    lista.ForEach(p => p.Medico.Especialidade = Con.Especialidades.AsNoTracking().FirstOrDefault(e => e.Id == p.Medico.IdEspecialidade));
                    //    lista.Add(c);
                    //}
                    return lista;
                    //return Con.Consultas.ToList();
                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }
        public List<Consulta> GetAllOfOne(int id) {
            List<Consulta> lista = new List<Consulta>();
            try {
                using (Conexao Con = new Conexao()) {
                    lista = Con.Consultas.AsNoTracking().Where(x => x.IdPaciente == id).ToList();
                    lista.ForEach(p => p.Paciente = Con.Pacientes.AsNoTracking().FirstOrDefault(pc => pc.Id == p.IdPaciente));
                    lista.ForEach(p => p.Paciente.Sangue = Con.Sangues.AsNoTracking().FirstOrDefault(s => s.Id == p.Paciente.TipoSanguineo));
                    lista.ForEach(p => p.Medico = Con.Medicos.AsNoTracking().FirstOrDefault(m => m.Id == p.IdMedico));
                    lista.ForEach(p => p.Medico.Especialidade = Con.Especialidades.AsNoTracking().FirstOrDefault(e => e.Id == p.Medico.IdEspecialidade));
                    return lista;
                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }

        public Consulta GetOne(int idd) {
            try {
                using (Conexao Con = new Conexao()) {
                    Consulta c = new Consulta();
                    c = Con.Consultas.AsNoTracking().FirstOrDefault(x => x.Id == idd);
                    c.Medico = Con.Medicos.AsNoTracking().FirstOrDefault(m => m.Id == c.IdMedico);
                    c.Medico.Especialidade = Con.Especialidades.AsNoTracking().FirstOrDefault(e => e.Id == c.Medico.IdEspecialidade);
                    c.Paciente = Con.Pacientes.AsNoTracking().FirstOrDefault(p => p.Id == c.IdPaciente);
                    return c;
                }

            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }

        //public void Delete(Consulta c) {
        //    try {
        //        using (Conexao Con = new Conexao()) {
        //            //String sql = "DELETE FROM consulta WHERE id=@idd";
        //            //conn.Open();
        //            //SqlCommand cmd = new SqlCommand(sql, conn);
        //            //cmd.Parameters.AddWithValue("@idd", c.Id);
        //            //cmd.ExecuteNonQuery();
        //            ConsultaDal.Deletar
        //        }
        //    }
        //    catch (Exception e) {
        //        throw new Exception("Erro ao Deletar Consulta: " + e.ToString());
        //    }
        //}

        //public static void DeletePacienteX(int id) {
        //    try {
        //        using(Conexao Con = new Conexao()) {
        //            Con.Consultas.AsNoTracking().Where(c => c.IdPaciente == id).ToList();
                    
        //        }
        //    }
        //    catch (Exception e) {

        //        throw new Exception (e.ToString());
        //    }
        //}



        //public void DeleteMedicos(int id) {
        //    try {
        //        using (Conexao Con = new Conexao()) {
        //            Con.Entry(Con.Consultas.AsNoTracking().FirstOrDefault(c => c.IdMedico == id)).State = EntityState.Deleted;
        //        }
        //    }
        //    catch (Exception e) {
        //        throw new Exception("Erro ao Deletar Consulta: " + e.ToString());
        //    }
        //}

        public bool HasMedic(int idM) {
            bool bule = false;
            try {
                using (Conexao Con = new Conexao()) {
                    if (Con.Consultas.AsNoTracking().Any(x => x.IdMedico == idM)) {
                        bule = true;
                    }
                }
            }
            catch (Exception e) {
                throw new Exception("erro ao buscar medico na consulta " + e.ToString());
            }
            return bule;
        }
    }
}
