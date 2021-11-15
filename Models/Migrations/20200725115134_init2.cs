using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "SoaElements",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "SoaElements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
