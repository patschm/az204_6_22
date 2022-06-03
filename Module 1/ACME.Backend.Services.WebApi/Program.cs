using ACME.Backend.Interfaces;
using ACME.Backend.Repository.SqlServer;
using Newtonsoft.Json;
using ACME.Backend.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShopContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopDatabase"));
});
builder.Services.AddTransient<IShopUnitOfWork, ShopUnitOfWork>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

public partial class Program { }
