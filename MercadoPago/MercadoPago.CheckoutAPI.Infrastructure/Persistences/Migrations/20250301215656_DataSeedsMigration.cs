using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MercadoPago.CheckoutAPI.Infrastructure.Persistences.Migrations
{
    /// <inheritdoc />
    public partial class DataSeedsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "IdRol", "Descripcion", "FechaActualizacion", "FechaCreacion" },
                values: new object[,]
                {
                    { 1, "administrator", null, new DateTime(2025, 3, 1, 15, 30, 12, 0, DateTimeKind.Unspecified) },
                    { 2, "supervisor", null, new DateTime(2025, 3, 1, 16, 40, 24, 0, DateTimeKind.Unspecified) },
                    { 3, "customer", null, new DateTime(2025, 3, 1, 17, 50, 36, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "IdUsuario", "Email", "FechaActualizacion", "FechaCreacion", "IdRol", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "test@admin.com.ar", null, new DateTime(2025, 3, 1, 18, 35, 22, 0, DateTimeKind.Unspecified), 1, "$argon2id$v=19$m=65536,t=3,p=1$wJzWQr1EhYRpANrf4CN46g$r769BfweDJBt6GW/l2/9+Tf+glooEXl4WWAf18u+1C8", "Admin" },
                    { 2, "test@customer.com.ar", null, new DateTime(2025, 3, 1, 19, 49, 44, 0, DateTimeKind.Unspecified), 3, "$argon2id$v=19$m=65536,t=3,p=1$dx/JCx8reoHCnDyv7N30dg$qK//vGto1eBnUUmjwoVOKmVNxNO2rHwIRvIun4eWAxc", "Customer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 3);
        }
    }
}
