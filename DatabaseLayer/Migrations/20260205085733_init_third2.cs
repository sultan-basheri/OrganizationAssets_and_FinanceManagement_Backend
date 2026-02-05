using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class init_third2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyRentAgreements_Properties_PropertiesId",
                table: "PropertyRentAgreements");

            migrationBuilder.DropIndex(
                name: "IX_PropertyRentAgreements_PropertiesId",
                table: "PropertyRentAgreements");

            migrationBuilder.DropColumn(
                name: "PropertiesId",
                table: "PropertyRentAgreements");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyRentAgreements_PropertyId",
                table: "PropertyRentAgreements",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyRentAgreements_Properties_PropertyId",
                table: "PropertyRentAgreements",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyRentAgreements_Properties_PropertyId",
                table: "PropertyRentAgreements");

            migrationBuilder.DropIndex(
                name: "IX_PropertyRentAgreements_PropertyId",
                table: "PropertyRentAgreements");

            migrationBuilder.AddColumn<int>(
                name: "PropertiesId",
                table: "PropertyRentAgreements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyRentAgreements_PropertiesId",
                table: "PropertyRentAgreements",
                column: "PropertiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyRentAgreements_Properties_PropertiesId",
                table: "PropertyRentAgreements",
                column: "PropertiesId",
                principalTable: "Properties",
                principalColumn: "Id");
        }
    }
}
