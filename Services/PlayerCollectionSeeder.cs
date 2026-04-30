using Gql.Model;
using MongoDB.Driver;

namespace Gql.Services;

internal sealed class PlayerCollectionSeeder : IHostedService
{
    private readonly IMongoCollection<Player> _players;

    public PlayerCollectionSeeder(IMongoDatabase database)
    {
        _players = database.GetCollection<Player>("players");
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var count = await _players.CountDocumentsAsync(FilterDefinition<Player>.Empty, cancellationToken: cancellationToken);
        if (count > 0)
            return;

        var seed = new[]
        {
            new Player
            {
                Username = "Subham",
                Level = 5,
                Experience = 120,
                Items =
                [
                    new Item { Name = "Iron Sword", Type = ItemType.Weapon, Power = 10 },
                    new Item { Name = "Health Potion", Type = ItemType.Potion, Power = 50 }
                ]
            },
            new Player
            {
                Username = "Alex",
                Level = 3,
                Experience = 45,
                Items = [new Item { Name = "Leather Boots", Type = ItemType.Armor, Power = 3 }]
            }
        };

        await _players.InsertManyAsync(seed, cancellationToken: cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
