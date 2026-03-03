using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MosqueImam",
                table: "Mosques");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateFrom",
                table: "RentMasters",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateTo",
                table: "RentMasters",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "OfficeStaffs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "RentMasters");

            migrationBuilder.DropColumn(
                name: "DateTo",
                table: "RentMasters");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "OfficeStaffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MosqueImam",
                table: "Mosques",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
