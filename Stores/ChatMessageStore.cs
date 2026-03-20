using Data.Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Stores;

public class ChatMessageStore
{
    private readonly AppDbContext _context;

    public ChatMessageStore(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ChatMessage>> GetByMatchAsync(int matchId) =>
        await _context.ChatMessages
            .Include(c => c.Sender)
            .Where(c => c.MatchId == matchId)
            .OrderBy(c => c.SentAt)
            .ToListAsync();

    public async Task AddAsync(ChatMessage message) =>
        await _context.ChatMessages.AddAsync(message);

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();
}
