using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VitalConnect_API.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeysCita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Asistentes_AsistenteID",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Pacientes_PacienteID",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Profesionales_ProfesionalID",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleReceta_Medicamentos_MedicamentoId",
                table: "DetalleReceta");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleReceta_Recetas_RecetaId",
                table: "DetalleReceta");

            migrationBuilder.DropForeignKey(
                name: "FK_FichaAtencion_Citas_CitaId",
                table: "FichaAtencion");

            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_FichaAtencion_FichaAtencionId",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_AsistenteID",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_PacienteID",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_ProfesionalID",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "AsistenteID",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "PacienteID",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "ProfesionalID",
                table: "Citas");

            migrationBuilder.AlterColumn<int>(
                name: "FichaAtencionId",
                table: "Recetas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "CitaId",
                table: "FichaAtencion",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "RecetaId",
                table: "DetalleReceta",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "MedicamentoId",
                table: "DetalleReceta",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_IdAsistente",
                table: "Citas",
                column: "IdAsistente");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_IdPaciente",
                table: "Citas",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_IdProfesional",
                table: "Citas",
                column: "IdProfesional");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Asistentes_IdAsistente",
                table: "Citas",
                column: "IdAsistente",
                principalTable: "Asistentes",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Pacientes_IdPaciente",
                table: "Citas",
                column: "IdPaciente",
                principalTable: "Pacientes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Profesionales_IdProfesional",
                table: "Citas",
                column: "IdProfesional",
                principalTable: "Profesionales",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleReceta_Medicamentos_MedicamentoId",
                table: "DetalleReceta",
                column: "MedicamentoId",
                principalTable: "Medicamentos",
                principalColumn: "MedicamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleReceta_Recetas_RecetaId",
                table: "DetalleReceta",
                column: "RecetaId",
                principalTable: "Recetas",
                principalColumn: "RecetaId");

            migrationBuilder.AddForeignKey(
                name: "FK_FichaAtencion_Citas_CitaId",
                table: "FichaAtencion",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "CitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_FichaAtencion_FichaAtencionId",
                table: "Recetas",
                column: "FichaAtencionId",
                principalTable: "FichaAtencion",
                principalColumn: "FichaAtencionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Asistentes_IdAsistente",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Pacientes_IdPaciente",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Profesionales_IdProfesional",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleReceta_Medicamentos_MedicamentoId",
                table: "DetalleReceta");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleReceta_Recetas_RecetaId",
                table: "DetalleReceta");

            migrationBuilder.DropForeignKey(
                name: "FK_FichaAtencion_Citas_CitaId",
                table: "FichaAtencion");

            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_FichaAtencion_FichaAtencionId",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_IdAsistente",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_IdPaciente",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_IdProfesional",
                table: "Citas");

            migrationBuilder.AlterColumn<int>(
                name: "FichaAtencionId",
                table: "Recetas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CitaId",
                table: "FichaAtencion",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecetaId",
                table: "DetalleReceta",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MedicamentoId",
                table: "DetalleReceta",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AsistenteID",
                table: "Citas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteID",
                table: "Citas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfesionalID",
                table: "Citas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Asistentes_AsistenteID",
                table: "Citas",
                column: "AsistenteID",
                principalTable: "Asistentes",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Pacientes_PacienteID",
                table: "Citas",
                column: "PacienteID",
                principalTable: "Pacientes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Profesionales_ProfesionalID",
                table: "Citas",
                column: "ProfesionalID",
                principalTable: "Profesionales",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleReceta_Medicamentos_MedicamentoId",
                table: "DetalleReceta",
                column: "MedicamentoId",
                principalTable: "Medicamentos",
                principalColumn: "MedicamentoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleReceta_Recetas_RecetaId",
                table: "DetalleReceta",
                column: "RecetaId",
                principalTable: "Recetas",
                principalColumn: "RecetaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FichaAtencion_Citas_CitaId",
                table: "FichaAtencion",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "CitaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_FichaAtencion_FichaAtencionId",
                table: "Recetas",
                column: "FichaAtencionId",
                principalTable: "FichaAtencion",
                principalColumn: "FichaAtencionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
