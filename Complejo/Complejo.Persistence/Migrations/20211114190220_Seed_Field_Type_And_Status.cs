using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Complejo.Persistence.Migrations
{
    public partial class Seed_Field_Type_And_Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
