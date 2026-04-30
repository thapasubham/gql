public class Query
{
    public IReadOnlyList<Player> Players([Service] IPlayerStore store) => store.GetAll();

    public Player? Player(string username, [Service] IPlayerStore store) =>
        store.GetByUsername(username);
}
