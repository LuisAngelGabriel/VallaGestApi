using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VallaGestApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregaCampoMesesAOrdeDetalle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Email", "Nombre", "PasswordHash", "Rol" },
                values: new object[] { 1, "admin@vallagest.com", "Administrador", "$2a$11$qR77EshGZsh6IAnI7YpGTe6H.SPhI6IAnI7YpGTe6H.SPhI6IAnI", "Admin" });
        }
    }
}
