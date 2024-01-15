using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
	public class StockService : IStockService
	{
		private readonly IStocksRepository _repository;
		private readonly ILogger<StockService> _logger;

		public StockService(IStocksRepository repository, ILogger<StockService> logger) 
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? request)
		{
			_logger.LogInformation("'CreatBuyOrder' of StockService");

			if (request == null) 
			{
				_logger.LogWarning("Buy Order Request is null!");

				throw new ArgumentNullException(nameof(request));
			}

			ValidationHelper.ModelValidation(request);

			var buyOrder = await _repository.CreateBuyOrder(request.ToBuyOrder());

			return buyOrder.ToBuyOrderResponse();
		}

		public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
		{
			_logger.LogInformation("'CreateSellOrder' of StockService");

			if (sellOrderRequest == null)
			{
				_logger.LogWarning("Sell Order Request is null!");

				throw new ArgumentNullException(nameof(sellOrderRequest));
			}

			ValidationHelper.ModelValidation(sellOrderRequest);

			var sellOrder = await _repository.CreateSellOrder(sellOrderRequest.ToSellOrder());

			return sellOrder.ToSellOrderResponse();
		}

		public async Task<List<BuyOrderResponse>> GetBuyOrders()
		{
			_logger.LogInformation("'GetBuyOrders' of StockService");

			var buyOrders = await _repository.GetBuyOrders();
			return buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
		}

		public async Task<List<SellOrderResponse>> GetSellOrders()
		{
			_logger.LogInformation("'GetSellOrders' of StockService");

			var sellOrders = await _repository.GetSellOrders();
			return sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
		}
	}
}
