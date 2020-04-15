using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Generic {
    public class GenericDal<T, k> where T : class {

        public static void Insertt(T Entidade) {
            try {
                using (Conexao Con = new Conexao()) {

                    Con.Entry(Entidade).State = EntityState.Added;
                    Con.SaveChanges();
                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }

        public static T FindByKey(k Key) {
            try {
                using (Conexao Con = new Conexao()) {

                    return Con.Set<T>().Find(Key);

                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }

        public static List<T> FindAll() {
            try {
                using (Conexao Con = new Conexao()) {

                    return Con.Set<T>().ToList();

                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }


        //public static void Update(T entidade) {
        //    try {
        //        using (Conexao Con = new Conexao()) {
        //            Con.Entry(entidade).State = EntityState.Modified;
        //        }
        //    }
        //    catch (Exception e) {
        //        throw new Exception(e.ToString());
        //    }
        //}

        public static void Deletar(T entidade) {
            try {
                using (Conexao Con = new Conexao()) {
                    Con.Entry(entidade).State = EntityState.Deleted;
                    Con.SaveChanges();
                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }

        public static void Atualizar(T entidade) {
            try {
                using (Conexao Con = new Conexao()) {
                    Con.Entry(entidade).State = EntityState.Modified;
                    Con.SaveChanges();
                }
            }
            catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }
    }
}
