using Data.Data;
using Data.Enums;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Stores;

public class MatchStore
{
    private readonly AppDbContext _context;

    public MatchStore(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Match?> GetByIdAsync(int id) =>
        await _context.Matches
            .Include(m => m.Owner)
            .Include(m => m.Pitch)
            .FirstOrDefaultAsync(m => m.Id == id);

    public async Task<List<Match>> GetOpenAsync() =>
        await _context.Matches
            .Include(m => m.Owner)
            .Include(m => m.Pitch)
            .Where(m => m.Status == MatchStatusEnum.Open)
            .OrderBy(m => m.ScheduledAt)
            .ToListAsync();

    public async Task<List<Match>> GetUpcomingAsync() =>
        await _context.Matches
            .Include(m => m.Owner)
            .Include(m => m.Pitch)
            .Where(m => m.ScheduledAt > DateTime.UtcNow &&
                        m.Status != MatchStatusEnum.Cancelled &&
                        m.Status != MatchStatusEnum.Completed)
            .OrderBy(m => m.ScheduledAt)
            .ToListAsync();

    public async Task<List<Match>> GetByUserAsync(int userId)
    {
        var playerMatchIds = await _context.MatchPlayers
            .Where(mp => mp.UserId == userId)
            .Select(mp => mp.MatchId)
            .ToListAsync();

        return await _context.Matches
            .Include(m => m.Owner)
            .Include(m => m.Pitch)
            .Where(m => m.OwnerUserId == userId || playerMatchIds.Contains(m.Id))
            .OrderByDescending(m => m.ScheduledAt)
            .ToListAsync();
    }

    public async Task<List<Match>> GetCompletedAsync() =>
        await _context.Matches
            .Include(m => m.Owner)
            .Include(m => m.Pitch)
            .Where(m => m.Status == MatchStatusEnum.Completed)
            .OrderByDescending(m => m.ScheduledAt)
            .ToListAsync();

    public async Task AddAsync(Match match) =>
        await _context.Matches.AddAsync(match);

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();
}
