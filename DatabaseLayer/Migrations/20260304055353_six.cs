using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class six : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Staffs_StaffId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Mosques_MosqueId",
                table: "Staffs");

            migrationBuilder.AlterColumn<int>(
                name: "MosqueId",
                table: "Staffs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StaffId",
                table: "Donations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MosqueId",
                table: "Donations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_MosqueId",
                table: "Donations",
                column: "MosqueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Mosques_MosqueId",
                table: "Donations",
                column: "MosqueId",
                principalTable: "Mosques",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Staffs_StaffId",
                table: "Donations",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Mosques_MosqueId",
                table: "Staffs",
                column: "MosqueId",
                principalTable: "Mosques",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Mosques_MosqueId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Staffs_StaffId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Mosques_MosqueId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Donations_MosqueId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "MosqueId",
                table: "Donations");

            migrationBuilder.AlterColumn<int>(
                name: "MosqueId",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StaffId",
                table: "Donations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Staffs_StaffId",
                table: "Donations",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Mosques_MosqueId",
                table: "Staffs",
                column: "MosqueId",
                principalTable: "Mosques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
