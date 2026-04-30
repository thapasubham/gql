public interface IPlayerStore
{
    IReadOnlyList<Player> GetAll();

    Player? GetByUsername(string username);

    Player? AddItem(string username, string name, ItemType type, int power);

    Player? AddExperience(string username, int amount);
}
