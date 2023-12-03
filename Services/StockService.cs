using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
	public class StockService : IStockService
	{
		private readonly StockMarketDbContext _db;

		public StockService(StockMarketDbContext stockMarketDbContext) 
		{
			_db = stockMarketDbContext;
		}

		public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? request)
		{
			if (request == null) 
			{
				throw new ArgumentNullException(nameof(request));
			}

			ValidationHelper.ModelValidation(request);

			var buyOrder = request.ToBuyOrder();

			await _db.sp_InsertBuyOrderAsync(buyOrder);

			return buyOrder.ToBuyOrderResponse();
		}

		public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
		{
			if (sellOrderRequest == null)
			{
				throw new ArgumentNullException(nameof(sellOrderRequest));
			}

			ValidationHelper.ModelValidation(sellOrderRequest);

			var sellOrder = sellOrderRequest.ToSellOrder();

			await _db.sp_InsertSellOrderAsync(sellOrder);

			return sellOrder.ToSellOrderResponse();
		}

		public List<BuyOrderResponse> GetBuyOrders()
		{
			return _db.sp_GetBuyOrders().Select(temp => temp.ToBuyOrderResponse()).ToList();
		}

		public List<SellOrderResponse> GetSellOrders()
		{
			return _db.sp_GetSellOrders().Select(temp => temp.ToSellOrderResponse()).ToList();
		}
	}
}
