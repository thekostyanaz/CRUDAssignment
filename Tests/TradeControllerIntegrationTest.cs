using Fizzler.Systems.HtmlAgilityPack;
using FluentAssertions;
using HtmlAgilityPack;
using Xunit;

namespace Tests
{
	public class TradeControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _httpClient;

		public TradeControllerIntegrationTest(CustomWebApplicationFactory factory) 
		{
			_httpClient = factory.CreateClient();
		}

		[Fact]
		public async Task Index_ToReturnView() 
		{
			//Act
			var response = await _httpClient.GetAsync("/Trade/Index/");

			//Assert
			response.Should().BeSuccessful();
			var responseBody = await response.Content.ReadAsStringAsync();

			var html = new HtmlDocument();

			html.LoadHtml(responseBody);
			var document = html.DocumentNode;

			document.QuerySelectorAll(".stock-price").Should().NotBeNull();
		}
	}
}
