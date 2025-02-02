using CasaDanaAPI.Data;
using CasaDanaAPI.Services;
using CasaDanaAPI.Services.Interfaces;
using CasaDanaAPI.Repositories;
using CasaDanaAPI.Repositories.Interfaces;
using CasaDanaAPI.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();