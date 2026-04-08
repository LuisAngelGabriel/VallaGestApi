using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VallaGestApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoCampoMesesOrdenDetalle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Meses",
                table: "OrdenDetalles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Meses",
                table: "OrdenDetalles");
        }
    }
}
