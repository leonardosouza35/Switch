using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Switch.Domain.Entities;

namespace Switch.Infra.Data.Config
{
    public class StatusRelacionamentoConfiguration : IEntityTypeConfiguration<StatusRelacionamento>
    {
        
        public void Configure(EntityTypeBuilder<StatusRelacionamento> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Descricao);

            builder.HasData(
                new StatusRelacionamento() { Id = 1, Descricao = "NaoEspecificado"},
                new StatusRelacionamento() { Id = 2, Descricao = "Solteiro" },
                new StatusRelacionamento() { Id = 3, Descricao = "Casado" },
                new StatusRelacionamento() { Id = 4, Descricao = "EmRelacionamentoSerio" }                
                );
        }
    }
}
