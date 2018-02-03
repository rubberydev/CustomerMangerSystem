using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Ingeneo.AppContratos.Models
{
    public class GestorContratosDbContext : DbContext
    {
        public GestorContratosDbContext()
            :base("GestorContratos")
        {
        }

        public DbSet<Cliente> Cliente { set; get; }
        public DbSet<Contrato> Contrato { set; get; }
        public DbSet<Prorroga> Prorroga { set; get; }        
        public DbSet<Poliza> Poliza { set; get; }
        public DbSet<DetallePoliza> DetallePoliza { set; get; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            modelBuilder.Entity<Cliente>()
                        .Property(e => e.NombreCliente)
                        //.HasMaxLength(30)
                        .IsRequired();

            modelBuilder.Entity<Cliente>()
                       .Property(e => e.Direccion);
                       //.HasMaxLength(30);

            modelBuilder.Entity<Cliente>().HasMany(e => e.Contrato)
                        .WithOptional(e => e.Cliente)
                        .HasForeignKey(e => e.Clienteid);

            modelBuilder.Entity<Contrato>()
                        .Property(e => e.FechaInicioContrato)
                        .IsRequired();

            modelBuilder.Entity<Contrato>().HasMany(e => e.Prorroga)
                        .WithOptional(e => e.Contrato)
                        .HasForeignKey(e => e.Contratoid);

            //Crear una clave compuesta a partir de dos claves
            //modelBuilder.Entity<ContratoPoliza>()
            //            .HasKey(e => new { e.idContrato, e.idPoliza });

            modelBuilder.Entity<Contrato>().HasMany(e => e.Poliza)
                        .WithOptional(e => e.Contrato)
                        .HasForeignKey(e => e.idContrato);

            modelBuilder.Entity<Prorroga>().HasMany(e => e.PolizaPro)
                        .WithOptional(e => e.Prorroga)
                        .HasForeignKey(e => e.idProrroga);

            //modelBuilder.Entity<Contrato>().HasRequired(c => c.Poliza)
            //            .WithRequiredPrincipal(e => e.Contrato);

            //modelBuilder.Entity<Prorroga>().HasRequired(c => c.PolizaPro)
            //           .WithRequiredPrincipal(e => e.Prorroga);

            //otra manera de crear relationships de one-to-many
            modelBuilder.Entity<DetallePoliza>()
                        .HasRequired<Poliza>(e => e.Poliza)
                        .WithMany(e => e.DetallePoliza)
                        .HasForeignKey(e => e.PolizaId);

            modelBuilder.Entity<Poliza>()
                         .HasMany(p => p.DetallePoliza)
                         .WithRequired(p => p.Poliza)
                         .WillCascadeOnDelete(false);

   
        }
    }    
}