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

            builder.HasData(
                new Usuario() { Id = 1, Email = "test@admin.com.ar", Username = "Admin", Password = "$argon2id$v=19$m=65536,t=3,p=1$wJzWQr1EhYRpANrf4CN46g$r769BfweDJBt6GW/l2/9+Tf+glooEXl4WWAf18u+1C8", IdRol = 1, FechaCreacion = new DateTime(2025, 03, 01, 18, 35, 22) }, // Password: 1234
                new Usuario() { Id = 2, Email = "test@customer.com.ar", Username = "Customer", Password = "$argon2id$v=19$m=65536,t=3,p=1$dx/JCx8reoHCnDyv7N30dg$qK//vGto1eBnUUmjwoVOKmVNxNO2rHwIRvIun4eWAxc", IdRol = 3, FechaCreacion = new DateTime(2025, 03, 01, 19, 49, 44) } // Password: 4321
            );
        }
    }
}
