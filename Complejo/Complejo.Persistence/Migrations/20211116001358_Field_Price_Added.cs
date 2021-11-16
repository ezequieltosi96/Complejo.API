using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Complejo.Persistence.Migrations
{
    public partial class Field_Price_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Field",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Field");

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
    }
}
