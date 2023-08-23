using ServiceContracts.DTO;

namespace ServiceContracts
{
	public interface IStockService
	{
		BuyOrderResponse CreateBuyOrder(BuyOrderRequest? request);

		SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest);

		List<BuyOrderResponse> GetBuyOrders();

		List<SellOrderResponse> GetSellOrders();
	}
}
