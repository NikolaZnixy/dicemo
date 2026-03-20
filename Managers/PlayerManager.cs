using Data.Models;
using Data.Stores;

namespace Data.Managers;

public class PlayerManager
{
    private readonly UserStore _store;

    public PlayerManager(UserStore store)
    {
        _store = store;
    }

    public async Task<User?> GetPlayerAsync(int id) =>
        await _store.GetByIdAsync(id);

    public async Task<List<User>> GetAllPlayersAsync() =>
        await _store.GetAllActiveAsync();

    public async Task UpdatePlayerAsync(int id, UserModel model)
    {
        var user = await _store.GetByIdAsync(id);
        if (user is null) return;
        await UpdatePlayerAsync(model, user);
    }

    private async Task UpdatePlayerAsync(UserModel model, User user)
    {
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Bio = model.Bio;
        user.AvatarUrl = model.AvatarUrl;
        user.DateOfBirth = model.DateOfBirth;
        user.PreferredPosition = model.PreferredPosition;
        user.PreferredFoot = model.PreferredFoot;
        user.SelfReportedSkill = model.SelfReportedSkill;
        user.Neighborhood = model.Neighborhood;
        await _store.SaveAsync();
    }

    public async Task DeactivatePlayerAsync(int id)
    {
        var user = await _store.GetByIdAsync(id);
        if (user is null) return;
        user.IsActive = false;
        await _store.SaveAsync();
    }
}
