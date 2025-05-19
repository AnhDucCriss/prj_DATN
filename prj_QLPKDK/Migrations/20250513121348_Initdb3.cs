using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prj_QLPKDK.Migrations
{
    /// <inheritdoc />
    public partial class Initdb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dosage",
                table: "PrescriptionDetails",
                newName: "Unit");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Medicines");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "PrescriptionDetails",
                newName: "Dosage");
        }
    }
}
