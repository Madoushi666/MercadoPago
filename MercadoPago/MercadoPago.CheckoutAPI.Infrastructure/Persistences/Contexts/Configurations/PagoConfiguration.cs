using MercadoPago.CheckoutAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MercadoPago.CheckoutAPI.Infrastructure.Persistences.Contexts.Configurations
{
    public class PagoConfiguration : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("IdPago");

            builder.Property(e => e.Estado)
                .HasMaxLength(30)
                .IsUnicode(false);
            builder.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            builder.Property(e => e.FechaAprobacion).HasColumnType("datetime");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.IdMetodoPago)
                .HasMaxLength(20)
                .IsUnicode(false);
            builder.Property(e => e.IdPagoMp).HasColumnName("IdPagoMP");
            builder.Property(e => e.ImporteTotal).HasColumnType("decimal(18, 2)");
        }
    }
}
