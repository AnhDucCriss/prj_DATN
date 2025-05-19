using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prj_QLPKDK.Migrations
{
    /// <inheritdoc />
    public partial class Initdb4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsageInstructions",
                table: "PrescriptionDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsageInstructions",
                table: "PrescriptionDetails",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
