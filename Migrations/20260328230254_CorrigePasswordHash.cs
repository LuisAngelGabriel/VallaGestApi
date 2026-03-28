using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VallaGestApi.Migrations
{
    /// <inheritdoc />
    public partial class CorrigePasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$qR77EshGZsh6IAnI7YpGTe6H.SPhI6IAnI7YpGTe6H.SPhI6IAnI");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$8sNlXay9S.GzBOPmS1O7uOn8G6Hk6iH/Y7X8XpYv.G8G8G8G8G8G8");
        }
    }
}
