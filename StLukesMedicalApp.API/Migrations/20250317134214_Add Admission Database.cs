using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StLukesMedicalApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmissionDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Admissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailabilityStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admissions", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Admissions");

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
        }
    }
}
