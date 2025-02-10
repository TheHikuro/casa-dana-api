using CasaDanaAPI.Extensions;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase();
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerWithAuth();

builder.Services.AddControllers();
builder.Services.AddMappings();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddJwtAuthentication();

var app = builder.Build();
app.MigrateDatabase();

app.UseSwaggerWithUi(Env.GetString("ASPNETCORE_ENVIRONMENT"));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();