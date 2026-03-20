using Data.Enums;
using Data.Models;
using Data.Stores;

namespace Data.Managers;

public class MatchManager
{
    private readonly MatchStore _store;

    public MatchManager(MatchStore store)
    {
        _store = store;
    }

    public async Task<Match?> GetMatchAsync(int id) =>
        await _store.GetByIdAsync(id);

    public async Task<List<Match>> GetOpenMatchesAsync() =>
        await _store.GetOpenAsync();

    public async Task<List<Match>> GetUpcomingMatchesAsync() =>
        await _store.GetUpcomingAsync();

    public async Task<List<Match>> GetUserMatchesAsync(int userId) =>
        await _store.GetByUserAsync(userId);

    public async Task<List<Match>> GetCompletedMatchesAsync() =>
        await _store.GetCompletedAsync();

    public async Task CreateMatchAsync(Match model)
    {
        model.Status = MatchStatusEnum.Open;
        model.CreatedAt = DateTime.UtcNow;
        await _store.AddAsync(model);
        await _store.SaveAsync();
    }

    public async Task UpdateMatchAsync(int id, Match model)
    {
        var match = await _store.GetByIdAsync(id);
        if (match is null) return;
        await UpdateMatchAsync(model, match);
    }

    private async Task UpdateMatchAsync(Match model, Match match)
    {
        match.RoomName = model.RoomName;
        match.Description = model.Description;
        match.PitchId = model.PitchId;
        match.ScheduledAt = model.ScheduledAt;
        match.DurationMinutes = model.DurationMinutes;
        match.MaxPlayers = model.MaxPlayers;
        match.MinPlayersToConfirm = model.MinPlayersToConfirm;
        match.SkillPreference = model.SkillPreference;
        match.IsPrivate = model.IsPrivate;
        match.CostPerPlayer = model.CostPerPlayer;
        match.JoinDeadline = model.JoinDeadline;
        match.IsRecurring = model.IsRecurring;
        match.RecurrenceRule = model.RecurrenceRule;
        await _store.SaveAsync();
    }

    public async Task CancelMatchAsync(int id)
    {
        var match = await _store.GetByIdAsync(id);
        if (match is null) return;
        match.Status = MatchStatusEnum.Cancelled;
        await _store.SaveAsync();
    }

    public async Task CompleteMatchAsync(int id)
    {
        var match = await _store.GetByIdAsync(id);
        if (match is null) return;
        match.Status = MatchStatusEnum.Completed;
        await _store.SaveAsync();
    }
}
