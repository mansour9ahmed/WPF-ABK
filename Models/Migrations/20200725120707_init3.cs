using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoaElements_Soas_SoaId",
                table: "SoaElements");

            migrationBuilder.AlterColumn<int>(
                name: "SoaId",
                table: "SoaElements",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SoaElements_Soas_SoaId",
                table: "SoaElements",
                column: "SoaId",
                principalTable: "Soas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoaElements_Soas_SoaId",
                table: "SoaElements");

            migrationBuilder.AlterColumn<int>(
                name: "SoaId",
                table: "SoaElements",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SoaElements_Soas_SoaId",
                table: "SoaElements",
                column: "SoaId",
                principalTable: "Soas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
