using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StLukesMedicalApp.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Nurse_NurseId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "AdmissionNurse");

            migrationBuilder.DropTable(
                name: "AdmissionPatient");

            migrationBuilder.DropTable(
                name: "PatientPerscription");

            migrationBuilder.DropTable(
                name: "Admission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nurse",
                table: "Nurse");

            migrationBuilder.RenameTable(
                name: "Nurse",
                newName: "Nurses");

            migrationBuilder.AddColumn<Guid>(
                name: "PerscriptionId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nurses",
                table: "Nurses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PerscriptionId",
                table: "Patients",
                column: "PerscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Nurses_NurseId",
                table: "Patients",
                column: "NurseId",
                principalTable: "Nurses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Perscription_PerscriptionId",
                table: "Patients",
                column: "PerscriptionId",
                principalTable: "Perscription",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Nurses_NurseId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Perscription_PerscriptionId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PerscriptionId",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nurses",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "PerscriptionId",
                table: "Patients");

            migrationBuilder.RenameTable(
                name: "Nurses",
                newName: "Nurse");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nurse",
                table: "Nurse",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Admission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvailabilityStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientPerscription",
                columns: table => new
                {
                    PatientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerscriptionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPerscription", x => new { x.PatientsId, x.PerscriptionsId });
                    table.ForeignKey(
                        name: "FK_PatientPerscription_Patients_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientPerscription_Perscription_PerscriptionsId",
                        column: x => x.PerscriptionsId,
                        principalTable: "Perscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdmissionNurse",
                columns: table => new
                {
                    AdmissionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NursesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionNurse", x => new { x.AdmissionsId, x.NursesId });
                    table.ForeignKey(
                        name: "FK_AdmissionNurse_Admission_AdmissionsId",
                        column: x => x.AdmissionsId,
                        principalTable: "Admission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdmissionNurse_Nurse_NursesId",
                        column: x => x.NursesId,
                        principalTable: "Nurse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdmissionPatient",
                columns: table => new
                {
                    AdmissionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionPatient", x => new { x.AdmissionsId, x.PatientsId });
                    table.ForeignKey(
                        name: "FK_AdmissionPatient_Admission_AdmissionsId",
                        column: x => x.AdmissionsId,
                        principalTable: "Admission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdmissionPatient_Patients_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionNurse_NursesId",
                table: "AdmissionNurse",
                column: "NursesId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionPatient_PatientsId",
                table: "AdmissionPatient",
                column: "PatientsId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPerscription_PerscriptionsId",
                table: "PatientPerscription",
                column: "PerscriptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Nurse_NurseId",
                table: "Patients",
                column: "NurseId",
                principalTable: "Nurse",
                principalColumn: "Id");
        }
    }
}
