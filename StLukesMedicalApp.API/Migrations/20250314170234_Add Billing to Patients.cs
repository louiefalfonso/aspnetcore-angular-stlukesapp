using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StLukesMedicalApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBillingtoPatients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Billings_BillingId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_BillingId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "BillingId",
                table: "Patients");

            migrationBuilder.CreateTable(
                name: "BillingPatient",
                columns: table => new
                {
                    BillingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingPatient", x => new { x.BillingsId, x.PatientsId });
                    table.ForeignKey(
                        name: "FK_BillingPatient_Billings_BillingsId",
                        column: x => x.BillingsId,
                        principalTable: "Billings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillingPatient_Patients_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingPatient_PatientsId",
                table: "BillingPatient",
                column: "PatientsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingPatient");

            migrationBuilder.AddColumn<Guid>(
                name: "BillingId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_BillingId",
                table: "Patients",
                column: "BillingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Billings_BillingId",
                table: "Patients",
                column: "BillingId",
                principalTable: "Billings",
                principalColumn: "Id");
        }
    }
}
