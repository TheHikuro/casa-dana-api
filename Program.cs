using CasaDanaAPI.Data;
using CasaDanaAPI.Services;
using CasaDanaAPI.Services.Interfaces;
using CasaDanaAPI.Repositories;
using CasaDanaAPI.Repositories.Interfaces;
using CasaDanaAPI.Extensions; 
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = $"Host={Env.GetString("DATABASE_HOST")};" +
                       $"Port={Env.GetString("DATABASE_PORT")};" +
                       $"Database={Env.GetString("DATABASE_NAME")};" +
                       $"Username={Env.GetString("DATABASE_USER")};" +
                       $"Password={Env.GetString("DATABASE_PASSWORD")};";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddJwtAuthentication();

builder.Services.AddSwaggerWithAuth();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseSwaggerWithUi();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();