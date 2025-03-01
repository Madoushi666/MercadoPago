using MercadoPago.CheckoutAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MercadoPago.CheckoutAPI.Infrastructure.Persistences.Contexts.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("IdCliente");

            builder.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.IdClienteMP)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("IdClienteMP");
            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        }
    }
}
