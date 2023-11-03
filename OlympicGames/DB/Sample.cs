namespace OlympicGames.DB;

public class Sample
{
    public int Id { get; set; }
    public int CompetitorId { get; set; }
    public Mode Mode { get; set; }
    public int Score { get; set; }

    public Competitor Competitor { get; set; }
};
