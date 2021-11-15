using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Complejo.Persistence.Migrations
{
    public partial class Updated_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    FullNameSearch = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false, computedColumnSql: "upper([FullName])"),
                    PhoneNumber = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    Dni = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

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
                name: "DEF_TurnStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DescriptionSearch = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, computedColumnSql: "upper([Description])"),
                    IdTurnStatusGroup = table.Column<int>(type: "int", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEF_TurnStatus", x => x.Id);
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
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Turn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "varchar(7)", maxLength: 7, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdField = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdClient = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTurnStatus = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turn_Client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turn_DEF_TurnStatus_IdTurnStatus",
                        column: x => x.IdTurnStatus,
                        principalTable: "DEF_TurnStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turn_Field_IdField",
                        column: x => x.IdField,
                        principalTable: "Field",
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

            migrationBuilder.CreateIndex(
                name: "IX_Turn_IdClient",
                table: "Turn",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Turn_IdField",
                table: "Turn",
                column: "IdField");

            migrationBuilder.CreateIndex(
                name: "IX_Turn_IdTurnStatus",
                table: "Turn",
                column: "IdTurnStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Turn");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "DEF_TurnStatus");

            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "DEF_FieldStatus");

            migrationBuilder.DropTable(
                name: "DEF_FieldType");
        }
    }
}
