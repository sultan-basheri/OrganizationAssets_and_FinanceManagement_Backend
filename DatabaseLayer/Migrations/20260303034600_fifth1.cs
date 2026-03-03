using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class fifth1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "FinancialYears",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "FinancialYears");
        }
    }
}
