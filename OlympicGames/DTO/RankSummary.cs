namespace OlympicGames.DTO;

public class RankSummary
{
    public string Name { get; set; } = string.Empty;
    public int SnatchScore { get; set; }
    public int CleanAndJerkScore { get; set; }
    public int TotalWeight => SnatchScore + CleanAndJerkScore;
}
