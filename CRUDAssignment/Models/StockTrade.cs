using System.ComponentModel.DataAnnotations;

namespace CRUDAssignment.Models
{
	public class StockTrade
	{
		public string? StockSymbol { get; set; }

		public string? StockName { get; set; }

		public double Price { get; set; }

		[Range(1, 100000, ErrorMessage = "Quantity value should be between 1 and 100000")]
		public uint Quantity { get; set; }
	}
}
