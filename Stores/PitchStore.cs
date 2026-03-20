using Data.Data;
using Data.Enums;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Stores;

public class PitchStore
{
    private readonly AppDbContext _context;

    public PitchStore(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pitch?> GetByIdAsync(int id) =>
        await _context.Pitches
            .Include(p => p.SubmittedBy)
            .Include(p => p.Photos)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<Pitch>> GetAllAsync() =>
        await _context.Pitches
            .Include(p => p.SubmittedBy)
            .Include(p => p.Photos)
            .OrderBy(p => p.Name)
            .ToListAsync();

    public async Task<List<Pitch>> GetVerifiedAsync() =>
        await _context.Pitches
            .Include(p => p.SubmittedBy)
            .Include(p => p.Photos)
            .Where(p => p.Status == PitchStatusEnum.Verified)
            .OrderBy(p => p.Name)
            .ToListAsync();

    public async Task AddAsync(Pitch pitch) =>
        await _context.Pitches.AddAsync(pitch);

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();
}
