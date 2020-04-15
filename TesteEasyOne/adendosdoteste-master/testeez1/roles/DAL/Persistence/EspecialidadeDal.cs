using DAL.Entity;
using DAL.Generic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence {
    public class EspecialidadeDal : GenericDal<Especialidade, int> {

        public List<Especialidade> GetAll() {
            try {
                using (Conexao Con = new Conexao()) {
                    return Con.Especialidades.ToList();
                }
            }
            catch (Exception e) {

                throw new Exception(e.ToString());
            }
        }

        public Especialidade GetOne(int p) {
            try {
                using (Conexao Con = new Conexao()) {
                    var sn = Con.Especialidades.SingleOrDefault(id => id.Id == p);
                    return sn;
                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }



        public void Alterar(Especialidade es) {
            try {
                using(Conexao Con = new Conexao()) {
                    Con.Entry(es).State = EntityState.Modified;
                    Con.SaveChanges();
                }
            }catch(Exception e) {
                throw new Exception(e.ToString());
            }
        }

    }
}
