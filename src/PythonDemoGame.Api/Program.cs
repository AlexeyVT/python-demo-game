global using System.Linq;
using PythonDemoGame.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHostedService<GameService>();

var app = builder.Build();

app.MapControllers();

app.Run();