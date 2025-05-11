using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medi.Server.Migrations
{
    /// <inheritdoc />
    public partial class addForeiginKeyToAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentSlots_AppointmentSlotId",
                table: "Appointments");
            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppointmentSlotId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentSlotId",
                table: "Appointments",
                column: "AppointmentSlotId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentSlots_AppointmentSlotId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppointmentSlotId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentSlotId",
                table: "Appointments",
                column: "AppointmentSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentSlots_AppointmentSlotId",
                table: "Appointments",
                column: "AppointmentSlotId",
                principalTable: "AppointmentSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
