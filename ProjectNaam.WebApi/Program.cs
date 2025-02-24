using ProjectNaam.WebApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var sqlConnectionString = builder.Configuration["SqlConnectionString"];
builder.Services.AddTransient<Object2DRepository, Object2DRepository>(o => new Object2DRepository(sqlConnectionString));
builder.Services.AddTransient<Enviroment2DRepository, Enviroment2DRepository>(o => new Enviroment2DRepository(sqlConnectionString));
var app = builder.Build();
var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(sqlConnectionString);
app.MapGet("/", () => $"The API is up 🚀. Connection string found: {(sqlConnectionStringFound ? "✅" : "❎")}");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
