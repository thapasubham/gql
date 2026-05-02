using Gql.Models;

namespace Gql.Services;

public interface IRiotApiService
{
    Task<Summoner?> GetSummonerByNameAsync(string summonerName, string region = "sg2", CancellationToken cancellationToken = default);

    Task<List<Match>> GetMatchHistoryByPuuidAsync(string puuid, string cluster = "sea", CancellationToken cancellationToken = default);

    Task<List<ChampionMastery>> GetChampionMasteriesAsync(string puuid, string region = "sg2", CancellationToken cancellationToken = default);
}
