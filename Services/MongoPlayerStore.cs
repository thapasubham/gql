using Gql.Model;
using MongoDB.Driver;

namespace Gql.Services;

public sealed class MongoPlayerStore : IPlayerStore
{
    private readonly IMongoCollection<Player> _players;

    public MongoPlayerStore(IMongoDatabase database)
    {
        _players = database.GetCollection<Player>("players");
    }

    public async Task<IReadOnlyList<Player>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cursor = await _players.FindAsync(FilterDefinition<Player>.Empty, cancellationToken: cancellationToken);
        var list = await cursor.ToListAsync(cancellationToken);
        list.Sort(static (a, b) => string.Compare(a.Username, b.Username, StringComparison.OrdinalIgnoreCase));
        return list;
    }

    public async Task<Player?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        var player = await _players.Find(p => p.Username == username).FirstOrDefaultAsync(cancellationToken);
        return player;
    }

    public async Task<Player?> AddItemAsync(
        string username,
        string name,
        ItemType type,
        int power,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Username, username);
        var update = Builders<Player>.Update.Push(
            p => p.Items,
            new Item { Name = name, Type = type, Power = power });
        var options = new FindOneAndUpdateOptions<Player> { ReturnDocument = ReturnDocument.After };
        return await _players.FindOneAndUpdateAsync(filter, update, options, cancellationToken);
    }

    public async Task<Player?> AddExperienceAsync(string username, int amount, CancellationToken cancellationToken = default)
    {
        if (amount <= 0)
            return null;

        var player = await GetByUsernameAsync(username, cancellationToken);
        if (player is null)
            return null;

        player.Experience += amount;
        while (player.Experience >= ExperiencePerLevel(player.Level))
        {
            player.Experience -= ExperiencePerLevel(player.Level);
            player.Level++;
        }

        var filter = Builders<Player>.Filter.Eq(p => p.Username, username);
        await _players.ReplaceOneAsync(filter, player, cancellationToken: cancellationToken);
        return player;
    }

    private static int ExperiencePerLevel(int level) => 100 + (level - 1) * 25;
}
