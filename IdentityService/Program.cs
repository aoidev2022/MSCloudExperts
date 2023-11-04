using IdentityService.Entities;
using IdentityService.Modules;
using IdentityService.Settings;

using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

using Serilog.Events;
using Serilog;

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
builder.Services.AddAuthorization();

BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));

var mongoSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

builder.Services
    .AddDefaultIdentity<ApplicationUser>()
    .AddRoles<ApplicationRole>()
    .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(mongoSettings.ConnectionString, "IdentityService");

var identityServerSettings = builder.Configuration.GetSection(nameof(IdentityServerSettings)).Get<IdentityServerSettings>();

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseSuccessEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseErrorEvents = true;

    options.IssuerUri = "http://ms_identityservice";
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

//app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

app.RegisterUserEndpoints();

app.Run();
