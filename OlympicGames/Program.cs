using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;

using OlympicGames.DB;
using OlympicGames.Identity;
using OlympicGames.Modules;

using Serilog.Events;
using Serilog;

using System.Reflection;

var name = typeof(Program).Assembly.GetName().Name;
Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Assembly", name)
                // available sinks: https://github.com/serilog/serilog/wiki/Provided-Sinks
                // Seq: https://datalust.co/seq
                // Seq with Docker: https://docs.datalust.co/docs/getting-started-with-docker
                .WriteTo.Seq(serverUrl: "http://ms_seq:5341")
                .WriteTo.Console()
                .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("db")));

builder.Services.AddJwtBearerAuthentication();
builder.Services.AddAuthorization();

IdentityModelEventSource.ShowPII = true;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.RegisterWeightliftingEndpoints();

app.Run();