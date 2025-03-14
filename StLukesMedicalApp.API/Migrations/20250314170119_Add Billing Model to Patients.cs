using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StLukesMedicalApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBillingModeltoPatients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BillingId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBilling = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Billings_BillingId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.DropIndex(
                name: "IX_Patients_BillingId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "BillingId",
                table: "Patients");
        }
    }
}
