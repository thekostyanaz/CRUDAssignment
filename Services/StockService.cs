using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
	public class StockService : IStockService
	{
		private readonly List<BuyOrder> _buyOrders;
		private readonly List<SellOrder> _sellOrders;

		public StockService() 
		{
			_buyOrders = new List<BuyOrder>();
			_sellOrders = new List<SellOrder>();
		}

		public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? request)
		{
			if (request == null) 
			{
				throw new ArgumentNullException(nameof(request));
			}

			ValidationHelper.ModelValidation(request);

			var buyOrder = request.ToBuyOrder();

			_buyOrders.Add(buyOrder);

			return buyOrder.ToBuyOrderResponse();
		}

		public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
		{
			if (sellOrderRequest == null)
			{
				throw new ArgumentNullException(nameof(sellOrderRequest));
			}

			ValidationHelper.ModelValidation(sellOrderRequest);

			var sellOrder = sellOrderRequest.ToSellOrder();

			_sellOrders.Add(sellOrder);

			return sellOrder.ToSellOrderResponse();
		}

		public List<BuyOrderResponse> GetBuyOrders()
		{
			return _buyOrders.Select(o => o.ToBuyOrderResponse()).ToList();
		}

		public List<SellOrderResponse> GetSellOrders()
		{
			return _sellOrders.Select(o => o.ToSellOrderResponse()).ToList();
		}
	}
}
