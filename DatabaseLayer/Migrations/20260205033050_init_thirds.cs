using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class init_thirds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdharNo",
                table: "Staffs",
                newName: "AadharNo");

            migrationBuilder.AddColumn<string>(
                name: "ContactNo",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNo",
                table: "Staffs");

            migrationBuilder.RenameColumn(
                name: "AadharNo",
                table: "Staffs",
                newName: "AdharNo");
        }
    }
}
