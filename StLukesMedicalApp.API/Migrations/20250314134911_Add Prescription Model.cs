using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StLukesMedicalApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPrescriptionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Perscription_PerscriptionId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "DoctorPerscription");

            migrationBuilder.DropTable(
                name: "Perscription");

            migrationBuilder.RenameColumn(
                name: "PerscriptionId",
                table: "Patients",
                newName: "PrescriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_PerscriptionId",
                table: "Patients",
                newName: "IX_Patients_PrescriptionId");

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicationList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateIssued = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorPrescription",
                columns: table => new
                {
                    DoctorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerscriptionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorPrescription", x => new { x.DoctorsId, x.PerscriptionsId });
                    table.ForeignKey(
                        name: "FK_DoctorPrescription_Doctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorPrescription_Prescriptions_PerscriptionsId",
                        column: x => x.PerscriptionsId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPrescription_PerscriptionsId",
                table: "DoctorPrescription",
                column: "PerscriptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Prescriptions_PrescriptionId",
                table: "Patients",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Prescriptions_PrescriptionId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "DoctorPrescription");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "PrescriptionId",
                table: "Patients",
                newName: "PerscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_PrescriptionId",
                table: "Patients",
                newName: "IX_Patients_PerscriptionId");

            migrationBuilder.CreateTable(
                name: "Perscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateIssued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicationList = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perscription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorPerscription",
                columns: table => new
                {
                    DoctorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerscriptionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorPerscription", x => new { x.DoctorsId, x.PerscriptionsId });
                    table.ForeignKey(
                        name: "FK_DoctorPerscription_Doctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorPerscription_Perscription_PerscriptionsId",
                        column: x => x.PerscriptionsId,
                        principalTable: "Perscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPerscription_PerscriptionsId",
                table: "DoctorPerscription",
                column: "PerscriptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Perscription_PerscriptionId",
                table: "Patients",
                column: "PerscriptionId",
                principalTable: "Perscription",
                principalColumn: "Id");
        }
    }
}
