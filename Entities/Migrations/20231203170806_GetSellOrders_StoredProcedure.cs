using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class GetSellOrders_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			string sp_GetBuyOrders = @"CREATE PROCEDURE [dbo].[GetSellOrders]
            AS BEGIN
                Select SellOrderID, StockSymbol, StockName, DateAndTimeOfOrder, Quantity, 
                Price FROM [dbo].[SellOrders]
            END
            ";

			migrationBuilder.Sql(sp_GetBuyOrders);
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			string sp_GetAllPersons = @"DROP PROCEDURE [dbo].[GetSellOrders]";

			migrationBuilder.Sql(sp_GetAllPersons);
		}
    }
}
