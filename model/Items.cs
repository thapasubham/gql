public class Item
{
    public required string Name { get; set; }

    public ItemType Type { get; set; }

    public int Power { get; set; }
}
public enum ItemType
{
    Weapon,
    Armor,
    Potion,
    Misc
}
