using AutoMapper;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var tmpData = Enumerable.Range(1, 5)
    .Select(i => new Sample { Id = i, Competitor = $"Competitor {i}", Country = $"Country {i}", CleanAndJerkScore = 998, SnatchScore = 23, TotalWeightScore = 232 })
    .ToList();

app.MapGet("/weightlifting", () =>
{
    return Results.Ok(tmpData);
});

app.MapGet("/weightlifting/{id}", (int id) =>
{
    return Results.Ok(tmpData.Single(q => q.Id == id));
});

app.MapPut("/weightlifting/{id}", (int id, Sample sample, IMapper mapper) =>
{
    var target = tmpData.Single(q => q.Id == id);
    mapper.Map(sample, target);
    return Results.NoContent();
});

app.MapDelete("/weightlifting/{id}", (int id) =>
{
    var targetSample = tmpData.SingleOrDefault(q => q.Id == id);

    if (targetSample == null)
        return Results.NotFound();

    tmpData.Remove(targetSample);

    return Results.NoContent();
});

app.Run();

internal class Sample
{
    public int Id { get; set; }
    public string Competitor { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public int SnatchScore { get; set; }
    public int CleanAndJerkScore { get; set; }
    public int TotalWeightScore { get; set; }
};