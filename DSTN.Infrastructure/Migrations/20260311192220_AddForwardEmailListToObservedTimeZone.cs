using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSTN.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddForwardEmailListToObservedTimeZone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForwardEmailList",
                table: "TimeZones",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForwardEmailList",
                table: "TimeZones");
        }
    }
}
