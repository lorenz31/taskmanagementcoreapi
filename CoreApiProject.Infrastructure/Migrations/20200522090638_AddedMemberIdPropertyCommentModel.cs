using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApiProject.Infrastructure.Migrations
{
    public partial class AddedMemberIdPropertyCommentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "Comments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Comments");
        }
    }
}
