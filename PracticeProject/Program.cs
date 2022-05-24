using Microsoft.EntityFrameworkCore;
using PracticeProject;
using PracticeProject.Data;

var builder = WebApplication.CreateBuilder(args);

//var connectionString = string.Empty;
//connectionString = builder.Configuration.GetConnectionString("Development");
var _env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var isDevelopment = _env == Environments.Development;
var isProduction = _env == Environments.Production;
var isStaging = _env == Environments.Staging;
//builder.Services.AddDbContext<HRDbContext>(options =>
//options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = string.Empty;

if (isDevelopment)
{
    connectionString = builder.Configuration.GetConnectionString("Development");

}
else if (isProduction)
{
    connectionString = builder.Configuration.GetConnectionString("Production");
}

builder.Services.AddDbContext<HomeDbContext>(options =>
options.UseSqlServer(connectionString));

DependencyContainer.RegisterServices(builder.Services); //creating request pipeline
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
