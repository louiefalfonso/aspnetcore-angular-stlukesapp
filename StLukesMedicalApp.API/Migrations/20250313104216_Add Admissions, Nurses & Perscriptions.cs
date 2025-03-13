using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StLukesMedicalApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmissionsNursesPerscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NurseId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Admission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailabilityStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nurse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BadgeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qualifications = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicationList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateIssued = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perscription", x => x.Id);
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

            migrationBuilder.CreateIndex(
                name: "IX_Patients_NurseId",
                table: "Patients",
                column: "NurseId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionNurse_NursesId",
                table: "AdmissionNurse",
                column: "NursesId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionPatient_PatientsId",
                table: "AdmissionPatient",
                column: "PatientsId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPerscription_PerscriptionsId",
                table: "DoctorPerscription",
                column: "PerscriptionsId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Nurse_NurseId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "AdmissionNurse");

            migrationBuilder.DropTable(
                name: "AdmissionPatient");

            migrationBuilder.DropTable(
                name: "DoctorPerscription");

            migrationBuilder.DropTable(
                name: "PatientPerscription");

            migrationBuilder.DropTable(
                name: "Nurse");

            migrationBuilder.DropTable(
                name: "Admission");

            migrationBuilder.DropTable(
                name: "Perscription");

            migrationBuilder.DropIndex(
                name: "IX_Patients_NurseId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NurseId",
                table: "Patients");
        }
    }
}
