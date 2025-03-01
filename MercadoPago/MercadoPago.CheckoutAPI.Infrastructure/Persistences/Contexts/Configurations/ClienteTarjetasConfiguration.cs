using MercadoPago.CheckoutAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MercadoPago.CheckoutAPI.Infrastructure.Persistences.Contexts.Configurations
{
    public class ClienteTarjetasConfiguration : IEntityTypeConfiguration<ClienteTarjeta>
    {
        public void Configure(EntityTypeBuilder<ClienteTarjeta> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("IdClienteTarjeta");

            builder.HasIndex(e => new { e.IdClienteMp, e.IdTarjetaMp }, "UK_ClienteTarjetas_IdClienteMP_IdTarjetaMP").IsUnique();

            builder.Property(e => e.AnioExpiracion)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            builder.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.IdClienteMp)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("IdClienteMP");
            builder.Property(e => e.IdTarjetaMp).HasColumnName("IdTarjetaMP");
            builder.Property(e => e.MesExpiracion)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            builder.Property(e => e.PrimerosSeisDigitos)
                .HasMaxLength(6)
                .IsUnicode(false);
            builder.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false);
            builder.Property(e => e.Titular)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.UltimosCuatroDigitos)
                .HasMaxLength(4)
                .IsUnicode(false);
        }
    }
}
