using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Personas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_CatalogoItems_EstadoCivilId",
                table: "Pacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_CatalogoItems_ExtensionDocumentoId",
                table: "Pacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_CatalogoItems_SexoId",
                table: "Pacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_CatalogoItems_TipoDocumentoId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_EstadoCivilId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_ExtensionDocumentoId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_SexoId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_TipoDocumentoId_NumeroDocumento_ComplementoDocumento_ExtensionDocumentoId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "ApellidoMaterno",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "ApellidoPaterno",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "ComplementoDocumento",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "EstadoCivilId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "ExtensionDocumentoId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Nombres",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "NumeroDocumento",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "SexoId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Pacientes");

            migrationBuilder.EnsureSchema(
                name: "Personas");

            migrationBuilder.RenameTable(
                name: "Pacientes",
                newName: "Pacientes",
                newSchema: "Personas");

            migrationBuilder.RenameColumn(
                name: "TipoDocumentoId",
                schema: "Personas",
                table: "Pacientes",
                newName: "PersonaId");

            migrationBuilder.CreateTable(
                name: "Personas",
                schema: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ExtensionDocumentoId = table.Column<int>(type: "int", nullable: true),
                    ComplementoDocumento = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SexoId = table.Column<int>(type: "int", nullable: false),
                    EstadoCivilId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personas_CatalogoItems_EstadoCivilId",
                        column: x => x.EstadoCivilId,
                        principalTable: "CatalogoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personas_CatalogoItems_ExtensionDocumentoId",
                        column: x => x.ExtensionDocumentoId,
                        principalTable: "CatalogoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personas_CatalogoItems_SexoId",
                        column: x => x.SexoId,
                        principalTable: "CatalogoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personas_CatalogoItems_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "CatalogoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                schema: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    ServicioId = table.Column<int>(type: "int", nullable: false),
                    ProfesionId = table.Column<int>(type: "int", nullable: false),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleados_Areas_AreaId",
                        column: x => x.AreaId,
                        principalSchema: "Catalogos",
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleados_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalSchema: "Catalogos",
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleados_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalSchema: "Catalogos",
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleados_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalSchema: "Personas",
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleados_Profesiones_ProfesionId",
                        column: x => x.ProfesionId,
                        principalSchema: "Catalogos",
                        principalTable: "Profesiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleados_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalSchema: "Catalogos",
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                schema: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    EspecialidadId = table.Column<int>(type: "int", nullable: false),
                    MatriculaProfesional = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicos_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalSchema: "Personas",
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicos_Especialidades_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalSchema: "Catalogos",
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_PersonaId",
                schema: "Personas",
                table: "Pacientes",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_AreaId",
                schema: "Personas",
                table: "Empleados",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_CargoId",
                schema: "Personas",
                table: "Empleados",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_DepartamentoId",
                schema: "Personas",
                table: "Empleados",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_PersonaId",
                schema: "Personas",
                table: "Empleados",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_ProfesionId",
                schema: "Personas",
                table: "Empleados",
                column: "ProfesionId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_ServicioId",
                schema: "Personas",
                table: "Empleados",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_EmpleadoId",
                schema: "Personas",
                table: "Medicos",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_EspecialidadId",
                schema: "Personas",
                table: "Medicos",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_EstadoCivilId",
                schema: "Personas",
                table: "Personas",
                column: "EstadoCivilId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_ExtensionDocumentoId",
                schema: "Personas",
                table: "Personas",
                column: "ExtensionDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_SexoId",
                schema: "Personas",
                table: "Personas",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_TipoDocumentoId_NumeroDocumento_ComplementoDocumento_ExtensionDocumentoId",
                schema: "Personas",
                table: "Personas",
                columns: new[] { "TipoDocumentoId", "NumeroDocumento", "ComplementoDocumento", "ExtensionDocumentoId" },
                unique: true,
                filter: "[ComplementoDocumento] IS NOT NULL AND [ExtensionDocumentoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Personas_PersonaId",
                schema: "Personas",
                table: "Pacientes",
                column: "PersonaId",
                principalSchema: "Personas",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Personas_PersonaId",
                schema: "Personas",
                table: "Pacientes");

            migrationBuilder.DropTable(
                name: "Medicos",
                schema: "Personas");

            migrationBuilder.DropTable(
                name: "Empleados",
                schema: "Personas");

            migrationBuilder.DropTable(
                name: "Personas",
                schema: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_PersonaId",
                schema: "Personas",
                table: "Pacientes");

            migrationBuilder.RenameTable(
                name: "Pacientes",
                schema: "Personas",
                newName: "Pacientes");

            migrationBuilder.RenameColumn(
                name: "PersonaId",
                table: "Pacientes",
                newName: "TipoDocumentoId");

            migrationBuilder.AddColumn<string>(
                name: "ApellidoMaterno",
                table: "Pacientes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApellidoPaterno",
                table: "Pacientes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ComplementoDocumento",
                table: "Pacientes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Pacientes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EstadoCivilId",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExtensionDocumentoId",
                table: "Pacientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaNacimiento",
                table: "Pacientes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Nombres",
                table: "Pacientes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumeroDocumento",
                table: "Pacientes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SexoId",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Pacientes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_EstadoCivilId",
                table: "Pacientes",
                column: "EstadoCivilId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_ExtensionDocumentoId",
                table: "Pacientes",
                column: "ExtensionDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_SexoId",
                table: "Pacientes",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_TipoDocumentoId_NumeroDocumento_ComplementoDocumento_ExtensionDocumentoId",
                table: "Pacientes",
                columns: new[] { "TipoDocumentoId", "NumeroDocumento", "ComplementoDocumento", "ExtensionDocumentoId" },
                unique: true,
                filter: "[ComplementoDocumento] IS NOT NULL AND [ExtensionDocumentoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_CatalogoItems_EstadoCivilId",
                table: "Pacientes",
                column: "EstadoCivilId",
                principalTable: "CatalogoItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_CatalogoItems_ExtensionDocumentoId",
                table: "Pacientes",
                column: "ExtensionDocumentoId",
                principalTable: "CatalogoItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_CatalogoItems_SexoId",
                table: "Pacientes",
                column: "SexoId",
                principalTable: "CatalogoItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_CatalogoItems_TipoDocumentoId",
                table: "Pacientes",
                column: "TipoDocumentoId",
                principalTable: "CatalogoItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
