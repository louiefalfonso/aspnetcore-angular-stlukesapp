using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StLukesMedicalApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPrescriptionModeltoPatients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Prescriptions_PrescriptionId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PrescriptionId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "Patients");

            migrationBuilder.CreateTable(
                name: "PatientPrescription",
                columns: table => new
                {
                    PatientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrescriptionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPrescription", x => new { x.PatientsId, x.PrescriptionsId });
                    table.ForeignKey(
                        name: "FK_PatientPrescription_Patients_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientPrescription_Prescriptions_PrescriptionsId",
                        column: x => x.PrescriptionsId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientPrescription_PrescriptionsId",
                table: "PatientPrescription",
                column: "PrescriptionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientPrescription");

            migrationBuilder.AddColumn<Guid>(
                name: "PrescriptionId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PrescriptionId",
                table: "Patients",
                column: "PrescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Prescriptions_PrescriptionId",
                table: "Patients",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id");
        }
    }
}
