using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoachingWebsite.Migrations
{
    /// <inheritdoc />
    public partial class AddFeesStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FeesPaid",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeesPaid",
                table: "Students");
        }
    }
}
