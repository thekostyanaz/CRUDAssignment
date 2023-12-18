using AutoFixture;
using Entities;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using Xunit;

namespace Tests
{
	public class StockServiceTests
	{
		private readonly IStockService _stockService;
		private readonly IStocksRepository _stocksRepository;
		private readonly Mock<IStocksRepository> _stockRepositoryMock;
		private readonly IFixture _fixture;

		public StockServiceTests() 
		{
			_fixture = new Fixture();
			_stockRepositoryMock = new Mock<IStocksRepository>();
			_stocksRepository = _stockRepositoryMock.Object;
			_stockService = new StockService(_stocksRepository);
		}

		#region CreateBuyOrder

		[Fact]
		public async Task CreateBuyOrder_NullRequest_ToBeArgumentNullException()
		{
			BuyOrderRequest? buyOrderRequest = null;

			var action = async() => await _stockService.CreateBuyOrder(buyOrderRequest);

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task CreateBuyOrder_ZeroQuantity_ToBeArgumentException()
		{
			var buyOrderRequest = _fixture.Build<BuyOrderRequest>()
				.With(temp => temp.Quantity,  0)
				.Create();
			var buyOrder = buyOrderRequest.ToBuyOrder();

			_stockRepositoryMock
				.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
				.ReturnsAsync(buyOrder);

			var action = async () => await _stockService.CreateBuyOrder(buyOrderRequest);

			var exception = await action.Should().ThrowAsync<ArgumentException>();
			exception.Which.Message.Should().Be("Quantity value should be between 1 and 100000", "Incorrect validation message is returned.");
		}

		[Fact]
		public async Task CreateBuyOrder_BigQuantity_ToBeArgumentException()
		{
			var buyOrderRequest = _fixture.Build<BuyOrderRequest>()
				.With(temp => temp.Quantity, 100001)
				.Create();
			var buyOrder = buyOrderRequest.ToBuyOrder();

			_stockRepositoryMock
				.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
				.ReturnsAsync(buyOrder);

			var action = async () => await _stockService.CreateBuyOrder(buyOrderRequest);

			var exception = await action.Should().ThrowAsync<ArgumentException>();
			exception.Which.Message.Should().Be("Quantity value should be between 1 and 100000", "Incorrect validation message is returned.");
		}

		[Fact]
		public async Task CreateBuyOrder_ZeroPrice_ToBeArgumentException()
		{
			var buyOrderRequest = _fixture.Build<BuyOrderRequest>()
				.With(temp => temp.Price, 0)
				.Create();
			var buyOrder = buyOrderRequest.ToBuyOrder();

			_stockRepositoryMock
				.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
				.ReturnsAsync(buyOrder);

			var action = async () => await _stockService.CreateBuyOrder(buyOrderRequest);

			var exception = await action.Should().ThrowAsync<ArgumentException>();
			exception.Which.Message.Should().Be("Price value should be between 1 and 10000", "Incorrect validation message is returned.");
		}

		[Fact]
		public async Task CreateBuyOrder_BigPrice_ToBeArgumentException()
		{
			var buyOrderRequest = _fixture.Build<BuyOrderRequest>()
				.With(temp => temp.Price, 10001)
				.Create();
			var buyOrder = buyOrderRequest.ToBuyOrder();

			_stockRepositoryMock
				.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
				.ReturnsAsync(buyOrder);

			var action = async () => await _stockService.CreateBuyOrder(buyOrderRequest);

			var exception = await action.Should().ThrowAsync<ArgumentException>();
			exception.Which.Message.Should().Be("Price value should be between 1 and 10000", "Incorrect validation message is returned.");
		}

		[Fact]
		public async Task CreateBuyOrder_NullSymbol_ToBeArgumentException()
		{
			var buyOrderRequest = _fixture.Build<BuyOrderRequest>()
				.With(temp => temp.StockSymbol, null as string)
				.Create();
			var buyOrder = buyOrderRequest.ToBuyOrder();

			_stockRepositoryMock
				.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
				.ReturnsAsync(buyOrder);

			var action = async () => await _stockService.CreateBuyOrder(buyOrderRequest);

			var exception = await action.Should().ThrowAsync<ArgumentException>();
			exception.Which.Message.Should().Be("Stock symbol cannot be blank", "Incorrect validation message is returned.");
		}

		[Fact]
		public async Task CreateBuyOrder_IncorrectDate_ToBeArgumentException()
		{
			var buyOrderRequest = _fixture.Build<BuyOrderRequest>()
				.With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("1999-12-31"))
				.Create();
			var buyOrder = buyOrderRequest.ToBuyOrder();

			_stockRepositoryMock
				.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
				.ReturnsAsync(buyOrder);

			var action = async () => await _stockService.CreateBuyOrder(buyOrderRequest);

			var exception = await action.Should().ThrowAsync<ArgumentException>();
			exception.Which.Message.Should().Be("Date And Time Of Order should be later then 2000-01-01", "Incorrect validation message is returned.");
		}

		[Fact]
		public async Task CreateBuyOrder_ToBeSuccessful()
		{
			var buyOrderRequest = _fixture.Build<BuyOrderRequest>().Create();
			var buyOrder = buyOrderRequest.ToBuyOrder();

			_stockRepositoryMock
				.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
				.ReturnsAsync(buyOrder);

			var response = await _stockService.CreateBuyOrder(buyOrderRequest);

			response.Should().NotBeNull();
			response.BuyOrderID.Should().NotBeEmpty();
		}

		#endregion

		#region CreateSellOrder

		[Fact]
		public async Task CreateSellOrder_NullRequest_ToBeArgumentNullException()
		{
			SellOrderRequest? sellOrderRequest = null;

			var action = async () => await _stockService.CreateSellOrder(sellOrderRequest);

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task CreateSellOrder_ZeroQuantity_ToBeArgumentException()
		{
			var sellOrderRequest = _fixture.Build<SellOrderRequest>()
				.With(temp => temp.Quantity, 0)
				.Create();
			var sellOrder = sellOrderRequest.ToSellOrder();

			_stockRepositoryMock
				.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
				.ReturnsAsync(sellOrder);

			var action = async () => await _stockService.CreateSellOrder(sellOrderRequest);

			var exception = await action.Should().ThrowAsync<ArgumentException>();
			exception.Which.Message.Should().Be("Quantity value should be between 1 and 100000", "Incorrect validation message is returned.");
		}

		[Fact]
		public async Task CreateSellOrder_BigQuantity_ToBeArgumentException()
		{
			var sellOrderRequest = _fixture.Build<SellOrderRequest>()
				.With(temp => temp.Quantity, 100001)
				.Create();
			var sellOrder = sellOrderRequest.ToSellOrder();

			_stockRepositoryMock
				.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
				.ReturnsAsync(sellOrder);

			var action = async () => await _stockService.CreateSellOrder(sellOrderRequest);

			var exception = await action.Should().ThrowAsync<ArgumentException>();
			exception.Which.Message.Should().Be("Quantity value should be between 1 and 100000", "Incorrect validation message is returned.");
		}

		[Fact]
		public async Task CreateSellOrder_ZeroPrice_ToBeArgumentException()
		{
			var sellOrderRequest = _fixture.Build<SellOrderRequest>()
				.With(temp => temp.Price, 0)
				.Create();
			var sellOrder = sellOrderRequest.ToSellOrder();

			_stockRepositoryMock
				.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
				.ReturnsAsync(sellOrder);

			var action = async () => await _stockService.CreateSellOrder(sellOrderRequest);

			var exception = await action.Should().ThrowAsync<ArgumentException>();
			exception.Which.Message.Should().Be("Price value should be between 1 and 10000", "Incorrect validation message is returned.");
		}

		[Fact]
		public async Task CreatSellOrder_BigPrice_ToBeArgumentException()
		{
			var sellOrderRequest = _fixture.Build<SellOrderRequest>()
				.With(temp => temp.Price, 10001)
				.Create();
			var sellOrder = sellOrderRequest.ToSellOrder();

			_stockRepositoryMock
				.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
				.ReturnsAsync(sellOrder);

			var action = async () => await _stockService.CreateSellOrder(sellOrderRequest);

			var exception = await action.Should().ThrowAsync<ArgumentException>();
			exception.Which.Message.Should().Be("Price value should be between 1 and 10000", "Incorrect validation message is returned.");
		}

		[Fact]
		public async Task CreateSellOrder_NullSymbol_ToBeArgumentException()
		{
			var sellOrderRequest = _fixture.Build<SellOrderRequest>()
				.With(temp => temp.StockSymbol, null as string)
				.Create();
			var sellOrder = sellOrderRequest.ToSellOrder();

			_stockRepositoryMock
				.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
				.ReturnsAsync(sellOrder);

			var action = async () => await _stockService.CreateSellOrder(sellOrderRequest);

			var exception = await action.Should().ThrowAsync<ArgumentException>();
			exception.Which.Message.Should().Be("Stock symbol cannot be blank", "Incorrect validation message is returned.");
		}

		[Fact]
		public async Task CreateSellOrder_IncorrectDate_ToBeArgumentException()
		{
			var sellOrderRequest = _fixture.Build<SellOrderRequest>()
				.With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("1999-12-31"))
				.Create();
			var sellOrder = sellOrderRequest.ToSellOrder();

			_stockRepositoryMock
				.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
				.ReturnsAsync(sellOrder);

			var action = async () => await _stockService.CreateSellOrder(sellOrderRequest);

			var exception = await action.Should().ThrowAsync<ArgumentException>();
			exception.Which.Message.Should().Be("Date And Time Of Order should be later then 2000-01-01", "Incorrect validation message is returned.");
		}

		[Fact]
		public async Task CreateSellOrder_ToBeSuccessful()
		{
			var sellOrderRequest = _fixture.Build<SellOrderRequest>().Create();
			var sellOrder = sellOrderRequest.ToSellOrder();

			_stockRepositoryMock
				.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
				.ReturnsAsync(sellOrder);

			var response = await _stockService.CreateSellOrder(sellOrderRequest);

			response.Should().NotBeNull();
			response.SellOrderID.Should().NotBeEmpty();
		}

		#endregion

		#region GetAllBuyOrders

		[Fact]
		public async Task GetAllBuyOrders_ToBeEmpty()
		{
			var ordersList = new List<BuyOrder>();

			_stockRepositoryMock
				.Setup(temp => temp.GetBuyOrders())
				.ReturnsAsync(ordersList);

			var response = await _stockService.GetBuyOrders();

			response.Should().BeEmpty();
		}

		[Fact]
		public async Task GetAllBuyOrders_AddFewBuyOrders_ToBeSuccessfull()
		{
			var ordersList = new List<BuyOrder>()
			{
				_fixture.Build<BuyOrder>().Create(),
				_fixture.Build<BuyOrder>().Create()
			};

			_stockRepositoryMock
				.Setup(temp => temp.GetBuyOrders())
				.ReturnsAsync(ordersList);

			var response = await _stockService.GetBuyOrders();

			ordersList.ForEach(o => response.Should().Contain(o.ToBuyOrderResponse()));
		}

		#endregion

		#region GetAllSellOrders

		[Fact]
		public async Task GetAllSellOrders_ToBeEmpty()
		{
			var ordersList = new List<SellOrder>();

			_stockRepositoryMock
				.Setup(temp => temp.GetSellOrders())
				.ReturnsAsync(ordersList);

			var response = await _stockService.GetSellOrders();

			response.Should().BeEmpty();
		}

		[Fact]
		public async Task GetAllSellOrders_AddFewSellOrders_ToBeSuccessfull()
		{
			var ordersList = new List<SellOrder>()
			{
				_fixture.Build<SellOrder>().Create(),
				_fixture.Build<SellOrder>().Create()
			};

			_stockRepositoryMock
				.Setup(temp => temp.GetSellOrders())
				.ReturnsAsync(ordersList);

			var response = await _stockService.GetSellOrders();

			ordersList.ForEach(o => response.Should().Contain(o.ToSellOrderResponse()));
		}

		#endregion
	}
}