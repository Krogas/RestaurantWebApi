using System.Reflection;
using NLog.Web;
using RestaurantAPI;
using RestaurantWebApi.Entities;
using RestaurantWebApi.Middleware;
using RestaurantWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Host.UseNLog();

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddDbContext<RestaurantContext>();
builder.Services.AddScoped<RestaurantSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ErrorHandlingMiddleWare>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();

seeder.Seed();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleWare>();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API");
});

//app.UseAuthorization();

app.MapControllers();

app.Run();
