using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SenaiCrud.Models.Tables;


namespace SenaiCrud.Data
{
    public class SenaiCrudDbContext : DbContext
    {
        public SenaiCrudDbContext(DbContextOptions<SenaiCrudDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tarefas> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefas>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Descricao); // Removido HasColumnType
                entity.Property(e => e.DataCriacao).IsRequired();
                entity.Property(e => e.DataConclusao);
                entity.Property(e => e.Concluida).IsRequired();
            });
        }
    }
}
