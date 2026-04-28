public class Query
{
    public Player GetPlayer()
    {
        return new Player
        {
            Username = "Subham",
            Level = 5,
            Items =
            [
                new() { Name = "Iron Sword", Type = ItemType.Weapon, Power = 10 },
                new() { Name = "Health Potion", Type = ItemType.Potion, Power = 50 }
            ]
        };
    }
}
