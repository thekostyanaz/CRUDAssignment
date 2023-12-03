using Entities;

namespace ServiceContracts.DTO
{
	public class BuyOrderResponse : OrderResponse
	{
		public Guid BuyOrderID { get; set; }

		public override bool Equals(object? obj)
		{
			if (obj == null) return false; 
			
			if (obj.GetType() != typeof(BuyOrderResponse)) return false;

			BuyOrderResponse other = (BuyOrderResponse) obj;

			return BuyOrderID == other.BuyOrderID
				&& StockSymbol == other.StockSymbol
				&& StockName == other.StockName
				&& DateAndTimeOfOrder == other.DateAndTimeOfOrder
				&& Quantity == other.Quantity
				&& Price == other.Price;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}

	public static class BuyOrderExtensions 
	{
		public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder) 
		{
			var stockPrice = Math.Round(buyOrder.Price, 2);
			var tradeAmount = Math.Round((stockPrice * buyOrder.Quantity), 2);

			return new BuyOrderResponse
			{
				BuyOrderID = buyOrder.BuyOrderID,
				StockSymbol = buyOrder.StockSymbol,
				StockName = buyOrder.StockName,
				DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
				Quantity = buyOrder.Quantity,
				Price = stockPrice,
				TradeAmount = tradeAmount
			};
		}
	}
}
