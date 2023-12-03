using Entities;

namespace ServiceContracts.DTO
{
	public class SellOrderResponse : OrderResponse
	{
		public Guid SellOrderID { get; set; }

		public override bool Equals(object? obj)
		{
			if (obj == null) return false;

			if (obj.GetType() != typeof(SellOrderResponse)) return false;

			SellOrderResponse other = (SellOrderResponse)obj;

			return SellOrderID == other.SellOrderID
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

	public static class SellOrderExtensions
	{
		public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
		{
			var stockPrice = Math.Round(sellOrder.Price, 2);
			var tradeAmount = Math.Round((stockPrice * sellOrder.Quantity), 2);

			return new SellOrderResponse
			{
				SellOrderID = sellOrder.SellOrderID,
				StockSymbol = sellOrder.StockSymbol,
				StockName = sellOrder.StockName,
				DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
				Quantity = sellOrder.Quantity,
				Price = stockPrice,
				TradeAmount = tradeAmount
			};
		}
	}
}
