using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Complejo.Persistence.Migrations
{
    public partial class Field_Price_Added_Fixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DEF_FieldStatus",
                keyColumn: "Id",
                keyValue: new Guid("76492c2c-2e4a-4fc8-8b0b-eda1604810bd"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldStatus",
                keyColumn: "Id",
                keyValue: new Guid("f1c024dc-cbcb-45d2-a834-a19ab865b84a"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldType",
                keyColumn: "Id",
                keyValue: new Guid("040498cc-92d6-4952-8d38-451435a28e4c"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldType",
                keyColumn: "Id",
                keyValue: new Guid("6461109c-0c03-4e29-9a85-b9183c7ba546"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldType",
                keyColumn: "Id",
                keyValue: new Guid("e1b27fad-18c5-45b2-bba2-3c355f8597f9"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Field",
                type: "decimal",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "DEF_FieldStatus",
                columns: new[] { "Id", "Description", "IdFieldStatusGroup" },
                values: new object[,]
                {
                    { new Guid("762c14e0-9e82-4e19-928c-869a2affd44c"), "Disponible", 1 },
                    { new Guid("edbef389-6fbf-40f2-9e03-a24692351ac3"), "En Mantenimiento", 2 }
                });

            migrationBuilder.InsertData(
                table: "DEF_FieldType",
                columns: new[] { "Id", "Description", "IdFieldTypeGroup" },
                values: new object[,]
                {
                    { new Guid("bb029c38-4690-40bf-806a-8d34a5d497de"), "Fútbol 5", 1 },
                    { new Guid("3b4b39f6-90c3-4f63-959c-b488d00e4de0"), "Fútbol 8", 2 },
                    { new Guid("c07785d8-4571-452c-9cce-b4c45f0b55bf"), "Fútbol 11", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DEF_FieldStatus",
                keyColumn: "Id",
                keyValue: new Guid("762c14e0-9e82-4e19-928c-869a2affd44c"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldStatus",
                keyColumn: "Id",
                keyValue: new Guid("edbef389-6fbf-40f2-9e03-a24692351ac3"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldType",
                keyColumn: "Id",
                keyValue: new Guid("3b4b39f6-90c3-4f63-959c-b488d00e4de0"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldType",
                keyColumn: "Id",
                keyValue: new Guid("bb029c38-4690-40bf-806a-8d34a5d497de"));

            migrationBuilder.DeleteData(
                table: "DEF_FieldType",
                keyColumn: "Id",
                keyValue: new Guid("c07785d8-4571-452c-9cce-b4c45f0b55bf"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Field",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal");

            migrationBuilder.InsertData(
                table: "DEF_FieldStatus",
                columns: new[] { "Id", "Description", "IdFieldStatusGroup" },
                values: new object[,]
                {
                    { new Guid("76492c2c-2e4a-4fc8-8b0b-eda1604810bd"), "Disponible", 1 },
                    { new Guid("f1c024dc-cbcb-45d2-a834-a19ab865b84a"), "En Mantenimiento", 2 }
                });

            migrationBuilder.InsertData(
                table: "DEF_FieldType",
                columns: new[] { "Id", "Description", "IdFieldTypeGroup" },
                values: new object[,]
                {
                    { new Guid("e1b27fad-18c5-45b2-bba2-3c355f8597f9"), "Fútbol 5", 1 },
                    { new Guid("040498cc-92d6-4952-8d38-451435a28e4c"), "Fútbol 8", 2 },
                    { new Guid("6461109c-0c03-4e29-9a85-b9183c7ba546"), "Fútbol 11", 3 }
                });
        }
    }
}
