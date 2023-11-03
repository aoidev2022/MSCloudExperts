using Microsoft.EntityFrameworkCore;

namespace OlympicGames.DB;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Competitor>()
            .HasData(new List<Competitor> {
                new Competitor { Id=1, Country="COL", FullName="Anthony Boral" },
                new Competitor { Id=2, Country="CHN", FullName="Marcela Lopez" },
                new Competitor { Id=3, Country="AUS", FullName="Alejandra Ortega" }
            });

        modelBuilder.Entity<Sample>()
            .HasData(new List<Sample> {
                new Sample { Id = 1, CompetitorId=1, Mode=Mode.Snatch, Score=134 },
                new Sample { Id = 2, CompetitorId=1, Mode=Mode.Snatch, Score=130 },
                new Sample { Id = 3, CompetitorId=1, Mode=Mode.Snatch, Score=120 },
                new Sample { Id = 4, CompetitorId=1, Mode=Mode.CleanAndJerk, Score=177 },
                new Sample { Id = 5, CompetitorId=1, Mode=Mode.CleanAndJerk, Score=100 },
                new Sample { Id = 6, CompetitorId=1, Mode=Mode.CleanAndJerk, Score=115 },

                new Sample { Id = 7, CompetitorId=2, Mode=Mode.Snatch, Score=130 },
                new Sample { Id = 8, CompetitorId=2, Mode=Mode.Snatch, Score=90 },
                new Sample { Id = 9, CompetitorId=2, Mode=Mode.Snatch, Score=125 },
                new Sample { Id = 10, CompetitorId=2, Mode=Mode.CleanAndJerk, Score=180 },
                new Sample { Id = 11, CompetitorId=2, Mode=Mode.CleanAndJerk, Score=112 },
                new Sample { Id = 12, CompetitorId=2, Mode=Mode.CleanAndJerk, Score=120 },

                new Sample { Id = 13, CompetitorId=3, Mode=Mode.Snatch, Score=0 },
                new Sample { Id = 14, CompetitorId=3, Mode=Mode.Snatch, Score=0 },
                new Sample { Id = 15, CompetitorId=3, Mode=Mode.Snatch, Score=0 },
                new Sample { Id = 16, CompetitorId=3, Mode=Mode.CleanAndJerk, Score=150 },
                new Sample { Id = 17, CompetitorId=3, Mode=Mode.CleanAndJerk, Score=149 },
                new Sample { Id = 18, CompetitorId=3, Mode=Mode.CleanAndJerk, Score=150 },
            });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Sample> Samples { get; set; }
}
