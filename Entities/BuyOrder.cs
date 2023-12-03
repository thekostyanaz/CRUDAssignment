﻿using System.ComponentModel.DataAnnotations;

namespace Entities
{
	public class BuyOrder
	{
		[Key]
		public Guid BuyOrderID { get; set; }

		public string? StockSymbol { get; set; }

		public string? StockName { get; set; }

		public DateTime DateAndTimeOfOrder { get; set; }

		public int Quantity { get; set; }

		public double Price { get; set; }
	}
}