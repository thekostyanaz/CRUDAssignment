using CRUDAssignment.Models;
using CRUDAssignment.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using Services;

namespace CRUDAssignment.ViewComponents
{
	public class SelectedStockViewComponent : ViewComponent
	{
		private readonly IFinnhubService _finnhubService;
		private readonly IStockService _stockService;
		private readonly TradingOptions _tradingOptions;

		public SelectedStockViewComponent(IFinnhubService finnhubService, IStockService stockService, IOptions<TradingOptions> tradingOptions) 
		{
			_finnhubService = finnhubService;
			_stockService = stockService;
			_tradingOptions = tradingOptions.Value;
		}

		public async Task<IViewComponentResult> InvokeAsync(string stock)
		{
			var companyProfile = await _finnhubService.GetCompanyProfile(stock);
			var stockPriceQuote = await _finnhubService.GetStockPriceQuote(stock);
			ViewBag.Price = stockPriceQuote["o"];
			return View(companyProfile);
		}
	}
}
