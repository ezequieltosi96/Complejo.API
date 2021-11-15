using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Complejo.Identity.Migrations
{
    public partial class User_Have_Optional_Client : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdClient",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdClient",
                table: "AspNetUsers");
        }
    }
}
