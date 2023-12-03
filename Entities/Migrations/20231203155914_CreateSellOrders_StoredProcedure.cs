using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class CreateSellOrders_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			string sp_InsertSellOrder = @"CREATE PROCEDURE [dbo].[InsertSellOrder]
			(@SellOrderID uniqueidentifier, @StockSymbol nvarchar(10), @StockName nvarchar(100), @DateAndTimeOfOrder datetime, 
			@Quantity bigint, @Price float)
            AS BEGIN
                INSERT INTO [dbo].[SellOrders](SellOrderID, StockSymbol, StockName, DateAndTimeOfOrder, Quantity, Price)
				VALUES (@SellOrderID, @StockSymbol, @StockName, @DateAndTimeOfOrder, @Quantity, @Price)
            END
            ";

			migrationBuilder.Sql(sp_InsertSellOrder);
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			string sp_InsertPerson = @"DROP PROCEDURE [dbo].[InsertSellOrder]";

			migrationBuilder.Sql(sp_InsertPerson);
		}
    }
}
