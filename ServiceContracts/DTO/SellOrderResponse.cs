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
		public static SellOrderResponse ToSellOrderResponse(this SellOrder buyOrder)
		{
			return new SellOrderResponse
			{
				SellOrderID = buyOrder.SellOrderID,
				StockSymbol = buyOrder.StockSymbol,
				StockName = buyOrder.StockName,
				DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
				Quantity = buyOrder.Quantity,
				Price = buyOrder.Price
			};
		}
	}
}
