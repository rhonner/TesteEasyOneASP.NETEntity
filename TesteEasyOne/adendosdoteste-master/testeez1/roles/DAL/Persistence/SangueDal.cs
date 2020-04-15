using DAL.Entity;
using DAL.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence {
    public class SangueDal : GenericDal <Sangue,int> {

        public List<Sangue> GetAll() {
            try {
                using (Conexao Con = new Conexao()) {
                    return Con.Sangues.ToList();
                }
            }
            catch (Exception e) {
                throw new Exception (e.ToString());
            }
        }

        public Sangue GetOne(int p) {
            try {
                using (Conexao Con = new Conexao()) {
                    var sn = Con.Sangues.SingleOrDefault(id => id.Id == p);
                    return sn;
                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }

        }

    }
}
