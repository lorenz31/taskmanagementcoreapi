using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApiProject.Infrastructure.Migrations
{
    public partial class AddedIsPriorityPropertyToTaskModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPriority",
                table: "Tasks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPriority",
                table: "Tasks");
        }
    }
}
