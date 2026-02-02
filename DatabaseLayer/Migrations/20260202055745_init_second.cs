using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class init_second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfficeStaffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeStaffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficeStaffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseCategories_OfficeStaffs_OfficeStaffId",
                        column: x => x.OfficeStaffId,
                        principalTable: "OfficeStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancialYears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    DateTo = table.Column<DateOnly>(type: "date", nullable: false),
                    YearName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeStaffId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialYears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialYears_OfficeStaffs_OfficeStaffId",
                        column: x => x.OfficeStaffId,
                        principalTable: "OfficeStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PropertyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Measurement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnershipDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DocType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeStaffId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_OfficeStaffs_OfficeStaffId",
                        column: x => x.OfficeStaffId,
                        principalTable: "OfficeStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Properties_OrganizationMaster_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MosqueId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfJoining = table.Column<DateOnly>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdharNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternateNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VillageTown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StaffType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficeStaffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staffs_Mosques_MosqueId",
                        column: x => x.MosqueId,
                        principalTable: "Mosques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staffs_OfficeStaffs_OfficeStaffId",
                        column: x => x.OfficeStaffId,
                        principalTable: "OfficeStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staffs_OrganizationMaster_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpensesMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseCategoryId = table.Column<int>(type: "int", nullable: false),
                    PaidTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpenseAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeStaffId = table.Column<int>(type: "int", nullable: false),
                    FinancialYearId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpensesMaster_ExpenseCategories_ExpenseCategoryId",
                        column: x => x.ExpenseCategoryId,
                        principalTable: "ExpenseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpensesMaster_FinancialYears_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpensesMaster_OfficeStaffs_OfficeStaffId",
                        column: x => x.OfficeStaffId,
                        principalTable: "OfficeStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillNo = table.Column<int>(type: "int", nullable: true),
                    ChallanNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GSTIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GSTType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GSTPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GSTAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BillDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DocType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillUpload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    OfficeStaffId = table.Column<int>(type: "int", nullable: false),
                    FinancialYearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseMasters_FinancialYears_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseMasters_OfficeStaffs_OfficeStaffId",
                        column: x => x.OfficeStaffId,
                        principalTable: "OfficeStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseMasters_OrganizationMaster_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyRentAgreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertiesId = table.Column<int>(type: "int", nullable: true),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    DateTo = table.Column<DateOnly>(type: "date", nullable: false),
                    RentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternateNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deposite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AadharNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeStaffId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyRentAgreements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyRentAgreements_OfficeStaffs_OfficeStaffId",
                        column: x => x.OfficeStaffId,
                        principalTable: "OfficeStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyRentAgreements_Properties_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "Properties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    DonorThrough = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinancialYearId = table.Column<int>(type: "int", nullable: false),
                    OfficeStaffId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_FinancialYears_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donations_OfficeStaffs_OfficeStaffId",
                        column: x => x.OfficeStaffId,
                        principalTable: "OfficeStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donations_OrganizationMaster_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donations_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalaryMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    SalaryAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneratedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    OfficeStaffId = table.Column<int>(type: "int", nullable: false),
                    FinancialYearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryMaster_FinancialYears_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalaryMaster_OfficeStaffs_OfficeStaffId",
                        column: x => x.OfficeStaffId,
                        principalTable: "OfficeStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalaryMaster_OrganizationMaster_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalaryMaster_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawalSalaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    WithdrawalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficeStaffId = table.Column<int>(type: "int", nullable: false),
                    FinancialYearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawalSalaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WithdrawalSalaries_FinancialYears_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WithdrawalSalaries_OfficeStaffs_OfficeStaffId",
                        column: x => x.OfficeStaffId,
                        principalTable: "OfficeStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WithdrawalSalaries_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchasePayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseMasterId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    OfficeStaffId = table.Column<int>(type: "int", nullable: false),
                    FinancialYearId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasePayments_FinancialYears_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchasePayments_OfficeStaffs_OfficeStaffId",
                        column: x => x.OfficeStaffId,
                        principalTable: "OfficeStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchasePayments_OrganizationMaster_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "OrganizationMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchasePayments_PurchaseMasters_PurchaseMasterId",
                        column: x => x.PurchaseMasterId,
                        principalTable: "PurchaseMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyRentAgreementId = table.Column<int>(type: "int", nullable: false),
                    RentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeStaffId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentMasters_OfficeStaffs_OfficeStaffId",
                        column: x => x.OfficeStaffId,
                        principalTable: "OfficeStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentMasters_PropertyRentAgreements_PropertyRentAgreementId",
                        column: x => x.PropertyRentAgreementId,
                        principalTable: "PropertyRentAgreements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_FinancialYearId",
                table: "Donations",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_OfficeStaffId",
                table: "Donations",
                column: "OfficeStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_OrganizationId",
                table: "Donations",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_StaffId",
                table: "Donations",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_OfficeStaffId",
                table: "ExpenseCategories",
                column: "OfficeStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesMaster_ExpenseCategoryId",
                table: "ExpensesMaster",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesMaster_FinancialYearId",
                table: "ExpensesMaster",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesMaster_OfficeStaffId",
                table: "ExpensesMaster",
                column: "OfficeStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYears_OfficeStaffId",
                table: "FinancialYears",
                column: "OfficeStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OfficeStaffId",
                table: "Properties",
                column: "OfficeStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OrganizationId",
                table: "Properties",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyRentAgreements_OfficeStaffId",
                table: "PropertyRentAgreements",
                column: "OfficeStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyRentAgreements_PropertiesId",
                table: "PropertyRentAgreements",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseMasters_FinancialYearId",
                table: "PurchaseMasters",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseMasters_OfficeStaffId",
                table: "PurchaseMasters",
                column: "OfficeStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseMasters_OrganizationId",
                table: "PurchaseMasters",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePayments_FinancialYearId",
                table: "PurchasePayments",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePayments_OfficeStaffId",
                table: "PurchasePayments",
                column: "OfficeStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePayments_OrganizationId",
                table: "PurchasePayments",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePayments_PurchaseMasterId",
                table: "PurchasePayments",
                column: "PurchaseMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_RentMasters_OfficeStaffId",
                table: "RentMasters",
                column: "OfficeStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_RentMasters_PropertyRentAgreementId",
                table: "RentMasters",
                column: "PropertyRentAgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryMaster_FinancialYearId",
                table: "SalaryMaster",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryMaster_OfficeStaffId",
                table: "SalaryMaster",
                column: "OfficeStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryMaster_OrganizationId",
                table: "SalaryMaster",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryMaster_StaffId",
                table: "SalaryMaster",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_MosqueId",
                table: "Staffs",
                column: "MosqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_OfficeStaffId",
                table: "Staffs",
                column: "OfficeStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_OrganizationId",
                table: "Staffs",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalSalaries_FinancialYearId",
                table: "WithdrawalSalaries",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalSalaries_OfficeStaffId",
                table: "WithdrawalSalaries",
                column: "OfficeStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalSalaries_StaffId",
                table: "WithdrawalSalaries",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "ExpensesMaster");

            migrationBuilder.DropTable(
                name: "PurchasePayments");

            migrationBuilder.DropTable(
                name: "RentMasters");

            migrationBuilder.DropTable(
                name: "SalaryMaster");

            migrationBuilder.DropTable(
                name: "WithdrawalSalaries");

            migrationBuilder.DropTable(
                name: "ExpenseCategories");

            migrationBuilder.DropTable(
                name: "PurchaseMasters");

            migrationBuilder.DropTable(
                name: "PropertyRentAgreements");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "FinancialYears");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "OfficeStaffs");
        }
    }
}
