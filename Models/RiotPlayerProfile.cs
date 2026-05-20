namespace Gql.Models;

public class RiotPlayerProfile
{
    public Summoner Summoner { get; set; } = null!;

    public IReadOnlyList<ChampionMastery> ChampionMasteries { get; set; } = [];

    public IReadOnlyList<Match> RecentMatches { get; set; } = [];
}
