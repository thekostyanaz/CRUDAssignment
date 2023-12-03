using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
	/// <inheritdoc />
	public partial class CreateBuyOrders_StoredProcedure : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			string sp_InsertBuyOrder = @"CREATE PROCEDURE [dbo].[InsertBuyOrder]
			(@BuyOrderID uniqueidentifier, @StockSymbol nvarchar(10), @StockName nvarchar(100), @DateAndTimeOfOrder datetime, 
			@Quantity bigint, @Price float)
            AS BEGIN
                INSERT INTO [dbo].[BuyOrders](BuyOrderID, StockSymbol, StockName, DateAndTimeOfOrder, Quantity, Price)
				VALUES (@BuyOrderID, @StockSymbol, @StockName, @DateAndTimeOfOrder, @Quantity, @Price)
            END
            ";

			migrationBuilder.Sql(sp_InsertBuyOrder);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			string sp_InsertPerson = @"DROP PROCEDURE [dbo].[InsertBuyOrder]";

			migrationBuilder.Sql(sp_InsertPerson);
		}
	}
}
