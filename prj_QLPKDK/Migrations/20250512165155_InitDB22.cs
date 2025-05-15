using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prj_QLPKDK.Migrations
{
    /// <inheritdoc />
    public partial class InitDB22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Prescriptions");

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "Prescriptions");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Prescriptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
