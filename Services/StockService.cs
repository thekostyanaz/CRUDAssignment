using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
	public class StockService : IStockService
	{
		private readonly IStocksRepository _repository;

		public StockService(IStocksRepository repository) 
		{
			_repository = repository;
		}

		public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? request)
		{
			if (request == null) 
			{
				throw new ArgumentNullException(nameof(request));
			}

			ValidationHelper.ModelValidation(request);

			var buyOrder = await _repository.CreateBuyOrder(request.ToBuyOrder());

			return buyOrder.ToBuyOrderResponse();
		}

		public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
		{
			if (sellOrderRequest == null)
			{
				throw new ArgumentNullException(nameof(sellOrderRequest));
			}

			ValidationHelper.ModelValidation(sellOrderRequest);

			var sellOrder = await _repository.CreateSellOrder(sellOrderRequest.ToSellOrder());

			return sellOrder.ToSellOrderResponse();
		}

		public async Task<List<BuyOrderResponse>> GetBuyOrders()
		{
			var buyOrders = await _repository.GetBuyOrders();
			return buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
		}

		public async Task<List<SellOrderResponse>> GetSellOrders()
		{
			var sellOrders = await _repository.GetSellOrders();
			return sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
		}
	}
}
