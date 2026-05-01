using Gql.Model;
using Gql.Services;
using HotChocolate.Authorization;

namespace Gql.Graphql;

[Authorize]
public class Mutation
{
    public async Task<Player?> AddItem(
        string username,
        string name,
        ItemType type,
        int power,
        [Service] IPlayerStore store,
        CancellationToken cancellationToken) =>
        await store.AddItemAsync(username, name, type, power, cancellationToken);

    public async Task<Player?> AddExperience(
        string username,
        int amount,
        [Service] IPlayerStore store,
        CancellationToken cancellationToken) =>
        await store.AddExperienceAsync(username, amount, cancellationToken);
}
