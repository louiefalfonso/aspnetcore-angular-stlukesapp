using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StLukesMedicalApp.API.Migrations
{
    /// <inheritdoc />
    public partial class _5thMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Appointments_AppointmentId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_AppointmentId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Doctors");

            migrationBuilder.CreateTable(
                name: "AppointmentDoctor",
                columns: table => new
                {
                    AppointmentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDoctor", x => new { x.AppointmentsId, x.DoctorsId });
                    table.ForeignKey(
                        name: "FK_AppointmentDoctor_Appointments_AppointmentsId",
                        column: x => x.AppointmentsId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentDoctor_Doctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDoctor_DoctorsId",
                table: "AppointmentDoctor",
                column: "DoctorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentDoctor");

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_AppointmentId",
                table: "Doctors",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Appointments_AppointmentId",
                table: "Doctors",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");
        }
    }
}
