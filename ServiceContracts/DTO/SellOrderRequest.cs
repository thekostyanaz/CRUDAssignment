using Entities;
using ServiceContracts.DTO.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	public class SellOrderRequest : OrderRequest
	{
		public SellOrder ToSellOrder()
		{
			return new SellOrder()
			{
				SellOrderID = Guid.NewGuid(),
				StockSymbol = StockSymbol,
				StockName = StockName,
				DateAndTimeOfOrder = DateAndTimeOfOrder,
				Quantity = Quantity,
				Price = Price
			};
		}
	}
}
