using Data.Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Stores;

public class NotificationStore
{
    private readonly AppDbContext _context;

    public NotificationStore(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Notification>> GetByUserAsync(int userId) =>
        await _context.Notifications
            .Where(n => n.RecipientUserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

    public async Task<List<Notification>> GetUnreadByUserAsync(int userId) =>
        await _context.Notifications
            .Where(n => n.RecipientUserId == userId && !n.IsRead)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

    public async Task<int> GetUnreadCountAsync(int userId) =>
        await _context.Notifications
            .CountAsync(n => n.RecipientUserId == userId && !n.IsRead);

    public async Task<Notification?> GetByIdAsync(int id) =>
        await _context.Notifications.FindAsync(id);

    public async Task AddAsync(Notification notification) =>
        await _context.Notifications.AddAsync(notification);

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();
}
