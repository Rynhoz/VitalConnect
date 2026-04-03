using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VitalConnect_API.Migrations
{
    /// <inheritdoc />
    public partial class vtcproyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicamentos",
                columns: table => new
                {
                    MedicamentoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Estado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamentos", x => x.MedicamentoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreCompleto = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false),
                    CI = table.Column<string>(type: "TEXT", nullable: false),
                    Rol = table.Column<string>(type: "TEXT", nullable: false),
                    Estado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Asistentes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Turno = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistentes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Asistentes_Usuarios_ID",
                        column: x => x.ID,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaNacimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Genero = table.Column<string>(type: "TEXT", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pacientes_Usuarios_ID",
                        column: x => x.ID,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profesionales",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatriculaProfesional = table.Column<string>(type: "TEXT", nullable: false),
                    Especialidad = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesionales", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Profesionales_Usuarios_ID",
                        column: x => x.ID,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    CitaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Hora = table.Column<string>(type: "TEXT", nullable: false),
                    EstadoCita = table.Column<string>(type: "TEXT", nullable: false),
                    Estado = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdPaciente = table.Column<int>(type: "INTEGER", nullable: false),
                    PacienteID = table.Column<int>(type: "INTEGER", nullable: false),
                    IdProfesional = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfesionalID = table.Column<int>(type: "INTEGER", nullable: false),
                    IdAsistente = table.Column<int>(type: "INTEGER", nullable: true),
                    AsistenteID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.CitaId);
                    table.ForeignKey(
                        name: "FK_Citas_Asistentes_AsistenteID",
                        column: x => x.AsistenteID,
                        principalTable: "Asistentes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Citas_Pacientes_PacienteID",
                        column: x => x.PacienteID,
                        principalTable: "Pacientes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Citas_Profesionales_ProfesionalID",
                        column: x => x.ProfesionalID,
                        principalTable: "Profesionales",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FichaAtencion",
                columns: table => new
                {
                    FichaAtencionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaAtencion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Diagnostico = table.Column<string>(type: "TEXT", nullable: false),
                    Indicaciones = table.Column<string>(type: "TEXT", nullable: false),
                    IdCita = table.Column<int>(type: "INTEGER", nullable: false),
                    CitaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaAtencion", x => x.FichaAtencionId);
                    table.ForeignKey(
                        name: "FK_FichaAtencion_Citas_CitaId",
                        column: x => x.CitaId,
                        principalTable: "Citas",
                        principalColumn: "CitaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recetas",
                columns: table => new
                {
                    RecetaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Observaciones = table.Column<string>(type: "TEXT", nullable: true),
                    IdFicha = table.Column<int>(type: "INTEGER", nullable: false),
                    FichaAtencionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recetas", x => x.RecetaId);
                    table.ForeignKey(
                        name: "FK_Recetas_FichaAtencion_FichaAtencionId",
                        column: x => x.FichaAtencionId,
                        principalTable: "FichaAtencion",
                        principalColumn: "FichaAtencionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleReceta",
                columns: table => new
                {
                    DetalleRecetaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Dosis = table.Column<string>(type: "TEXT", nullable: false),
                    Frecuencia = table.Column<string>(type: "TEXT", nullable: false),
                    Duracion = table.Column<string>(type: "TEXT", nullable: false),
                    IdReceta = table.Column<int>(type: "INTEGER", nullable: false),
                    RecetaId = table.Column<int>(type: "INTEGER", nullable: false),
                    IdMedicamento = table.Column<int>(type: "INTEGER", nullable: false),
                    MedicamentoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleReceta", x => x.DetalleRecetaId);
                    table.ForeignKey(
                        name: "FK_DetalleReceta_Medicamentos_MedicamentoId",
                        column: x => x.MedicamentoId,
                        principalTable: "Medicamentos",
                        principalColumn: "MedicamentoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleReceta_Recetas_RecetaId",
                        column: x => x.RecetaId,
                        principalTable: "Recetas",
                        principalColumn: "RecetaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citas_AsistenteID",
                table: "Citas",
                column: "AsistenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_PacienteID",
                table: "Citas",
                column: "PacienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_ProfesionalID",
                table: "Citas",
                column: "ProfesionalID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleReceta_MedicamentoId",
                table: "DetalleReceta",
                column: "MedicamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleReceta_RecetaId",
                table: "DetalleReceta",
                column: "RecetaId");

            migrationBuilder.CreateIndex(
                name: "IX_FichaAtencion_CitaId",
                table: "FichaAtencion",
                column: "CitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_FichaAtencionId",
                table: "Recetas",
                column: "FichaAtencionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleReceta");

            migrationBuilder.DropTable(
                name: "Medicamentos");

            migrationBuilder.DropTable(
                name: "Recetas");

            migrationBuilder.DropTable(
                name: "FichaAtencion");

            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Asistentes");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Profesionales");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
