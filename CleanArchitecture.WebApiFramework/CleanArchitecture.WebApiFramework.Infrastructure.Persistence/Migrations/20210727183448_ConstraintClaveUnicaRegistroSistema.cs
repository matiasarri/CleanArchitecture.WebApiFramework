using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.WebApiFramework.Infrastructure.Persistence.Migrations
{
    public partial class ConstraintClaveUnicaRegistroSistema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Clave5",
                table: "ClavesRegistroSistema",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Clave4",
                table: "ClavesRegistroSistema",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Clave3",
                table: "ClavesRegistroSistema",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Clave2",
                table: "ClavesRegistroSistema",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Clave1",
                table: "ClavesRegistroSistema",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClavesRegistroSistema_Clave1_Clave2_Clave3_Clave4_Clave5",
                table: "ClavesRegistroSistema",
                columns: new[] { "Clave1", "Clave2", "Clave3", "Clave4", "Clave5" },
                unique: true,
                filter: "[Clave1] IS NOT NULL AND [Clave2] IS NOT NULL AND [Clave3] IS NOT NULL AND [Clave4] IS NOT NULL AND [Clave5] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClavesRegistroSistema_Clave1_Clave2_Clave3_Clave4_Clave5",
                table: "ClavesRegistroSistema");

            migrationBuilder.AlterColumn<string>(
                name: "Clave5",
                table: "ClavesRegistroSistema",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Clave4",
                table: "ClavesRegistroSistema",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Clave3",
                table: "ClavesRegistroSistema",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Clave2",
                table: "ClavesRegistroSistema",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Clave1",
                table: "ClavesRegistroSistema",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
