using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace Entities
{
	public class StockMarketDbContext : DbContext
	{
		public StockMarketDbContext(DbContextOptions options) : base(options) 
		{

		}

		public DbSet<BuyOrder> BuyOrders { get; set; }

		public DbSet<SellOrder> SellOrders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<BuyOrder>().ToTable("BuyOrders");
			modelBuilder.Entity<SellOrder>().ToTable("SellOrders");
		}

		public async Task<int> sp_InsertBuyOrderAsync(BuyOrder buyOrder)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@BuyOrderID", buyOrder.BuyOrderID),
				new SqlParameter("@StockSymbol", buyOrder.StockSymbol),
				new SqlParameter("@StockName", buyOrder.StockName),
				new SqlParameter("@DateAndTimeOfOrder", buyOrder.DateAndTimeOfOrder),
				new SqlParameter("@Quantity", buyOrder.Quantity),
				new SqlParameter("@Price", buyOrder.Price)
			};

			return await Database.ExecuteSqlRawAsync("EXECUTE [dbo].[InsertBuyOrder] @BuyOrderID, @StockSymbol, @StockName, " +
				"@DateAndTimeOfOrder, @Quantity, @Price", parameters);
		}

		public async Task<int> sp_InsertSellOrderAsync(SellOrder sellOrder)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SellOrderID", sellOrder.SellOrderID),
				new SqlParameter("@StockSymbol", sellOrder.StockSymbol),
				new SqlParameter("@StockName", sellOrder.StockName),
				new SqlParameter("@DateAndTimeOfOrder", sellOrder.DateAndTimeOfOrder),
				new SqlParameter("@Quantity", sellOrder.Quantity),
				new SqlParameter("@Price", sellOrder.Price)
			};

			return await Database.ExecuteSqlRawAsync("EXECUTE [dbo].[InsertSellOrder] @SellOrderID, @StockSymbol, @StockName, " +
				"@DateAndTimeOfOrder, @Quantity, @Price", parameters);
		}

		public List<BuyOrder> sp_GetBuyOrders() 
		{
			return BuyOrders.FromSqlRaw("EXECUTE [dbo].[GetBuyOrders]").ToList();
		}

		public List<SellOrder> sp_GetSellOrders()
		{
			return SellOrders.FromSqlRaw("EXECUTE [dbo].[GetSellOrders]").ToList();
		}
	}
}
