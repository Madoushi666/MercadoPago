using MercadoPago.CheckoutAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MercadoPago.CheckoutAPI.Infrastructure.Persistences.Contexts.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK_Users");

            builder.Property(e => e.Id).HasColumnName("IdUsuario");

            builder.HasIndex(e => e.Email, "UK_Usuarios_Email").IsUnique();

            builder.HasIndex(e => e.Username, "UK_Usuarios_Username").IsUnique();

            builder.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            builder.Property(e => e.Username)
                .HasMaxLength(16)
                .IsUnicode(false);

            builder.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Roles");
        }
    }
}
