using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecomm_Project_1.DataAccess.Migrations
{
    public partial class OrdersAccordingToDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GetDataAsPerDate
	                            @start datetime,
                                @end datetime
                                AS
	                            select * from OrherHeader where OrderDate begween @start and @end");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
