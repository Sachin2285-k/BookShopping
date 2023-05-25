using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecomm_Project_1.DataAccess.Migrations
{
    public partial class CoverTypeNameTestAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoverTypess",
                table: "CoverTypess");

            migrationBuilder.RenameTable(
                name: "CoverTypess",
                newName: "CoverTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoverTypes",
                table: "CoverTypes",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CoverTypes",
                table: "CoverTypes");

            migrationBuilder.RenameTable(
                name: "CoverTypes",
                newName: "CoverTypess");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoverTypess",
                table: "CoverTypess",
                column: "Id");
        }
    }
}
