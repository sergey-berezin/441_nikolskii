using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerceptionComponent.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    blob = table.Column<byte[]>(type: "BLOB", nullable: true),
                    hash = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Rectangles",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    imageid = table.Column<int>(type: "INTEGER", nullable: true),
                    x = table.Column<double>(type: "REAL", nullable: false),
                    y = table.Column<double>(type: "REAL", nullable: false),
                    height = table.Column<double>(type: "REAL", nullable: false),
                    width = table.Column<double>(type: "REAL", nullable: false),
                    Cls = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rectangles", x => x.id);
                    table.ForeignKey(
                        name: "FK_Rectangles_Images_imageid",
                        column: x => x.imageid,
                        principalTable: "Images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rectangles_imageid",
                table: "Rectangles",
                column: "imageid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rectangles");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
