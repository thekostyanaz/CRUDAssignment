using Microsoft.Extensions.Configuration;
using ServiceContracts;

namespace Services
{
	public class FinnhubService : IFinnhubService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;

		public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
		{
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
		}

		public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
		{
			using (var httpClient = _httpClientFactory.CreateClient())
			{
				var uri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}");
				HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);
				var parsedResponse = await responseMessage.Content.ReadAsAsync<Dictionary<string, object>>();
				return parsedResponse;
			}
		}

		public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
		{
			using (var httpClient = _httpClientFactory.CreateClient())
			{
				var uri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}");
				HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);
				var parsedResponse = await responseMessage.Content.ReadAsAsync<Dictionary<string, object>>();
				return parsedResponse;
			}
		}
	}
}