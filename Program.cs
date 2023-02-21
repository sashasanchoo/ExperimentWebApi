using Microsoft.EntityFrameworkCore;
using TestTaskWebAPI.Data;
using TestTaskWebAPI.Services;
using TestTaskWebAPI.Services.Experement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TestTaskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestTaskContext") ?? throw new InvalidOperationException("Connection string 'IShopContext' not found.")));

builder.Services.AddControllers();
builder.Services.AddScoped<ApiKeyService>();
builder.Services.AddScoped<ButtonColorExpResponseValueGenerator>();
builder.Services.AddScoped<PriceExpResponseValueGenerator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseStaticFiles();

app.Run();
