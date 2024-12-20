﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEP7.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HQ_Usages",
                columns: table => new
                {
                    Year = table.Column<string>(type: "TEXT", nullable: false),
                    EnergyConsumption = table.Column<int>(type: "INTEGER", nullable: false),
                    Electricity = table.Column<int>(type: "INTEGER", nullable: false),
                    Solarpanels = table.Column<int>(type: "INTEGER", nullable: false),
                    DistrictHeating = table.Column<int>(type: "INTEGER", nullable: false),
                    NaturalGas = table.Column<int>(type: "INTEGER", nullable: false),
                    Propan = table.Column<int>(type: "INTEGER", nullable: false),
                    Hydrogen = table.Column<int>(type: "INTEGER", nullable: false),
                    Oil = table.Column<int>(type: "INTEGER", nullable: false),
                    Petrol = table.Column<int>(type: "INTEGER", nullable: false),
                    Diesel = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HQ_Usages", x => x.Year);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    role = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialsTotals",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaterialName = table.Column<string>(type: "TEXT", nullable: false),
                    ADP_Fossil_MJ = table.Column<double>(type: "REAL", nullable: false),
                    ADP_Minerals_Metals = table.Column<double>(type: "REAL", nullable: false),
                    AP_Mol_H_Eq = table.Column<double>(type: "REAL", nullable: false),
                    E_Fi_CTUe = table.Column<double>(type: "REAL", nullable: false),
                    E_Fm_CTUe = table.Column<double>(type: "REAL", nullable: false),
                    E_Fo_CTUe = table.Column<double>(type: "REAL", nullable: false),
                    EP_Terrestrial_Mol_N_Eq = table.Column<double>(type: "REAL", nullable: false),
                    EP_Freshwater_kg_P = table.Column<double>(type: "REAL", nullable: false),
                    EP_Marine_kg_N = table.Column<double>(type: "REAL", nullable: false),
                    ETP_FW_CTUe = table.Column<double>(type: "REAL", nullable: false),
                    GWP_Biogenic_kg_CO2 = table.Column<double>(type: "REAL", nullable: false),
                    GWP_Fossil_kg_CO2 = table.Column<double>(type: "REAL", nullable: false),
                    GWP_LULUC_kg_CO2 = table.Column<double>(type: "REAL", nullable: false),
                    GWP_Total_kg_CO2 = table.Column<double>(type: "REAL", nullable: false),
                    HT_CI_CTUh = table.Column<double>(type: "REAL", nullable: false),
                    HT_CM_CTUh = table.Column<double>(type: "REAL", nullable: false),
                    HT_CO_CTUh = table.Column<double>(type: "REAL", nullable: false),
                    HT_NCI_CTUh = table.Column<double>(type: "REAL", nullable: false),
                    HT_NCM_CTUh = table.Column<double>(type: "REAL", nullable: false),
                    HT_NCO_CTUh = table.Column<double>(type: "REAL", nullable: false),
                    HTTP_C_CTUh = table.Column<double>(type: "REAL", nullable: false),
                    HTTP_NC_CTUh = table.Column<double>(type: "REAL", nullable: false),
                    IRP_kBq_U235 = table.Column<double>(type: "REAL", nullable: false),
                    LU_Pt = table.Column<double>(type: "REAL", nullable: false),
                    ODP_kg_CFC11 = table.Column<double>(type: "REAL", nullable: false),
                    PM_Disease_Inc = table.Column<double>(type: "REAL", nullable: false),
                    POCP_kg_NMVOC = table.Column<double>(type: "REAL", nullable: false),
                    WDP_m3_Depriv = table.Column<double>(type: "REAL", nullable: false),
                    ProductID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialsTotals", x => x.MaterialId);
                    table.ForeignKey(
                        name: "FK_MaterialsTotals_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "MatData",
                columns: table => new
                {
                    MaterialDataId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductID = table.Column<string>(type: "TEXT", nullable: true),
                    MaterialId = table.Column<int>(type: "INTEGER", nullable: false),
                    MaterialType = table.Column<string>(type: "TEXT", nullable: true),
                    TotalWeight = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatData", x => x.MaterialDataId);
                    table.ForeignKey(
                        name: "FK_MatData_MaterialsTotals_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "MaterialsTotals",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatData_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatData_MaterialId",
                table: "MatData",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MatData_ProductID",
                table: "MatData",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsTotals_ProductID",
                table: "MaterialsTotals",
                column: "ProductID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HQ_Usages");

            migrationBuilder.DropTable(
                name: "MatData");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MaterialsTotals");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
