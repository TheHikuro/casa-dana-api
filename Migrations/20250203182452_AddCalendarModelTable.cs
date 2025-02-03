using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaDanaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCalendarModelTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dates",
                table: "Calendars");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Calendars",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Calendars",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Calendars",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Calendars");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Calendars",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<List<DateTime>>(
                name: "Dates",
                table: "Calendars",
                type: "timestamp with time zone[]",
                nullable: false);
        }
    }
}
