using Microsoft.EntityFrameworkCore;

namespace GerenciadorEscolar.Entity
{
    public class GerenciadorEscolarDbContext : DbContext
    {
        public DbSet<Escola> Escola { get; set; }
        public DbSet<Turma> Turma { get; set; }

        public GerenciadorEscolarDbContext()
        { }

        public GerenciadorEscolarDbContext(DbContextOptions<GerenciadorEscolarDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Escola>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(500);
                entity.Property(e => e.NumeroInep);
            });

            modelBuilder.Entity<Turma>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Ano).IsRequired();
                entity.Property(e => e.Curso).HasMaxLength(255);
                entity.Property(e => e.Serie).HasMaxLength(255);
                entity.Property(e => e.Nome).HasMaxLength(255);
                entity.HasOne(d => d.Escola)
                  .WithMany(p => p.Turmas)
                  .HasForeignKey(d => d.Id);
            });
        }
    }
}
