using Gql.Models;
using Gql.Services;
using HotChocolate;
using HotChocolate.Types;

namespace Gql.Graphql.Queries;

[ExtendObjectType(typeof(Query))]
public class SummonerQuery
{
    public async Task<Summoner?> GetSummoner(
        string name,
        [Service] IRiotApiService riotService,
        CancellationToken cancellationToken,
        string region = "sg2")
    {
        return await riotService.GetSummonerByNameAsync(name, region, cancellationToken);
    }

    public async Task<List<Match>> GetMatchHistory(
        string puuid,
        [Service] IRiotApiService riotService,
        CancellationToken cancellationToken,
        string cluster = "sea")
    {
        return await riotService.GetMatchHistoryByPuuidAsync(puuid, cluster, cancellationToken);
    }

    public async Task<List<ChampionMastery>> GetChampionMastery(
        string puuid,
        [Service] IRiotApiService riotService,
        CancellationToken cancellationToken,
        string region = "sg2")
    {
        return await riotService.GetChampionMasteriesAsync(puuid, region, cancellationToken);
    }
}
