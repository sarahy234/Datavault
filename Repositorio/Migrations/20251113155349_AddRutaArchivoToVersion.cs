using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class AddRutaArchivoToVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinaciones_Usuarios_Id_Usuario",
                table: "Coordinaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Docentes_Usuarios_Id_Usuario",
                table: "Docentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Usuarios_Id_Usuario",
                table: "Estudiantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Gestores_Usuarios_Id_Usuario",
                table: "Gestores");

            migrationBuilder.DropForeignKey(
                name: "FK_Metadatos_Recursos_Id_Recurso",
                table: "Metadatos");

            migrationBuilder.DropForeignKey(
                name: "FK_Metricas_Recursos_Id_Recurso",
                table: "Metricas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Recursos_Id_Recurso",
                table: "Notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Usuarios_Id_Usuario",
                table: "Notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Recursos_Docentes_Id_Docente",
                table: "Recursos");

            migrationBuilder.DropForeignKey(
                name: "FK_TIs_Usuarios_Id_Usuario",
                table: "TIs");

            migrationBuilder.DropForeignKey(
                name: "FK_Validacion_Gestores_Id_Gestor",
                table: "Validacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Validacion_Recursos_Id_Recurso",
                table: "Validacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Versiones_Recursos_Id_Recurso",
                table: "Versiones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Versiones",
                table: "Versiones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TIs",
                table: "TIs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recursos",
                table: "Recursos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notificaciones",
                table: "Notificaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metricas",
                table: "Metricas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metadatos",
                table: "Metadatos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gestores",
                table: "Gestores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estudiantes",
                table: "Estudiantes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Docentes",
                table: "Docentes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coordinaciones",
                table: "Coordinaciones");

            migrationBuilder.RenameTable(
                name: "Versiones",
                newName: "Version");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "TIs",
                newName: "TI");

            migrationBuilder.RenameTable(
                name: "Recursos",
                newName: "Recurso");

            migrationBuilder.RenameTable(
                name: "Notificaciones",
                newName: "Notificacion");

            migrationBuilder.RenameTable(
                name: "Metricas",
                newName: "Metrica");

            migrationBuilder.RenameTable(
                name: "Metadatos",
                newName: "Metadato");

            migrationBuilder.RenameTable(
                name: "Gestores",
                newName: "Gestor");

            migrationBuilder.RenameTable(
                name: "Estudiantes",
                newName: "Estudiante");

            migrationBuilder.RenameTable(
                name: "Docentes",
                newName: "Docente");

            migrationBuilder.RenameTable(
                name: "Coordinaciones",
                newName: "Coordinacion");

            migrationBuilder.RenameIndex(
                name: "IX_Versiones_Id_Recurso",
                table: "Version",
                newName: "IX_Version_Id_Recurso");

            migrationBuilder.RenameColumn(
                name: "Contraseña",
                table: "Usuario",
                newName: "Contrasena");

            migrationBuilder.RenameIndex(
                name: "IX_TIs_Id_Usuario",
                table: "TI",
                newName: "IX_TI_Id_Usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Recursos_Id_Docente",
                table: "Recurso",
                newName: "IX_Recurso_Id_Docente");

            migrationBuilder.RenameIndex(
                name: "IX_Notificaciones_Id_Usuario",
                table: "Notificacion",
                newName: "IX_Notificacion_Id_Usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Notificaciones_Id_Recurso",
                table: "Notificacion",
                newName: "IX_Notificacion_Id_Recurso");

            migrationBuilder.RenameIndex(
                name: "IX_Metricas_Id_Recurso",
                table: "Metrica",
                newName: "IX_Metrica_Id_Recurso");

            migrationBuilder.RenameIndex(
                name: "IX_Metadatos_Id_Recurso",
                table: "Metadato",
                newName: "IX_Metadato_Id_Recurso");

            migrationBuilder.RenameIndex(
                name: "IX_Gestores_Id_Usuario",
                table: "Gestor",
                newName: "IX_Gestor_Id_Usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Estudiantes_Id_Usuario",
                table: "Estudiante",
                newName: "IX_Estudiante_Id_Usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Docentes_Id_Usuario",
                table: "Docente",
                newName: "IX_Docente_Id_Usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Coordinaciones_Id_Usuario",
                table: "Coordinacion",
                newName: "IX_Coordinacion_Id_Usuario");

            migrationBuilder.AddColumn<string>(
                name: "Ruta_Archivo",
                table: "Version",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "BloqueadoHasta",
                table: "Usuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IntentosFallidos",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Version",
                table: "Version",
                column: "Id_Version");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id_Usuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TI",
                table: "TI",
                column: "Id_TI");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recurso",
                table: "Recurso",
                column: "Id_Recurso");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notificacion",
                table: "Notificacion",
                column: "Id_Notificacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metrica",
                table: "Metrica",
                column: "Id_Metrica");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metadato",
                table: "Metadato",
                column: "Id_Metadato");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gestor",
                table: "Gestor",
                column: "Id_Gestor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estudiante",
                table: "Estudiante",
                column: "Id_Estudiante");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Docente",
                table: "Docente",
                column: "Id_Docente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coordinacion",
                table: "Coordinacion",
                column: "Id_Coordinacion");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinacion_Usuario_Id_Usuario",
                table: "Coordinacion",
                column: "Id_Usuario",
                principalTable: "Usuario",
                principalColumn: "Id_Usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Docente_Usuario_Id_Usuario",
                table: "Docente",
                column: "Id_Usuario",
                principalTable: "Usuario",
                principalColumn: "Id_Usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiante_Usuario_Id_Usuario",
                table: "Estudiante",
                column: "Id_Usuario",
                principalTable: "Usuario",
                principalColumn: "Id_Usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gestor_Usuario_Id_Usuario",
                table: "Gestor",
                column: "Id_Usuario",
                principalTable: "Usuario",
                principalColumn: "Id_Usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metadato_Recurso_Id_Recurso",
                table: "Metadato",
                column: "Id_Recurso",
                principalTable: "Recurso",
                principalColumn: "Id_Recurso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrica_Recurso_Id_Recurso",
                table: "Metrica",
                column: "Id_Recurso",
                principalTable: "Recurso",
                principalColumn: "Id_Recurso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificacion_Recurso_Id_Recurso",
                table: "Notificacion",
                column: "Id_Recurso",
                principalTable: "Recurso",
                principalColumn: "Id_Recurso");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificacion_Usuario_Id_Usuario",
                table: "Notificacion",
                column: "Id_Usuario",
                principalTable: "Usuario",
                principalColumn: "Id_Usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recurso_Docente_Id_Docente",
                table: "Recurso",
                column: "Id_Docente",
                principalTable: "Docente",
                principalColumn: "Id_Docente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TI_Usuario_Id_Usuario",
                table: "TI",
                column: "Id_Usuario",
                principalTable: "Usuario",
                principalColumn: "Id_Usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Validacion_Gestor_Id_Gestor",
                table: "Validacion",
                column: "Id_Gestor",
                principalTable: "Gestor",
                principalColumn: "Id_Gestor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Validacion_Recurso_Id_Recurso",
                table: "Validacion",
                column: "Id_Recurso",
                principalTable: "Recurso",
                principalColumn: "Id_Recurso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Version_Recurso_Id_Recurso",
                table: "Version",
                column: "Id_Recurso",
                principalTable: "Recurso",
                principalColumn: "Id_Recurso",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinacion_Usuario_Id_Usuario",
                table: "Coordinacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Docente_Usuario_Id_Usuario",
                table: "Docente");

            migrationBuilder.DropForeignKey(
                name: "FK_Estudiante_Usuario_Id_Usuario",
                table: "Estudiante");

            migrationBuilder.DropForeignKey(
                name: "FK_Gestor_Usuario_Id_Usuario",
                table: "Gestor");

            migrationBuilder.DropForeignKey(
                name: "FK_Metadato_Recurso_Id_Recurso",
                table: "Metadato");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrica_Recurso_Id_Recurso",
                table: "Metrica");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificacion_Recurso_Id_Recurso",
                table: "Notificacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificacion_Usuario_Id_Usuario",
                table: "Notificacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Recurso_Docente_Id_Docente",
                table: "Recurso");

            migrationBuilder.DropForeignKey(
                name: "FK_TI_Usuario_Id_Usuario",
                table: "TI");

            migrationBuilder.DropForeignKey(
                name: "FK_Validacion_Gestor_Id_Gestor",
                table: "Validacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Validacion_Recurso_Id_Recurso",
                table: "Validacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Version_Recurso_Id_Recurso",
                table: "Version");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Version",
                table: "Version");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TI",
                table: "TI");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recurso",
                table: "Recurso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notificacion",
                table: "Notificacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metrica",
                table: "Metrica");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metadato",
                table: "Metadato");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gestor",
                table: "Gestor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estudiante",
                table: "Estudiante");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Docente",
                table: "Docente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coordinacion",
                table: "Coordinacion");

            migrationBuilder.DropColumn(
                name: "Ruta_Archivo",
                table: "Version");

            migrationBuilder.DropColumn(
                name: "BloqueadoHasta",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "IntentosFallidos",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Version",
                newName: "Versiones");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "TI",
                newName: "TIs");

            migrationBuilder.RenameTable(
                name: "Recurso",
                newName: "Recursos");

            migrationBuilder.RenameTable(
                name: "Notificacion",
                newName: "Notificaciones");

            migrationBuilder.RenameTable(
                name: "Metrica",
                newName: "Metricas");

            migrationBuilder.RenameTable(
                name: "Metadato",
                newName: "Metadatos");

            migrationBuilder.RenameTable(
                name: "Gestor",
                newName: "Gestores");

            migrationBuilder.RenameTable(
                name: "Estudiante",
                newName: "Estudiantes");

            migrationBuilder.RenameTable(
                name: "Docente",
                newName: "Docentes");

            migrationBuilder.RenameTable(
                name: "Coordinacion",
                newName: "Coordinaciones");

            migrationBuilder.RenameIndex(
                name: "IX_Version_Id_Recurso",
                table: "Versiones",
                newName: "IX_Versiones_Id_Recurso");

            migrationBuilder.RenameColumn(
                name: "Contrasena",
                table: "Usuarios",
                newName: "Contraseña");

            migrationBuilder.RenameIndex(
                name: "IX_TI_Id_Usuario",
                table: "TIs",
                newName: "IX_TIs_Id_Usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Recurso_Id_Docente",
                table: "Recursos",
                newName: "IX_Recursos_Id_Docente");

            migrationBuilder.RenameIndex(
                name: "IX_Notificacion_Id_Usuario",
                table: "Notificaciones",
                newName: "IX_Notificaciones_Id_Usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Notificacion_Id_Recurso",
                table: "Notificaciones",
                newName: "IX_Notificaciones_Id_Recurso");

            migrationBuilder.RenameIndex(
                name: "IX_Metrica_Id_Recurso",
                table: "Metricas",
                newName: "IX_Metricas_Id_Recurso");

            migrationBuilder.RenameIndex(
                name: "IX_Metadato_Id_Recurso",
                table: "Metadatos",
                newName: "IX_Metadatos_Id_Recurso");

            migrationBuilder.RenameIndex(
                name: "IX_Gestor_Id_Usuario",
                table: "Gestores",
                newName: "IX_Gestores_Id_Usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Estudiante_Id_Usuario",
                table: "Estudiantes",
                newName: "IX_Estudiantes_Id_Usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Docente_Id_Usuario",
                table: "Docentes",
                newName: "IX_Docentes_Id_Usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Coordinacion_Id_Usuario",
                table: "Coordinaciones",
                newName: "IX_Coordinaciones_Id_Usuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Versiones",
                table: "Versiones",
                column: "Id_Version");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id_Usuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TIs",
                table: "TIs",
                column: "Id_TI");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recursos",
                table: "Recursos",
                column: "Id_Recurso");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notificaciones",
                table: "Notificaciones",
                column: "Id_Notificacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metricas",
                table: "Metricas",
                column: "Id_Metrica");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metadatos",
                table: "Metadatos",
                column: "Id_Metadato");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gestores",
                table: "Gestores",
                column: "Id_Gestor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estudiantes",
                table: "Estudiantes",
                column: "Id_Estudiante");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Docentes",
                table: "Docentes",
                column: "Id_Docente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coordinaciones",
                table: "Coordinaciones",
                column: "Id_Coordinacion");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinaciones_Usuarios_Id_Usuario",
                table: "Coordinaciones",
                column: "Id_Usuario",
                principalTable: "Usuarios",
                principalColumn: "Id_Usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Docentes_Usuarios_Id_Usuario",
                table: "Docentes",
                column: "Id_Usuario",
                principalTable: "Usuarios",
                principalColumn: "Id_Usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Usuarios_Id_Usuario",
                table: "Estudiantes",
                column: "Id_Usuario",
                principalTable: "Usuarios",
                principalColumn: "Id_Usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gestores_Usuarios_Id_Usuario",
                table: "Gestores",
                column: "Id_Usuario",
                principalTable: "Usuarios",
                principalColumn: "Id_Usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metadatos_Recursos_Id_Recurso",
                table: "Metadatos",
                column: "Id_Recurso",
                principalTable: "Recursos",
                principalColumn: "Id_Recurso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metricas_Recursos_Id_Recurso",
                table: "Metricas",
                column: "Id_Recurso",
                principalTable: "Recursos",
                principalColumn: "Id_Recurso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Recursos_Id_Recurso",
                table: "Notificaciones",
                column: "Id_Recurso",
                principalTable: "Recursos",
                principalColumn: "Id_Recurso");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Usuarios_Id_Usuario",
                table: "Notificaciones",
                column: "Id_Usuario",
                principalTable: "Usuarios",
                principalColumn: "Id_Usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recursos_Docentes_Id_Docente",
                table: "Recursos",
                column: "Id_Docente",
                principalTable: "Docentes",
                principalColumn: "Id_Docente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TIs_Usuarios_Id_Usuario",
                table: "TIs",
                column: "Id_Usuario",
                principalTable: "Usuarios",
                principalColumn: "Id_Usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Validacion_Gestores_Id_Gestor",
                table: "Validacion",
                column: "Id_Gestor",
                principalTable: "Gestores",
                principalColumn: "Id_Gestor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Validacion_Recursos_Id_Recurso",
                table: "Validacion",
                column: "Id_Recurso",
                principalTable: "Recursos",
                principalColumn: "Id_Recurso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Versiones_Recursos_Id_Recurso",
                table: "Versiones",
                column: "Id_Recurso",
                principalTable: "Recursos",
                principalColumn: "Id_Recurso",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
