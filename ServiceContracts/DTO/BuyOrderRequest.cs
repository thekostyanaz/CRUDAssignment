using Entities;

namespace ServiceContracts.DTO
{
	public class BuyOrderRequest : OrderRequest
	{
		public BuyOrder ToBuyOrder() 
		{
			return new BuyOrder()
			{
				BuyOrderID = Guid.NewGuid(),
				StockSymbol = StockSymbol,
				StockName = StockName,
				DateAndTimeOfOrder = DateAndTimeOfOrder,
				Quantity = Quantity,
				Price = Price
			};
		}
	}
}
