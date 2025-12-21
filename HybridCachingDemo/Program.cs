using HybridCachingDemo.Models;
using HybridCachingDemo.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>( opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("PrimaryDB"));
});

//Rate limiter configurations
builder.Services.AddRateLimiter(opt =>
{
    opt.AddFixedWindowLimiter("Default-Policy", policy =>
    {
        policy.Window = TimeSpan.FromMinutes(1);
        policy.PermitLimit = 10;
        policy.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        policy.QueueLimit = 3;
    });
});



//configuration Redis (layer 2)
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
    opt.InstanceName = "M03:";
});

//configuration Sql Cache
//builder.Services.AddDistributedSqlServerCache(opt =>
//{
//    opt.ConnectionString = builder.Configuration.GetConnectionString("SqlCache");
//    opt.SchemaName = "dbo";
//    opt.TableName = "cacheEntries";
//});

//configuration Hybrid Cache
builder.Services.AddHybridCache(opt =>
{
    opt.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromMinutes(10), // for L2
        LocalCacheExpiration = TimeSpan.FromSeconds(30) // for local (in-memory)
    };
});




builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

app.UseRateLimiter();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
