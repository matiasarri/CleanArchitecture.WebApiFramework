using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.WebApiFramework.Infrastructure.Persistence.Migrations
{
    public partial class AddNombreConstraintClaveUnicaRegistroSistema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_ClavesRegistroSistema_Clave1_Clave2_Clave3_Clave4_Clave5",
                table: "ClavesRegistroSistema",
                newName: "IX_CLAVE_UNICA_REGISTRO_SISTEMA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_CLAVE_UNICA_REGISTRO_SISTEMA",
                table: "ClavesRegistroSistema",
                newName: "IX_ClavesRegistroSistema_Clave1_Clave2_Clave3_Clave4_Clave5");
        }
    }
}
