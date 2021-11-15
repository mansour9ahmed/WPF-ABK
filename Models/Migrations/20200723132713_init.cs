using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    BeneficiaryName = table.Column<string>(nullable: true),
                    UsdAccountNo = table.Column<string>(nullable: true),
                    UsdIban = table.Column<string>(nullable: true),
                    EuroAccountNo = table.Column<string>(nullable: true),
                    EuroIban = table.Column<string>(nullable: true),
                    Swift = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    BankId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_BankAccounts_BankId",
                        column: x => x.BankId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    IsInput = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>(nullable: false),
                    BankId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "TransactionAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_BankAccounts_BankId",
                        column: x => x.BankId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyEmails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyEmails_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Soas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoaNo = table.Column<string>(nullable: false),
                    SoaDate = table.Column<DateTime>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    BankId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Soas_BankAccounts_BankId",
                        column: x => x.BankId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Soas_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vessels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    SimNo = table.Column<string>(nullable: true),
                    SimType = table.Column<string>(nullable: true),
                    ActivationDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    BundleName = table.Column<string>(nullable: true),
                    BundlePrice = table.Column<double>(nullable: false),
                    OverMbPrice = table.Column<double>(nullable: false),
                    OverMinPrice = table.Column<double>(nullable: false),
                    IntegraPrice = table.Column<double>(nullable: false),
                    AccountPassword = table.Column<string>(nullable: true),
                    MsgSize = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vessels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vessels_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoaElements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    SoaId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoaElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoaElements_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoaElements_Soas_SoaId",
                        column: x => x.SoaId,
                        principalTable: "Soas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNo = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    BundleName = table.Column<string>(nullable: true),
                    BundlePrice = table.Column<double>(nullable: false),
                    OverMb = table.Column<double>(nullable: false),
                    OverMbPrice = table.Column<double>(nullable: false),
                    OverMin = table.Column<double>(nullable: false),
                    OverMinPrice = table.Column<double>(nullable: false),
                    IntegraPrice = table.Column<double>(nullable: false, defaultValue: 0.0),
                    IsPaid = table.Column<bool>(nullable: false, defaultValue: false),
                    VesselId = table.Column<int>(nullable: true),
                    BankId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_BankAccounts_BankId",
                        column: x => x.BankId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Vessels_VesselId",
                        column: x => x.VesselId,
                        principalTable: "Vessels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceSoaElements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(nullable: false),
                    SoaElementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceSoaElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceSoaElements_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceSoaElements_SoaElements_SoaElementId",
                        column: x => x.SoaElementId,
                        principalTable: "SoaElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_BankId",
                table: "Companies",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmails_CompanyId",
                table: "CompanyEmails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BankId",
                table: "Invoices",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_VesselId",
                table: "Invoices",
                column: "VesselId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceSoaElements_InvoiceId",
                table: "InvoiceSoaElements",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceSoaElements_SoaElementId",
                table: "InvoiceSoaElements",
                column: "SoaElementId");

            migrationBuilder.CreateIndex(
                name: "IX_SoaElements_CompanyId",
                table: "SoaElements",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SoaElements_SoaId",
                table: "SoaElements",
                column: "SoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Soas_BankId",
                table: "Soas",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Soas_CompanyId",
                table: "Soas",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankId",
                table: "Transactions",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Vessels_CompanyId",
                table: "Vessels",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyEmails");

            migrationBuilder.DropTable(
                name: "InvoiceSoaElements");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "SoaElements");

            migrationBuilder.DropTable(
                name: "TransactionAccounts");

            migrationBuilder.DropTable(
                name: "Vessels");

            migrationBuilder.DropTable(
                name: "Soas");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "BankAccounts");
        }
    }
}
