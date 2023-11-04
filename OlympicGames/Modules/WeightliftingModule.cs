using AutoMapper;

using Microsoft.EntityFrameworkCore;

using OlympicGames.DB;
using OlympicGames.DTO;

namespace OlympicGames.Modules;

public static class WeightliftingModule
{
    public static void RegisterWeightliftingEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/weightlifting", async (ApplicationDbContext db) =>
        {
            var list = await db.Samples.ToListAsync();
            return Results.Ok(list);
        }).RequireAuthorization();

        app.MapGet("/weightlifting/{id}", async (int id, ApplicationDbContext db) =>
        {
            var sample = await db.Samples.SingleOrDefaultAsync(q => q.Id == id);

            if (sample is null) return Results.NoContent();

            return Results.Ok(sample);
        }).RequireAuthorization();

        app.MapGet("/weightlifting/rank", async (ApplicationDbContext db) =>
        {
            var list = (await db.Samples
                .GroupBy(q => new { q.Competitor.Id, q.Competitor.FullName })
                .Select(g => new RankSummary
                {
                    Name = $"{g.Key.FullName} -> DNI# {g.Key.Id}",
                    SnatchScore = g.Where(r => r.Mode == Mode.Snatch).Max(r => r.Score),
                    CleanAndJerkScore = g.Where(r => r.Mode == Mode.CleanAndJerk).Max(r => r.Score)
                }).ToListAsync())
                .OrderByDescending(q => q.TotalWeight);

            return Results.Ok(list);
        }).RequireAuthorization();

        app.MapPost("/weightlifting", async (Sample sample, ApplicationDbContext db, CancellationToken ct) =>
        {
            await db.Samples.AddAsync(sample);
            await db.SaveChangesAsync(ct);
            return Results.Created($"/weightlifting/{sample.Id}", sample);
        }).RequireAuthorization();

        app.MapPut("/weightlifting/{id}", async (int id, Sample sample, IMapper mapper, ApplicationDbContext db) =>
        {
            var target = await db.Samples.SingleAsync(q => q.Id == id);
            mapper.Map(sample, target);

            await db.SaveChangesAsync();

            return Results.NoContent();
        }).RequireAuthorization();

        app.MapDelete("/weightlifting/{id}", async (int id, ApplicationDbContext db) =>
        {
            var targetSample = await db.Samples.SingleOrDefaultAsync(q => q.Id == id);

            if (targetSample == null)
                return Results.NotFound();

            db.Samples.Remove(targetSample);
            await db.SaveChangesAsync();

            return Results.NoContent();
        }).RequireAuthorization();

    }
}
