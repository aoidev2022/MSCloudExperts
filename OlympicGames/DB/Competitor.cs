namespace OlympicGames.DB;

public class Competitor
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public ICollection<Sample>? Samples { get; set; }
}
