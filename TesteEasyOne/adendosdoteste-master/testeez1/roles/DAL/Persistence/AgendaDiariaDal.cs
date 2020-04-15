using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence {
    public class AgendaDiariaDal {

        public List<AgendaDiaria> getAll() {
            List<AgendaDiaria> lista = new List<AgendaDiaria>();
            try {
                using (Conexao Con = new Conexao()) {
                    //String sql = "select * from agendaMedica";
                    //conn.Open();
                    //SqlCommand cmd = new SqlCommand(sql, conn);
                    //SqlDataReader dr = cmd.ExecuteReader();
                    //while (dr.Read()) {
                    //    AgendaDiaria ag = new AgendaDiaria();
                    //    ag.NomeMedico = dr["nomeMedico"].ToString();
                    //    ag.NomePaciente = dr["nomePaciente"].ToString();
                    //    ag.SanguePaciente = dr["sanguePaciente"].ToString();
                    //    ag.SexoPaciente = dr["sexoPaciente"].ToString();
                    //    ag.EspecialidadeConsulta = dr["especialidade"].ToString();
                    //    ag.DescricaoConsulta = dr["consultaDescricao"].ToString();
                    //    ag.DataConsulta = Convert.ToDateTime(dr["dataConsulta"]);
                    //    lista.Add(ag);
                    //}
                    //return lista;
                    lista = Con.AgendasDiarias.ToList();
                }
                return lista;
            }
            catch (Exception e) {

                throw new Exception(e.ToString());
            }
        }

    }
}
