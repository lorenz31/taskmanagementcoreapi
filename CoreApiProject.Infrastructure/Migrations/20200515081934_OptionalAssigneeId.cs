using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApiProject.Infrastructure.Migrations
{
    public partial class OptionalAssigneeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "AssigneeId",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "AssigneeId",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
