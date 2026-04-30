using MongoDB.Bson.Serialization.Attributes;

namespace Gql.Model;

public class Player
{
    [BsonId]
    public required string Username { get; set; }

    public int Level { get; set; }

    public int Experience { get; set; }

    public List<Item> Items { get; set; } = new();
}
