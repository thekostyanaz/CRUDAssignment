using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class GetBuyOrders_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			string sp_GetBuyOrders = @"CREATE PROCEDURE [dbo].[GetBuyOrders]
            AS BEGIN
                Select BuyOrderID, StockSymbol, StockName, DateAndTimeOfOrder, Quantity, 
                Price FROM [dbo].[BuyOrders]
            END
            ";

			migrationBuilder.Sql(sp_GetBuyOrders);
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			string sp_GetAllPersons = @"DROP PROCEDURE [dbo].[GetBuyOrders]";

			migrationBuilder.Sql(sp_GetAllPersons);
		}
    }
}
