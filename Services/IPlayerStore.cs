using Gql.Model;

namespace Gql.Services;

public interface IPlayerStore
{
    Task<IReadOnlyList<Player>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Player?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

    Task<Player?> AddItemAsync(
        string username,
        string name,
        ItemType type,
        int power,
        CancellationToken cancellationToken = default);

    Task<Player?> AddExperienceAsync(string username, int amount, CancellationToken cancellationToken = default);
}
