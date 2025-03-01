using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using ProjectNaam.WebApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var sqlConnectionString = builder.Configuration["SqlConnectionString"];
builder.Services.AddTransient<Object2DRepository, Object2DRepository>(o => new Object2DRepository(sqlConnectionString));
builder.Services.AddTransient<Environment2DRepository, Environment2DRepository>(o => new Environment2DRepository(sqlConnectionString));

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 10;
})
.AddRoles<IdentityRole>()
.AddDapperStores(options =>
{
    options.ConnectionString = sqlConnectionString;
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthenticationService, AspNetIdentityAuthenticationService>();

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

app.MapGroup("/account").MapIdentityApi<IdentityUser>();

app.MapControllers().RequireAuthorization();

app.Run();
