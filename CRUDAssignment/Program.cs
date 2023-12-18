using ServiceContracts;
using Services;
using CRUDAssignment.Options;
using Entities;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using RepositoryContracts;
using Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();
builder.Services.AddScoped<IStocksRepository, StockRepository>();

builder.Services.AddDbContext<StockMarketDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!builder.Environment.IsEnvironment("Test"))
{
	RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
}

app.UseStaticFiles();
app.MapControllers();

app.Run();

public partial class Program { }