using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TesteITAU.Models;

namespace TesteITAU
{
    public class DbContexto : DbContext
    {
        public DbContexto() : base("TesteItauDB") { }


        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Conta> Contas { get; set; }
        public virtual DbSet<Lancamento> Lancamento { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Usuario>()
            //   //.HasMany(e => e.Enderecos)
            //   .WithRequired(e => e.Usuario)
            //   .HasForeignKey(e => e.Usuario_ID)
            //   .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Usuario>()
            //.HasRequired(u => u.Endereco)
            //.WithRequiredPrincipal(e => e.Usuarios);
        }
    }
}