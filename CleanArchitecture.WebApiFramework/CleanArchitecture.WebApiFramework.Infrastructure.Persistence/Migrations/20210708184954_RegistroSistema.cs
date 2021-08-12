using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.WebApiFramework.Infrastructure.Persistence.Migrations
{
    public partial class RegistroSistema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClavesRegistroSistema",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    Clave1 = table.Column<string>(nullable: true),
                    Clave2 = table.Column<string>(nullable: true),
                    Clave3 = table.Column<string>(nullable: true),
                    Clave4 = table.Column<string>(nullable: true),
                    Clave5 = table.Column<string>(nullable: true),
                    Valor = table.Column<string>(nullable: true),
                    Comentario = table.Column<string>(nullable: true),
                    ValoresPosibles = table.Column<string>(nullable: true),
                    ValorPredeterminado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClavesRegistroSistema", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClavesRegistroSistema");
        }
    }
}
