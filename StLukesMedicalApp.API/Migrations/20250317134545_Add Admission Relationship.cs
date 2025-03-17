using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StLukesMedicalApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmissionRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Admissions_AdmissionId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Nurses_Admissions_AdmissionId",
                table: "Nurses");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Admissions_AdmissionId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_AdmissionId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Nurses_AdmissionId",
                table: "Nurses");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_AdmissionId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "AdmissionId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "AdmissionId",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "AdmissionId",
                table: "Doctors");

            migrationBuilder.CreateTable(
                name: "AdmissionDoctor",
                columns: table => new
                {
                    AdmissionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionDoctor", x => new { x.AdmissionsId, x.DoctorsId });
                    table.ForeignKey(
                        name: "FK_AdmissionDoctor_Admissions_AdmissionsId",
                        column: x => x.AdmissionsId,
                        principalTable: "Admissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdmissionDoctor_Doctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "Doctors",
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
                        name: "FK_AdmissionNurse_Admissions_AdmissionsId",
                        column: x => x.AdmissionsId,
                        principalTable: "Admissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdmissionNurse_Nurses_NursesId",
                        column: x => x.NursesId,
                        principalTable: "Nurses",
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
                        name: "FK_AdmissionPatient_Admissions_AdmissionsId",
                        column: x => x.AdmissionsId,
                        principalTable: "Admissions",
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
                name: "IX_AdmissionDoctor_DoctorsId",
                table: "AdmissionDoctor",
                column: "DoctorsId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionNurse_NursesId",
                table: "AdmissionNurse",
                column: "NursesId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionPatient_PatientsId",
                table: "AdmissionPatient",
                column: "PatientsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdmissionDoctor");

            migrationBuilder.DropTable(
                name: "AdmissionNurse");

            migrationBuilder.DropTable(
                name: "AdmissionPatient");

            migrationBuilder.AddColumn<Guid>(
                name: "AdmissionId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AdmissionId",
                table: "Nurses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AdmissionId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AdmissionId",
                table: "Patients",
                column: "AdmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_AdmissionId",
                table: "Nurses",
                column: "AdmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_AdmissionId",
                table: "Doctors",
                column: "AdmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Admissions_AdmissionId",
                table: "Doctors",
                column: "AdmissionId",
                principalTable: "Admissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nurses_Admissions_AdmissionId",
                table: "Nurses",
                column: "AdmissionId",
                principalTable: "Admissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Admissions_AdmissionId",
                table: "Patients",
                column: "AdmissionId",
                principalTable: "Admissions",
                principalColumn: "Id");
        }
    }
}
