﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medi.Server.Migrations
{
    /// <inheritdoc />
    public partial class nullable_user_medicalfac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalFacilities_Users_UserId",
                table: "MedicalFacilities");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MedicalFacilities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalFacilities_Users_UserId",
                table: "MedicalFacilities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalFacilities_Users_UserId",
                table: "MedicalFacilities");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MedicalFacilities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalFacilities_Users_UserId",
                table: "MedicalFacilities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
