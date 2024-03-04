using CRUDAssignment.Controllers;
using CRUDAssignment.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;

namespace CRUDAssignment.Filters.ActionFilters
{
	public class CreateOrderActionFilter : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var orderRequest = (OrderRequest?)context.ActionArguments.Single().Value;

			if (context.Controller is TradeController tradeController && orderRequest != null)
			{
				orderRequest.DateAndTimeOfOrder = DateTime.Now;
				tradeController.ModelState.Clear();

				if (!tradeController.ModelState.IsValid)
				{
					var stockTrade = new StockTrade { StockSymbol = orderRequest.StockSymbol, StockName = orderRequest.StockName, Price = orderRequest.Price, 
						Quantity = orderRequest.Quantity };
					tradeController.View("Index", stockTrade);
				}
				else
				{
					await next();
				}
			}
			else 
			{
				await next();
			}
		}
	}
}
