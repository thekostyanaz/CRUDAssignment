using System.ComponentModel.DataAnnotations;

namespace Entities
{
	public class SellOrder
	{
		[Key]
		public Guid SellOrderID { get; set; }

		public string? StockSymbol { get; set; }

		public string? StockName { get; set; }

		public DateTime DateAndTimeOfOrder { get; set; }

		public int Quantity { get; set; }

		public double Price { get; set; }
	}
}
