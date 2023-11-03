using IdentityService.DTO;
using IdentityService.Entities;
using IdentityService.Settings;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityServiceIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityServiceIdentityDbContextConnection' not found.");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));

builder.Services
    .AddDefaultIdentity<ApplicationUser>()
    .AddRoles<ApplicationRole>()
    .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>($"mongodb://localhost:27017", "IdentityService");



var identityServerSettings = builder.Configuration.GetSection(nameof(IdentityServerSettings)).Get<IdentityServerSettings>();

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseSuccessEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseErrorEvents = true;
})
.AddAspNetIdentity<ApplicationUser>()
.AddInMemoryApiScopes(identityServerSettings.ApiScopes)
.AddInMemoryApiResources(identityServerSettings.ApiResources)
.AddInMemoryClients(identityServerSettings.Clients)
.AddInMemoryIdentityResources(identityServerSettings.IdentityResources)
.AddDeveloperSigningCredential();

builder.Services.ConfigureExternalCookie(options =>
{
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Unspecified;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Unspecified;
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

app.MapGet("/user", (UserManager<ApplicationUser> _userManager) =>
{
    var list = _userManager.Users.ToList();
    return Results.Ok(list);
});

app.MapPost("/user", async (NewUserInput user, UserManager<ApplicationUser> _userManager) =>
{
    ApplicationUser applicationUser = new()
    {
        Email = user.Email,
        UserName = user.Email
    };

    var result = await _userManager.CreateAsync(applicationUser, user.Password);

    return Results.NoContent();
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}