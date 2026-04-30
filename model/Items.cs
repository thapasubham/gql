using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gql.Model;

public class Item
{
    public required string Name { get; set; }

    [BsonRepresentation(BsonType.String)]
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
