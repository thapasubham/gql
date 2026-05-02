using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Gql.Models;
using Microsoft.Extensions.Configuration;

namespace Gql.Services;

public class RiotApiService : IRiotApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public RiotApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["RiotApi:ApiKey"] ?? throw new ArgumentNullException("RiotApi:ApiKey is not configured.");
        
        _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", _apiKey);
    }

    public async Task<Summoner?> GetSummonerByNameAsync(string summonerName, string region = "sg2", CancellationToken cancellationToken = default)
    {
        string? puuid = null;

        if (summonerName.Contains('#'))
        {
            var parts = summonerName.Split('#');
            var gameName = parts[0];
            var tagLine = parts[1];

            var cluster = region == "sg2" ? "asia" : "americas"; 
            var accountUrl = $"https://{cluster}.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{Uri.EscapeDataString(gameName)}/{Uri.EscapeDataString(tagLine)}";
            
            var accountResponse = await _httpClient.GetAsync(accountUrl, cancellationToken);
            if (!accountResponse.IsSuccessStatusCode) return null;

            var accountData = await accountResponse.Content.ReadFromJsonAsync<AccountResponse>(cancellationToken: cancellationToken);
            puuid = accountData?.Puuid;
        }

        string url;
        if (!string.IsNullOrEmpty(puuid))
        {
            url = $"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{puuid}";
        }
        else
        {
            url = $"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/by-name/{Uri.EscapeDataString(summonerName)}";
        }
        
        var response = await _httpClient.GetAsync(url, cancellationToken);
        
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Summoner>(cancellationToken: cancellationToken);
    }

    private class AccountResponse
    {
        [JsonPropertyName("puuid")]
        public string Puuid { get; set; } = string.Empty;
    }

    public async Task<List<Match>> GetMatchHistoryByPuuidAsync(string puuid, string cluster = "sea", CancellationToken cancellationToken = default)
    {
        var url = $"https://{cluster}.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids?start=0&count=20";
        
        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        var matchIds = await response.Content.ReadFromJsonAsync<List<string>>(cancellationToken: cancellationToken);
        
        return matchIds?.Select(id => new Match { Id = id }).ToList() ?? new List<Match>();
    }

    public async Task<List<ChampionMastery>> GetChampionMasteriesAsync(string puuid, string region = "sg2", CancellationToken cancellationToken = default)
    {
        var url = $"https://{region}.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-puuid/{puuid}";
        
        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<ChampionMastery>>(cancellationToken: cancellationToken) ?? new List<ChampionMastery>();
    }
}
