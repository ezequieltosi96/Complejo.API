using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Complejo.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DEF_FieldStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DescriptionSearch = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, computedColumnSql: "upper([Description])"),
                    IdFieldStatusGroup = table.Column<int>(type: "int", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEF_FieldStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DEF_FieldType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DescriptionSearch = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, computedColumnSql: "upper([Description])"),
                    IdFieldTypeGroup = table.Column<int>(type: "int", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEF_FieldType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DescriptionSearch = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, computedColumnSql: "upper([Description])"),
                    IdFieldStatus = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFieldType = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    CreatedBy = table.Column<string>(type: "varchar", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Field_DEF_FieldStatus_IdFieldStatus",
                        column: x => x.IdFieldStatus,
                        principalTable: "DEF_FieldStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Field_DEF_FieldType_IdFieldType",
                        column: x => x.IdFieldType,
                        principalTable: "DEF_FieldType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Field_IdFieldStatus",
                table: "Field",
                column: "IdFieldStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Field_IdFieldType",
                table: "Field",
                column: "IdFieldType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "DEF_FieldStatus");

            migrationBuilder.DropTable(
                name: "DEF_FieldType");
        }
    }
}
