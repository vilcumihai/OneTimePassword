using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneTimePassword.DataAccess.Migrations
{
    public partial class AddOtpToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OTPCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpExpirationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OTPCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OtpExpirationDate",
                table: "AspNetUsers");
        }
    }
}
