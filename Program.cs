using CasaDanaAPI.Data;
using CasaDanaAPI.Services;
using CasaDanaAPI.Services.Interfaces;
using CasaDanaAPI.Repositories;
using CasaDanaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = $"Host={Env.GetString("DATABASE_HOST")};Port={Env.GetString("DATABASE_PORT")};Database={Env.GetString("DATABASE_NAME")};Username={Env.GetString("DATABASE_USER")};Password={Env.GetString("DATABASE_PASSWORD")}";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

var jwtSettings = new
{
    SecretKey = Env.GetString("JWT_SECRET_KEY"),
    Issuer = Env.GetString("JWT_ISSUER"),
    Audience = Env.GetString("JWT_AUDIENCE")
};

var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
