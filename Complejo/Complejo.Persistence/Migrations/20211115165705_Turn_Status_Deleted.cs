using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Complejo.Persistence.Migrations
{
    public partial class Turn_Status_Deleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turn_DEF_TurnStatus_IdTurnStatus",
                table: "Turn");

            migrationBuilder.DropTable(
                name: "DEF_TurnStatus");

            migrationBuilder.DropIndex(
                name: "IX_Turn_IdTurnStatus",
                table: "Turn");

            migrationBuilder.DeleteData(
                table: "DEF_FieldStatus",
                keyColumn: "Id",
                keyValue: new Guid("9a026ee8-4306-4a83-8e9e-84a2a73514c3"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldStatus",
                keyColumn: "Id",
                keyValue: new Guid("9b26959f-6dfa-4502-b363-aa5ac75aa2af"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldType",
                keyColumn: "Id",
                keyValue: new Guid("277d069d-b1b7-4513-8c32-d40e3d3ae77a"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldType",
                keyColumn: "Id",
                keyValue: new Guid("49eb8281-5f3d-4460-a513-01fdc323547b"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldType",
                keyColumn: "Id",
                keyValue: new Guid("c6f6df98-7a9a-4820-9d78-fc2f89346e3a"));

            migrationBuilder.DropColumn(
                name: "IdTurnStatus",
                table: "Turn");

            migrationBuilder.InsertData(
                table: "DEF_FieldStatus",
                columns: new[] { "Id", "Description", "IdFieldStatusGroup" },
                values: new object[,]
                {
                    { new Guid("76fdb5f8-34c9-4a81-8c3f-0de8e4e92286"), "Disponible", 1 },
                    { new Guid("d9499a3d-6c85-4838-9ea7-e307c9eb002f"), "En Mantenimiento", 2 }
                });

            migrationBuilder.InsertData(
                table: "DEF_FieldType",
                columns: new[] { "Id", "Description", "IdFieldTypeGroup" },
                values: new object[,]
                {
                    { new Guid("0921a032-084e-4506-a5f9-c039e0b50a6d"), "Fútbol 5", 1 },
                    { new Guid("dfd57843-a183-464f-8209-803ce807f66b"), "Fútbol 8", 2 },
                    { new Guid("8cb8a211-2a30-4b0a-b2db-36170dd467ee"), "Fútbol 11", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DEF_FieldStatus",
                keyColumn: "Id",
                keyValue: new Guid("76fdb5f8-34c9-4a81-8c3f-0de8e4e92286"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldStatus",
                keyColumn: "Id",
                keyValue: new Guid("d9499a3d-6c85-4838-9ea7-e307c9eb002f"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldType",
                keyColumn: "Id",
                keyValue: new Guid("0921a032-084e-4506-a5f9-c039e0b50a6d"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldType",
                keyColumn: "Id",
                keyValue: new Guid("8cb8a211-2a30-4b0a-b2db-36170dd467ee"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldType",
                keyColumn: "Id",
                keyValue: new Guid("dfd57843-a183-464f-8209-803ce807f66b"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdTurnStatus",
                table: "Turn",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.InsertData(
                table: "DEF_FieldStatus",
                columns: new[] { "Id", "Description", "IdFieldStatusGroup" },
                values: new object[,]
                {
                    { new Guid("9a026ee8-4306-4a83-8e9e-84a2a73514c3"), "Disponible", 1 },
                    { new Guid("9b26959f-6dfa-4502-b363-aa5ac75aa2af"), "En Mantenimiento", 2 }
                });

            migrationBuilder.InsertData(
                table: "DEF_FieldType",
                columns: new[] { "Id", "Description", "IdFieldTypeGroup" },
                values: new object[,]
                {
                    { new Guid("49eb8281-5f3d-4460-a513-01fdc323547b"), "Fútbol 5", 1 },
                    { new Guid("c6f6df98-7a9a-4820-9d78-fc2f89346e3a"), "Fútbol 8", 2 },
                    { new Guid("277d069d-b1b7-4513-8c32-d40e3d3ae77a"), "Fútbol 11", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turn_IdTurnStatus",
                table: "Turn",
                column: "IdTurnStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_Turn_DEF_TurnStatus_IdTurnStatus",
                table: "Turn",
                column: "IdTurnStatus",
                principalTable: "DEF_TurnStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
