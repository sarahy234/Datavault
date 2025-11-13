using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id_Usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id_Usuario);
                });

            migrationBuilder.CreateTable(
                name: "Coordinaciones",
                columns: table => new
                {
                    Id_Coordinacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinaciones", x => x.Id_Coordinacion);
                    table.ForeignKey(
                        name: "FK_Coordinaciones_Usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Docentes",
                columns: table => new
                {
                    Id_Docente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docentes", x => x.Id_Docente);
                    table.ForeignKey(
                        name: "FK_Docentes_Usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    Id_Estudiante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Semestre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Carrera = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.Id_Estudiante);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gestores",
                columns: table => new
                {
                    Id_Gestor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Area_Responsable = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestores", x => x.Id_Gestor);
                    table.ForeignKey(
                        name: "FK_Gestores_Usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIs",
                columns: table => new
                {
                    Id_TI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Especialidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIs", x => x.Id_TI);
                    table.ForeignKey(
                        name: "FK_TIs_Usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recursos",
                columns: table => new
                {
                    Id_Recurso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Semestre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Tamano = table.Column<int>(type: "int", nullable: true),
                    Fecha_Subida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id_Docente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recursos", x => x.Id_Recurso);
                    table.ForeignKey(
                        name: "FK_Recursos_Docentes_Id_Docente",
                        column: x => x.Id_Docente,
                        principalTable: "Docentes",
                        principalColumn: "Id_Docente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Metadatos",
                columns: table => new
                {
                    Id_Metadato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Materia = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Palabras_Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tipo_Recurso = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id_Recurso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadatos", x => x.Id_Metadato);
                    table.ForeignKey(
                        name: "FK_Metadatos_Recursos_Id_Recurso",
                        column: x => x.Id_Recurso,
                        principalTable: "Recursos",
                        principalColumn: "Id_Recurso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Metricas",
                columns: table => new
                {
                    Id_Metrica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo_Evento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fecha_Evento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_Recurso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metricas", x => x.Id_Metrica);
                    table.ForeignKey(
                        name: "FK_Metricas_Recursos_Id_Recurso",
                        column: x => x.Id_Recurso,
                        principalTable: "Recursos",
                        principalColumn: "Id_Recurso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id_Notificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Fecha_Envio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Leido = table.Column<bool>(type: "bit", nullable: false),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false),
                    Id_Recurso = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id_Notificacion);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Recursos_Id_Recurso",
                        column: x => x.Id_Recurso,
                        principalTable: "Recursos",
                        principalColumn: "Id_Recurso");
                    table.ForeignKey(
                        name: "FK_Notificaciones_Usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Validacion",
                columns: table => new
                {
                    Id_Validacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resultado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_Recurso = table.Column<int>(type: "int", nullable: false),
                    Id_Gestor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validacion", x => x.Id_Validacion);
                    table.ForeignKey(
                        name: "FK_Validacion_Gestores_Id_Gestor",
                        column: x => x.Id_Gestor,
                        principalTable: "Gestores",
                        principalColumn: "Id_Gestor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Validacion_Recursos_Id_Recurso",
                        column: x => x.Id_Recurso,
                        principalTable: "Recursos",
                        principalColumn: "Id_Recurso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Versiones",
                columns: table => new
                {
                    Id_Version = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero_Version = table.Column<int>(type: "int", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_Recurso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versiones", x => x.Id_Version);
                    table.ForeignKey(
                        name: "FK_Versiones_Recursos_Id_Recurso",
                        column: x => x.Id_Recurso,
                        principalTable: "Recursos",
                        principalColumn: "Id_Recurso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coordinaciones_Id_Usuario",
                table: "Coordinaciones",
                column: "Id_Usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Docentes_Id_Usuario",
                table: "Docentes",
                column: "Id_Usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_Id_Usuario",
                table: "Estudiantes",
                column: "Id_Usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gestores_Id_Usuario",
                table: "Gestores",
                column: "Id_Usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Metadatos_Id_Recurso",
                table: "Metadatos",
                column: "Id_Recurso");

            migrationBuilder.CreateIndex(
                name: "IX_Metricas_Id_Recurso",
                table: "Metricas",
                column: "Id_Recurso");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_Id_Recurso",
                table: "Notificaciones",
                column: "Id_Recurso");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_Id_Usuario",
                table: "Notificaciones",
                column: "Id_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_Id_Docente",
                table: "Recursos",
                column: "Id_Docente");

            migrationBuilder.CreateIndex(
                name: "IX_TIs_Id_Usuario",
                table: "TIs",
                column: "Id_Usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Validacion_Id_Gestor",
                table: "Validacion",
                column: "Id_Gestor");

            migrationBuilder.CreateIndex(
                name: "IX_Validacion_Id_Recurso",
                table: "Validacion",
                column: "Id_Recurso");

            migrationBuilder.CreateIndex(
                name: "IX_Versiones_Id_Recurso",
                table: "Versiones",
                column: "Id_Recurso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coordinaciones");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Metadatos");

            migrationBuilder.DropTable(
                name: "Metricas");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "TIs");

            migrationBuilder.DropTable(
                name: "Validacion");

            migrationBuilder.DropTable(
                name: "Versiones");

            migrationBuilder.DropTable(
                name: "Gestores");

            migrationBuilder.DropTable(
                name: "Recursos");

            migrationBuilder.DropTable(
                name: "Docentes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
