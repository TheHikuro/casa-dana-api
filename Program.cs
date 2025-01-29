using CasaDanaAPI.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = $"Host={Env.GetString("DATABASE_HOST")};Port={Env.GetString("DATABASE_PORT")};Database={Env.GetString("DATABASE_NAME")};Username={Env.GetString("DATABASE_USER")};Password={Env.GetString("DATABASE_PASSWORD")}";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();