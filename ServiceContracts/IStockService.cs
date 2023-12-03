using ServiceContracts.DTO;

namespace ServiceContracts
{
	public interface IStockService
	{
		Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? request);

		Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

		List<BuyOrderResponse> GetBuyOrders();

		List<SellOrderResponse> GetSellOrders();
	}
}
