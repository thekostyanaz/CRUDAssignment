﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using CRUDAssignment.Models;
using CRUDAssignment.Options;
using ServiceContracts.DTO;
using Rotativa.AspNetCore;

namespace CRUDAssignment.Controllers
{
	[Route("Trade")]
	public class TradeController : Controller
	{
		private readonly IFinnhubService _finnhubService;
		private readonly IStockService _stockService;
		private readonly TradingOptions _options;
		private readonly IConfiguration _configuration;
		private readonly string _defaultStockSymbol;
		private readonly ILogger<TradeController> _logger;

		public TradeController(IFinnhubService finnhubService, IStockService stockService, IOptions<TradingOptions> tradingOptions, IConfiguration configuration, ILogger<TradeController> logger)
		{
			_finnhubService = finnhubService;
			_stockService = stockService;
			_options = tradingOptions.Value;
			_configuration = configuration;
			_defaultStockSymbol = _options.DefaultStockSymbol ?? "MSFT";
			_logger = logger;
		}

		[Route("/")]
		[Route("Index")]
		public async Task<IActionResult> Index()
		{
			_logger.LogInformation("Index action method of TradeController");

			var priceQuoteResponse = await _finnhubService.GetStockPriceQuote(_defaultStockSymbol);
			var profileResponse = await _finnhubService.GetCompanyProfile(_defaultStockSymbol);

			var stockTrade = new StockTrade()
			{
				StockSymbol = profileResponse?["ticker"].ToString(),
				StockName = profileResponse?["name"].ToString(),
				Price = Convert.ToDouble(priceQuoteResponse?["c"].ToString())
			};

			ViewBag.FinnhubToken = _configuration?["FinnhubToken"]?.ToString();
			return View(stockTrade);
		}

		[Route("Orders")]
		public async Task<IActionResult> Orders() 
		{
			_logger.LogInformation("Orders action method of TradeController");

			var buyOrders = await _stockService.GetBuyOrders();
			var sellOrders = await _stockService.GetSellOrders();
			var orders = new Orders()
			{
				BuyOrders = buyOrders,
				SellOrders = sellOrders
			}; 
			return View(orders);
		}

		[Route("BuyOrder")]

		public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest) 
		{
			_logger.LogInformation("BuyOrder action method of TradeController");

			buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;
			ModelState.Clear();

			if (!ModelState.IsValid) 
			{
				var stockTrade = new StockTrade { StockSymbol = buyOrderRequest.StockSymbol, StockName = buyOrderRequest.StockName, Price = buyOrderRequest.Price, Quantity = buyOrderRequest.Quantity };
				return View("Index", stockTrade);
			}

			var priceQuoteResponse = await _finnhubService.GetStockPriceQuote(_defaultStockSymbol);
			var profileResponse = await _finnhubService.GetCompanyProfile(_defaultStockSymbol);
			buyOrderRequest.StockSymbol = profileResponse?["ticker"].ToString();
			buyOrderRequest.StockName = profileResponse?["name"].ToString();
			buyOrderRequest.Price = Convert.ToDouble(priceQuoteResponse?["c"].ToString());
			buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;
			
			await _stockService.CreateBuyOrder(buyOrderRequest);
			
			return RedirectToAction("Index");
		}

		[Route("SellOrder")]
		public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
		{
			_logger.LogInformation("SellOrder action method of TradeController");

			sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;
			ModelState.Clear();

			if (!ModelState.IsValid)
			{
				var stockTrade = new StockTrade { StockSymbol = sellOrderRequest.StockSymbol, StockName = sellOrderRequest.StockName, Price = sellOrderRequest.Price, Quantity = sellOrderRequest.Quantity };
				return View("Index", stockTrade);
			}

			var priceQuoteResponse = await _finnhubService.GetStockPriceQuote(_defaultStockSymbol);
			var profileResponse = await _finnhubService.GetCompanyProfile(_defaultStockSymbol);

			sellOrderRequest.StockSymbol = profileResponse?["ticker"].ToString();
			sellOrderRequest.StockName = profileResponse?["name"].ToString();
			sellOrderRequest.Price = Convert.ToDouble(priceQuoteResponse?["c"].ToString());
			sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;
			
			await _stockService.CreateSellOrder(sellOrderRequest);
			
			return RedirectToAction("Index");
		}

		[Route("OrdersPDF")]
		public async Task<IActionResult> TradeOrdersPdf() 
		{
			_logger.LogInformation("TradeOrdersPdf action method of TradeController");

			var orders = new Orders()
			{
				BuyOrders = await _stockService.GetBuyOrders(),
				SellOrders = await _stockService.GetSellOrders()
			};

			
			return new ViewAsPdf("OrdersPDF", orders, ViewData) 
			{
				PageMargins = new Rotativa.AspNetCore.Options.Margins() 
				{
					Top = 20, Right = 20, Bottom = 20, Left = 20
				},
				PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
			};
		}
	}
}
