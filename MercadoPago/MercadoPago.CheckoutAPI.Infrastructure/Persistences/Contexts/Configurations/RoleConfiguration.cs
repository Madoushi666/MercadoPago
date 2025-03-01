using MercadoPago.CheckoutAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MercadoPago.CheckoutAPI.Infrastructure.Persistences.Contexts.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("IdRol");

            builder.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
            builder.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");

            builder.HasData(
                new Role() { Id = 1, Descripcion = "administrator", FechaCreacion = new DateTime(2025, 03, 01, 15, 30, 12) },
                new Role() { Id = 2, Descripcion = "supervisor", FechaCreacion = new DateTime(2025, 03, 01, 16, 40, 24) },
                new Role() { Id = 3, Descripcion = "customer", FechaCreacion = new DateTime(2025, 03, 01, 17, 50, 36) }
            );
        }
    }
}
