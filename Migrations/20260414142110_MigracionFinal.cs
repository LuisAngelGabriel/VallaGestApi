using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VallaGestApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracionFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_UsuarioId",
                table: "Ordenes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_Usuarios_UsuarioId",
                table: "Ordenes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_Usuarios_UsuarioId",
                table: "Ordenes");

            migrationBuilder.DropIndex(
                name: "IX_Ordenes_UsuarioId",
                table: "Ordenes");
        }
    }
}
