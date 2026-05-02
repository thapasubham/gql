using System.Text.Json.Serialization;

namespace Gql.Models;

public class ChampionMastery
{
    [JsonPropertyName("puuid")]
    public string Puuid { get; set; } = string.Empty;

    [JsonPropertyName("championId")]
    public long ChampionId { get; set; }

    [JsonPropertyName("championLevel")]
    public int ChampionLevel { get; set; }

    [JsonPropertyName("championPoints")]
    public int ChampionPoints { get; set; }

    [JsonPropertyName("lastPlayTime")]
    public long LastPlayTime { get; set; }

    [JsonPropertyName("championPointsSinceLastLevel")]
    public long ChampionPointsSinceLastLevel { get; set; }

    [JsonPropertyName("championPointsUntilNextLevel")]
    public long ChampionPointsUntilNextLevel { get; set; }

    [JsonPropertyName("chestGranted")]
    public bool ChestGranted { get; set; }

    [JsonPropertyName("tokensEarned")]
    public int TokensEarned { get; set; }
}
