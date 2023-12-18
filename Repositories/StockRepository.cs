using Entities;
using RepositoryContracts;

namespace Repositories
{
	public class StockRepository : IStocksRepository
	{
		private readonly StockMarketDbContext _context;

		public StockRepository(StockMarketDbContext context) 
		{
			_context = context;
		}

		public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
		{
			await _context.sp_InsertBuyOrderAsync(buyOrder);

			return buyOrder;
		}

		public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
		{
			await _context.sp_InsertSellOrderAsync(sellOrder);

			return sellOrder;
		}

		public async Task<List<BuyOrder>> GetBuyOrders()
		{
			return _context.sp_GetBuyOrders().ToList();
		}

		public async Task<List<SellOrder>> GetSellOrders()
		{
			return _context.sp_GetSellOrders().ToList();
		}
	}
}