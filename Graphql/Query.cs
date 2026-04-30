using Gql.Model;
using Gql.Services;

namespace Gql.Graphql;

public class Query
{
    public async Task<IReadOnlyList<Player>> Players(
        [Service] IPlayerStore store,
        CancellationToken cancellationToken) =>
        await store.GetAllAsync(cancellationToken);

    public async Task<Player?> Player(
        string username,
        [Service] IPlayerStore store,
        CancellationToken cancellationToken) =>
        await store.GetByUsernameAsync(username, cancellationToken);
}
