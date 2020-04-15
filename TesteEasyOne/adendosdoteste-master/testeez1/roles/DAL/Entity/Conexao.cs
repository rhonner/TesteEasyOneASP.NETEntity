using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity {
    public class Conexao : DbContext {
        public Conexao() : base(GetString()) {
            //Enviar a connectionstring para a classe DbContext (superclasse)
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static string GetString() {
            try {
                return ConfigurationManager.ConnectionStrings["aula"].ConnectionString;

            }
            catch (Exception e) {

                throw;
            }
        }

        public override int SaveChanges() {
            try {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e) {
                foreach (var eve in e.EntityValidationErrors) {
                    Console.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" tem os seguintes erros de validação:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors) {
                        Console.WriteLine("- Property: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Administrativo> Administrativos { get; set; }
        public DbSet<Sangue> Sangues { get; set; }
        public DbSet<AgendaDiaria> AgendasDiarias{ get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            //modelBuilder.Entity<Consulta>()
            //    .HasOptional(x => x.Paciente)
            //    .WithMany(x => x.Consultas)
            //    .HasForeignKey(x => x.IdPaciente);

            //HasRequired(x => x.Animals)
            //.WithMany(x => x.Owners)  // Or, just .WithMany()
            //.HasForeignKey(x => x.Pet1ID);

            //modelBuilder.Entity<Paciente>()
            //    .HasOptional<Consulta>(s => s.Consultas)
            //   .HasMany<Consulta>()
            //   .WillCascadeOnDelete(false);
        }
        }

    }
