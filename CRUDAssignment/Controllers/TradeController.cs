using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using CRUDAssignment.Models;
using CRUDAssignment.Options;
using ServiceContracts.DTO;

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

		public TradeController(IFinnhubService finnhubService, IStockService stockService, IOptions<TradingOptions> tradingOptions, IConfiguration configuration)
		{
			_finnhubService = finnhubService;
			_stockService = stockService;
			_options = tradingOptions.Value;
			_configuration = configuration;
			_defaultStockSymbol = _options.DefaultStockSymbol ?? "MSFT";
		}

		[Route("/")]
		[Route("Index")]
		public async Task<IActionResult> Index()
		{
			var priceQuoteResponse = await _finnhubService.GetStockPriceQuote(_defaultStockSymbol);
			var profileResponse = await _finnhubService.GetCompanyProfile(_defaultStockSymbol);

			var stockTrade = new StockTrade()
			{
				StockSymbol = profileResponse["ticker"].ToString(),
				StockName = profileResponse["name"].ToString(),
				Price = Convert.ToDouble(priceQuoteResponse["c"].ToString())
			};

			ViewBag.FinnhubToken = _configuration["FinnhubToken"].ToString();
			return View(stockTrade);
		}

		[Route("Orders")]
		public async Task<IActionResult> Orders() 
		{
			var buyOrders =_stockService.GetBuyOrders();
			var sellOrders = _stockService.GetSellOrders();
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
			var priceQuoteResponse = await _finnhubService.GetStockPriceQuote(_defaultStockSymbol);
			var profileResponse = await _finnhubService.GetCompanyProfile(_defaultStockSymbol);

			buyOrderRequest.StockSymbol = profileResponse?["ticker"].ToString();
			buyOrderRequest.StockName = profileResponse?["name"].ToString();
			buyOrderRequest.Price = Convert.ToDouble(priceQuoteResponse?["c"].ToString());
			buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;
			_stockService.CreateBuyOrder(buyOrderRequest);
			return RedirectToAction("Index", "Trade");
		}

		[Route("SellOrder")]
		public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
		{
			var priceQuoteResponse = await _finnhubService.GetStockPriceQuote(_defaultStockSymbol);
			var profileResponse = await _finnhubService.GetCompanyProfile(_defaultStockSymbol);

			sellOrderRequest.StockSymbol = profileResponse?["ticker"].ToString();
			sellOrderRequest.StockName = profileResponse?["name"].ToString();
			sellOrderRequest.Price = Convert.ToDouble(priceQuoteResponse?["c"].ToString());
			sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;
			_stockService.CreateSellOrder(sellOrderRequest);
			return RedirectToAction("Index", "Trade");
		}
	}
}
