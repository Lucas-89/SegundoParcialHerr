using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SegundoParcialHerr.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Nacionalidad = table.Column<string>(type: "TEXT", nullable: false),
                    Contemporaneo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sucursal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreSucursal = table.Column<string>(type: "TEXT", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", nullable: false),
                    Localidad = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Libro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    Genero = table.Column<int>(type: "INTEGER", nullable: false),
                    Precio = table.Column<int>(type: "INTEGER", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    AutorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Libro_Autor_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Autor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibroSucursal",
                columns: table => new
                {
                    LibrosId = table.Column<int>(type: "INTEGER", nullable: false),
                    SucursalesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibroSucursal", x => new { x.LibrosId, x.SucursalesId });
                    table.ForeignKey(
                        name: "FK_LibroSucursal_Libro_LibrosId",
                        column: x => x.LibrosId,
                        principalTable: "Libro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibroSucursal_Sucursal_SucursalesId",
                        column: x => x.SucursalesId,
                        principalTable: "Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libro_AutorId",
                table: "Libro",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_LibroSucursal_SucursalesId",
                table: "LibroSucursal",
                column: "SucursalesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibroSucursal");

            migrationBuilder.DropTable(
                name: "Libro");

            migrationBuilder.DropTable(
                name: "Sucursal");

            migrationBuilder.DropTable(
                name: "Autor");
        }
    }
}
