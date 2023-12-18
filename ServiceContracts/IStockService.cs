using ServiceContracts.DTO;

namespace ServiceContracts
{
	public interface IStockService
	{
		Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? request);

		Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

		Task<List<BuyOrderResponse>> GetBuyOrders();

		Task<List<SellOrderResponse>> GetSellOrders();
	}
}
