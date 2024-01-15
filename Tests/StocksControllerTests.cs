using Castle.Core.Logging;
using CRUDAssignment.Controllers;
using CRUDAssignment.Models;
using CRUDAssignment.Options;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ServiceContracts;
using Xunit;

namespace Tests
{
	public class StocksControllerTests
	{
		private readonly IFinnhubService _finnhubService;
		private readonly ILogger<StocksController> _logger;
		private readonly Mock<IFinnhubService> _finnhubServiceMock;
		private readonly Mock<ILogger<StocksController>> _loggerMock;
		private readonly IOptions<TradingOptions> _tradingOptions;
		
		public StocksControllerTests() 
		{
			_finnhubServiceMock = new Mock<IFinnhubService>();
			_loggerMock = new Mock<ILogger<StocksController>>();
			_finnhubService = _finnhubServiceMock.Object;
			_logger = _loggerMock.Object;
			_tradingOptions = Options.Create(new TradingOptions() { Top25PopularStocks = "AAPL,MSFT,AMZN,TSLA,GOOGL,GOOG,NVDA,BRK.B,META,UNH,JNJ,JPM,V,PG,XOM,HD,CVX,MA,BAC,ABBV,PFE,AVGO,COST,DIS,KO" });
		}

		[Fact]
		public async Task Explore_ShouldReturnViewWithStockList() 
		{
			var stocksList = new List<Dictionary<string, string>> 
			{
				new Dictionary<string, string>() { { "symbol", "AAPL" }, { "description", "Apple"} },
				new Dictionary<string, string>() { { "symbol", "TSLA" }, { "description", "Tesla"} },
				new Dictionary<string, string>() { { "symbol", "GOOG" }, { "description", "Google"} },
			};

			var expectedStocks = stocksList.Select(s => new Stock() { StockName = s["description"], StockSymbol = s["symbol"] }).ToList();
			var stockController = new StocksController(_finnhubService, _tradingOptions, _logger);


			_finnhubServiceMock.Setup(temp => temp.GetStocks()).ReturnsAsync(stocksList);

			//Act
			var result = await stockController.Explore(null);

			//Assert
			var viewResult = Assert.IsType<ViewResult>(result);

			viewResult.ViewData.Model.Should().BeAssignableTo<List<Stock>>();
			viewResult.ViewData.Model.Should().BeEquivalentTo(expectedStocks);
		}
	}
}
