﻿using ServiceContracts.DTO.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	public class OrderRequest
	{
		[Required(ErrorMessage = "Stock symbol cannot be blank")]
		public string? StockSymbol { get; set; }

		[Required(ErrorMessage = "Stock name cannot be blank")]
		public string? StockName { get; set; }

		[MinimumDateValidation("2000-01-01")]
		public DateTime DateAndTimeOfOrder { get; set; }

		[Range(1, 100000, ErrorMessage = "Quantity value should be between 1 and 100000")]
		public int Quantity { get; set; }

		[Range(1, 1000, ErrorMessage = "Price value should be between 1 and 10000")]
		public double Price { get; set; }
	}
}