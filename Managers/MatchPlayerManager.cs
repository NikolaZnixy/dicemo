using Data.Enums;
using Data.Models;
using Data.Stores;

namespace Data.Managers;

public class MatchPlayerManager
{
    private readonly MatchPlayerStore _store;

    public MatchPlayerManager(MatchPlayerStore store)
    {
        _store = store;
    }

    public async Task<List<MatchPlayer>> GetMatchPlayersAsync(int matchId) =>
        await _store.GetByMatchAsync(matchId);

    public async Task JoinMatchAsync(int matchId, int userId)
    {
        var existing = await _store.GetAsync(matchId, userId);
        if (existing is not null) return;

        var matchPlayer = new MatchPlayer
        {
            MatchId = matchId,
            UserId = userId,
            JoinedAt = DateTime.UtcNow,
            Status = PlayerStatusEnum.Joined,
            CreatedAt = DateTime.UtcNow
        };

        await _store.AddAsync(matchPlayer);
        await _store.SaveAsync();
    }

    public async Task LeaveMatchAsync(int matchId, int userId)
    {
        var matchPlayer = await _store.GetAsync(matchId, userId);
        if (matchPlayer is null) return;
        _store.Remove(matchPlayer);
        await _store.SaveAsync();
    }

    public async Task UpdatePlayerStatusAsync(int matchId, int userId, PlayerStatusEnum status)
    {
        var matchPlayer = await _store.GetAsync(matchId, userId);
        if (matchPlayer is null) return;
        await UpdatePlayerStatusAsync(status, matchPlayer);
    }

    private async Task UpdatePlayerStatusAsync(PlayerStatusEnum status, MatchPlayer matchPlayer)
    {
        matchPlayer.Status = status;
        await _store.SaveAsync();
    }
}
