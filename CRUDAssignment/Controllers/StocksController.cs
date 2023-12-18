﻿using CRUDAssignment.Models;
using CRUDAssignment.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;

namespace CRUDAssignment.Controllers
{
	[Route("Stocks")]
	public class StocksController : Controller
	{
		private readonly IFinnhubService _finnhubService;
		private readonly TradingOptions _tradingOptions;

		public StocksController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions) 
		{
			_finnhubService = finnhubService;
			_tradingOptions = tradingOptions.Value;
		}

		[Route("Explore")]
		[Route("Explore/{stock}")]
		public async Task<IActionResult> Explore(string stock)
		{
			var popularStocksList = _tradingOptions.Top25PopularStocks?.Split(",");
			var stocksToDisplay = (await _finnhubService.GetStocks())?
				.Where(s => popularStocksList.Contains(s["symbol"])).ToList();
			var stocks = stocksToDisplay?.Select(s =>
				new Stock { StockName = s["description"], StockSymbol = s["symbol"] }).ToList();
			ViewBag.stock = stock;
			return View(stocks);
		}
	}
}