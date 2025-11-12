using Microsoft.EntityFrameworkCore;

namespace Repositorio.Models
{
    public class RepositorioContext : DbContext
    {
        public RepositorioContext(DbContextOptions<RepositorioContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Docente> Docente { get; set; }
        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<Gestor> Gestor{ get; set; }
        public DbSet<Coordinacion> Coordinacion { get; set; }
        public DbSet<TI> TI { get; set; }
        public DbSet<Recurso> Recurso { get; set; }
        public DbSet<Version> Version { get; set; }
        public DbSet<Metadato> Metadato { get; set; }
        public DbSet<Validacion> Validacione { get; set; }
        public DbSet<Notificacion> Notificacion { get; set; }
        public DbSet<Metrica> Metrica { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación 1:1 Usuario-Docente
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Docente)
                .WithOne(d => d.Usuario)
                .HasForeignKey<Docente>(d => d.Id_Usuario);

            // Relación 1:1 Usuario-Estudiante
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Estudiante)
                .WithOne(e => e.Usuario)
                .HasForeignKey<Estudiante>(e => e.Id_Usuario);

            // Relación 1:1 Usuario-Gestor
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Gestor)
                .WithOne(g => g.Usuario)
                .HasForeignKey<Gestor>(g => g.Id_Usuario);

            // Relación 1:1 Usuario-Coordinacion
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Coordinacion)
                .WithOne(c => c.Usuario)
                .HasForeignKey<Coordinacion>(c => c.Id_Usuario);

            // Relación 1:1 Usuario-TI
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.TI)
                .WithOne(t => t.Usuario)
                .HasForeignKey<TI>(t => t.Id_Usuario);
        }
    }
}
