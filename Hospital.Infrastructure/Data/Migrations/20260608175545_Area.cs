using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Area : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                schema: "Catalogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_Codigo",
                schema: "Catalogos",
                table: "Areas",
                column: "Codigo",
                unique: true);

            migrationBuilder.Sql("""
                INSERT INTO Catalogos.Areas (Codigo, Nombre, Descripcion, CreatedAt, CreatedBy, Activo)
                VALUES ('GENERAL', 'General', 'Área por defecto para departamentos existentes', GETUTCDATE(), 'Sistema', 1);
                """);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                schema: "Catalogos",
                table: "Departamentos",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_AreaId",
                schema: "Catalogos",
                table: "Departamentos",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departamentos_Areas_AreaId",
                schema: "Catalogos",
                table: "Departamentos",
                column: "AreaId",
                principalSchema: "Catalogos",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departamentos_Areas_AreaId",
                schema: "Catalogos",
                table: "Departamentos");

            migrationBuilder.DropTable(
                name: "Areas",
                schema: "Catalogos");

            migrationBuilder.DropIndex(
                name: "IX_Departamentos_AreaId",
                schema: "Catalogos",
                table: "Departamentos");

            migrationBuilder.DropColumn(
                name: "AreaId",
                schema: "Catalogos",
                table: "Departamentos");
        }
    }
}
