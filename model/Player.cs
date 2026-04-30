public class Player
{
    public required string Username { get; set; }

    public int Level { get; set; }

    public int Experience { get; set; }

    public List<Item> Items { get; set; } = new();
}
