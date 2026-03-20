using Data.Enums;
using Data.Models;
using Data.Stores;

namespace Data.Managers;

public class PitchManager
{
    private readonly PitchStore _store;

    public PitchManager(PitchStore store)
    {
        _store = store;
    }

    public async Task<Pitch?> GetPitchAsync(int id) =>
        await _store.GetByIdAsync(id);

    public async Task<List<Pitch>> GetAllPitchesAsync() =>
        await _store.GetAllAsync();

    public async Task<List<Pitch>> GetVerifiedPitchesAsync() =>
        await _store.GetVerifiedAsync();

    public async Task CreatePitchAsync(Pitch model, int submittedByUserId)
    {
        model.SubmittedByUserId = submittedByUserId;
        model.Status = PitchStatusEnum.Pending;
        model.CreatedAt = DateTime.UtcNow;
        await _store.AddAsync(model);
        await _store.SaveAsync();
    }

    public async Task UpdatePitchAsync(int id, Pitch model)
    {
        var pitch = await _store.GetByIdAsync(id);
        if (pitch is null) return;
        await UpdatePitchAsync(model, pitch);
    }

    private async Task UpdatePitchAsync(Pitch model, Pitch pitch)
    {
        pitch.Name = model.Name;
        pitch.Description = model.Description;
        pitch.Address = model.Address;
        pitch.Latitude = model.Latitude;
        pitch.Longitude = model.Longitude;
        pitch.Surface = model.Surface;
        pitch.Size = model.Size;
        pitch.HasLighting = model.HasLighting;
        pitch.HasParking = model.HasParking;
        pitch.HasChangingRooms = model.HasChangingRooms;
        pitch.IsFree = model.IsFree;
        pitch.CostPerHour = model.CostPerHour;
        pitch.OperatingHoursStart = model.OperatingHoursStart;
        pitch.OperatingHoursEnd = model.OperatingHoursEnd;
        pitch.ReservationLink = model.ReservationLink;
        pitch.ReservationPhone = model.ReservationPhone;
        await _store.SaveAsync();
    }

    public async Task VerifyPitchAsync(int id)
    {
        var pitch = await _store.GetByIdAsync(id);
        if (pitch is null) return;
        pitch.Status = PitchStatusEnum.Verified;
        await _store.SaveAsync();
    }

    public async Task RejectPitchAsync(int id)
    {
        var pitch = await _store.GetByIdAsync(id);
        if (pitch is null) return;
        pitch.Status = PitchStatusEnum.Rejected;
        await _store.SaveAsync();
    }
}
