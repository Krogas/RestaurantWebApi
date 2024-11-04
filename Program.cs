using RestaurantAPI;
using RestaurantWebApi;
using RestaurantWebApi.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IWeatherForeCastService, WeatherForeCastService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<RestaurantContext>();
builder.Services.AddScoped<RestaurantSeeder>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();

seeder.Seed();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
