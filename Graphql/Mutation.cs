public class Mutation
{
    public Player? AddItem(
        string username,
        string name,
        ItemType type,
        int power,
        [Service] IPlayerStore store) =>
        store.AddItem(username, name, type, power);

    public Player? AddExperience(string username, int amount, [Service] IPlayerStore store) =>
        store.AddExperience(username, amount);
}
