using CasaDanaAPI.Extensions;
using CasaDanaAPI.Infrastructure.Configuration;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddDatabase();
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerWithAuth();

builder.Services.AddControllers();
builder.Services.AddMappings();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddJwtAuthentication();
builder.Services.AddCustomCors();

var app = builder.Build();
app.MigrateDatabase();

app.UseSwaggerWithUi(Env.GetString("ASPNETCORE_ENVIRONMENT"));
app.UseCustomCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();