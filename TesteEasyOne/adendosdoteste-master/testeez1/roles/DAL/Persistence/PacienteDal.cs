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
    public class PacienteDal :GenericDal<Paciente, int> {

        public static void Inserir(Paciente p) {
            try {
                using (Conexao Con = new Conexao()) {
                    User u = new User(); //criar user
                    u.UserName = p.Nome; //setar valor de username = nome do paciente no user
                    u.Password = p.Cpf;  //setar valor de senha = cpf do paciente no user
                    u.UserEmailAdress = p.Email;
                    p.User = u; //setar paciente.user como null
                    //UserDal.Insertt(u); //adicionar user no banco
                    PacienteDal.Insertt(p); //adicionar paciente no banco
                    
                     //setar valor de email = email do paciente no user
                    p.IdUser = u.UserId; //setar paciente.idUser como user.UserId
                    UserRoles uroles = new UserRoles(); //criar novo userRoles
                    uroles.RoleId = 2; //userRoles.roleId recebe valor 2 pois paciente é usuario do tipo 2
                    uroles.UserId = u.UserId; //userRoles.userId recebe valor de user.userId
                    UserRolesDal.Insertt(uroles);
                    Con.SaveChanges();//atirar bala de comandos engatilhada
                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());

            }
        }

        //public void Deletar(Paciente p) {
        //    try {
        //        using (Conexao Con = new Conexao()) {
        //            Con.Entry(p).State = EntityState.Deleted;
        //            Con.SaveChanges();
        //        }
        //    }
        //    catch (Exception e) {
        //        throw new Exception(e.ToString());
        //    }
        //}

        public void Delete(Paciente p) {
            try {
                using (Conexao Con = new Conexao()) {
                    p.User = null;
                    p.Sangue = null;
                    User u = new User();
                    List<Consulta> lista = Con.Consultas.AsNoTracking().Where(c => c.IdPaciente == p.Id).ToList();
                    lista.ForEach(l => l.Paciente.User = null);
                    lista.ForEach(l => l.Medico = null);
                    lista.ForEach(l => l.Paciente = p);

                    foreach (var item in lista) {
                        Con.Entry(item).State = EntityState.Deleted;
                    }
                    Con.Entry(p).State = EntityState.Deleted;
                    Con.SaveChanges();
                    UserRoles urs = UserRolesDal.FindByUserId(p.IdUser);
                    UserRolesDal.Deletar(urs);
                    UserDal.Deletar(UserDal.FindByKey(p.IdUser));
                    Con.SaveChanges();
                }
            }
            catch (Exception e) {
                throw new Exception("Erro ao Deletar Consulta: " + e.ToString());
            }
        }

        public void Alterar(Paciente p) {
            try {
                using (Conexao Con = new Conexao()) {
                    Con.Entry(p).State = EntityState.Modified;
                    Con.SaveChanges();
                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }

        public Paciente GetOne(int p) {
            Paciente pc = new Paciente();
            try {
                using (Conexao Con = new Conexao()) {
                    //String sql = "select p.id,p.nome,p.cpf,p.email,p.sexo,p.idUser,p.tiposanguineo,s.id as sid,s.tipo as stipo  from paciente p, sangue s where p.id = @idd and p.tiposanguineo = s.id";
                    //SqlCommand cmd = new SqlCommand(sql, conn);
                    //cmd.Parameters.AddWithValue("@idd", p);
                    //conn.Open();
                    //SqlDataReader dr = cmd.ExecuteReader();
                    //if (dr.Read()) {
                    //    pc.Id = int.Parse(dr["id"].ToString());
                    //    pc.Sangue.Id = int.Parse(dr["sid"].ToString());
                    //    pc.Nome = dr["nome"].ToString();
                    //    pc.Email = dr["email"].ToString();
                    //    pc.IdUser = int.Parse(dr["idUser"].ToString());
                    //    pc.Sexo = dr["sexo"].ToString();
                    //    pc.TipoSanguineo = int.Parse(dr["tiposanguineo"].ToString());
                    //    pc.Sangue.Tipo = dr["stipo"].ToString();
                    //    pc.Cpf = dr["cpf"].ToString();
                    //}
                    pc.Sangue = null;
                    pc = Con.Pacientes.AsNoTracking().FirstOrDefault(pt => pt.Id == p);
                    return pc;
                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }

        }
        //public Paciente GetOneToSession(int p) {
        //    Paciente pc = new Paciente();
        //    try {
        //        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["aula"].ConnectionString)) {
        //            String sql = "select p.id,p.nome,p.cpf,p.email,p.sexo,p.tiposanguineo,s.id as sid,s.tipo as stipo  from paciente p, sangue s where p.idUser = @idd and p.tiposanguineo = s.id";
        //            SqlCommand cmd = new SqlCommand(sql, conn);
        //            cmd.Parameters.AddWithValue("@idd", p);
        //            conn.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            if (dr.Read()) {
        //                pc.Id = int.Parse(dr["id"].ToString());
        //                pc.Sangue.Id = int.Parse(dr["sid"].ToString());
        //                pc.Nome = dr["nome"].ToString();
        //                pc.Email = dr["email"].ToString();
        //                pc.Sexo = dr["sexo"].ToString();
        //                pc.TipoSanguineo = int.Parse(dr["tiposanguineo"].ToString());
        //                pc.Sangue.Tipo = dr["stipo"].ToString();
        //                pc.Cpf = dr["cpf"].ToString();
        //            }
        //            return pc;
        //        }
        //    }
        //    catch (Exception e) {
        //        throw new Exception(e.ToString());
        //    }

        //}

        public List<Paciente> GetAll() {
            List<Paciente> lista = new List<Paciente>();
            try {
                using (Conexao Con = new Conexao()) {
                    //String sql = "select p.id,p.nome,p.cpf,p.email,p.sexo,p.tiposanguineo,s.id as sid,s.tipo as stipo  from paciente p, sangue s where p.tiposanguineo = s.id";
                    //SqlCommand cmd = new SqlCommand(sql, conn);
                    //conn.Open();
                    //SqlDataReader dr = cmd.ExecuteReader();
                    //while (dr.Read()) {
                    //    Paciente pc = new Paciente();
                    //    pc.Id = int.Parse(dr["id"].ToString());
                    //    pc.Sangue.Id = int.Parse(dr["sid"].ToString());
                    //    pc.Nome = dr["nome"].ToString();
                    //    pc.Email = dr["email"].ToString();
                    //    pc.Sexo = dr["sexo"].ToString();
                    //    pc.TipoSanguineo = int.Parse(dr["tiposanguineo"].ToString());
                    //    pc.Sangue.Tipo = dr["stipo"].ToString();
                    //    pc.Cpf = dr["cpf"].ToString();
                    //    lista.Add(pc);
                    //}
                    lista = Con.Pacientes.ToList();
                    lista.ForEach(p => p.Sangue = Con.Sangues.AsNoTracking().FirstOrDefault(s => s.Id == p.TipoSanguineo));
                    return lista;
                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }

        //public List<Paciente> GetAllOfConsult(int idpac) {
        //    List<Paciente> lista = new List<Paciente>();
        //    try {
        //        using (Conexao Con = new Conexao()) {
        //            //String sql = "select p.id,p.nome,p.cpf,p.email,p.sexo,p.tiposanguineo,s.id as sid,s.tipo as stipo  from paciente p, sangue s, consulta c where p.tiposanguineo = s.id and p.id = c.idPaciente " +
        //            //    "and c.idMedico = @idPac";
        //            //SqlCommand cmd = new SqlCommand(sql, conn);
        //            //cmd.Parameters.AddWithValue("@idPac",idpac);
        //            //conn.Open();
        //            //SqlDataReader dr = cmd.ExecuteReader();
        //            //while (dr.Read()) {
        //            //    Paciente pc = new Paciente();
        //            //    pc.Id = int.Parse(dr["id"].ToString());
        //            //    pc.Sangue.Id = int.Parse(dr["sid"].ToString());
        //            //    pc.Nome = dr["nome"].ToString();
        //            //    pc.Email = dr["email"].ToString();
        //            //    pc.Sexo = dr["sexo"].ToString();
        //            //    pc.TipoSanguineo = int.Parse(dr["tiposanguineo"].ToString());
        //            //    pc.Sangue.Tipo = dr["stipo"].ToString();
        //            //    pc.Cpf = dr["cpf"].ToString();
        //            //    lista.Add(pc);
        //            //}
        //            Con.Pacientes.Where()
        //            return lista;
        //        }
        //    }
        //    catch (Exception e) {
        //        throw new Exception(e.ToString());
        //    }
        //}

    }
}
