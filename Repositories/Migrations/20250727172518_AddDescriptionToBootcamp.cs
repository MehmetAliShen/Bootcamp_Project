using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToBootcamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Bootcamps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bootcamps_InstructorId",
                table: "Bootcamps",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bootcamps_Users_InstructorId",
                table: "Bootcamps",
                column: "InstructorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bootcamps_Users_InstructorId",
                table: "Bootcamps");

            migrationBuilder.DropIndex(
                name: "IX_Bootcamps_InstructorId",
                table: "Bootcamps");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Bootcamps");
        }
    }
}
