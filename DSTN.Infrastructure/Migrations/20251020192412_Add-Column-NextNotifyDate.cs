using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSTN.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnNextNotifyDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NextNotificationDate",
                table: "TimeZones",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextNotificationDate",
                table: "TimeZones");
        }
    }
}
