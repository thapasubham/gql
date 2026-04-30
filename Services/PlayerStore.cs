using System.Collections.Concurrent;

public sealed class PlayerStore : IPlayerStore
{
    private readonly ConcurrentDictionary<string, Player> _players = new(StringComparer.OrdinalIgnoreCase);

    public PlayerStore()
    {
        Seed();
    }

    private void Seed()
    {
        var subham = new Player
        {
            Username = "Subham",
            Level = 5,
            Experience = 120,
            Items =
            [
                new() { Name = "Iron Sword", Type = ItemType.Weapon, Power = 10 },
                new() { Name = "Health Potion", Type = ItemType.Potion, Power = 50 }
            ]
        };
        _players[subham.Username] = subham;

        var alex = new Player
        {
            Username = "Alex",
            Level = 3,
            Experience = 45,
            Items = [new() { Name = "Leather Boots", Type = ItemType.Armor, Power = 3 }]
        };
        _players[alex.Username] = alex;
    }

    public IReadOnlyList<Player> GetAll() => _players.Values.OrderBy(p => p.Username).ToList();

    public Player? GetByUsername(string username) =>
        _players.TryGetValue(username, out var p) ? p : null;

    public Player? AddItem(string username, string name, ItemType type, int power)
    {
        if (!_players.TryGetValue(username, out var player))
            return null;

        player.Items.Add(new Item { Name = name, Type = type, Power = power });
        return player;
    }

    public Player? AddExperience(string username, int amount)
    {
        if (!_players.TryGetValue(username, out var player) || amount <= 0)
            return null;

        player.Experience += amount;
        while (player.Experience >= ExperiencePerLevel(player.Level))
        {
            player.Experience -= ExperiencePerLevel(player.Level);
            player.Level++;
        }

        return player;
    }

    private static int ExperiencePerLevel(int level) => 100 + (level - 1) * 25;
}
