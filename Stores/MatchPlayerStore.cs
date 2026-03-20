using Data.Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Stores;

public class MatchPlayerStore
{
    private readonly AppDbContext _context;

    public MatchPlayerStore(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MatchPlayer>> GetByMatchAsync(int matchId) =>
        await _context.MatchPlayers
            .Include(mp => mp.User)
            .Where(mp => mp.MatchId == matchId)
            .ToListAsync();

    public async Task<List<MatchPlayer>> GetByUserAsync(int userId) =>
        await _context.MatchPlayers
            .Include(mp => mp.Match)
            .Where(mp => mp.UserId == userId)
            .ToListAsync();

    public async Task<MatchPlayer?> GetAsync(int matchId, int userId) =>
        await _context.MatchPlayers
            .FirstOrDefaultAsync(mp => mp.MatchId == matchId && mp.UserId == userId);

    public async Task AddAsync(MatchPlayer matchPlayer) =>
        await _context.MatchPlayers.AddAsync(matchPlayer);

    public void Remove(MatchPlayer matchPlayer) =>
        _context.MatchPlayers.Remove(matchPlayer);

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();
}
