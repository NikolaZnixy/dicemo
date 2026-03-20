using Data.Models;
using Data.Stores;

namespace Data.Managers;

public class NotificationManager
{
    private readonly NotificationStore _store;

    public NotificationManager(NotificationStore store)
    {
        _store = store;
    }

    public async Task<List<Notification>> GetUserNotificationsAsync(int userId) =>
        await _store.GetByUserAsync(userId);

    public async Task<int> GetUnreadCountAsync(int userId) =>
        await _store.GetUnreadCountAsync(userId);

    public async Task CreateNotificationAsync(Notification model)
    {
        model.CreatedAt = DateTime.UtcNow;
        model.IsRead = false;
        await _store.AddAsync(model);
        await _store.SaveAsync();
    }

    public async Task MarkAsReadAsync(int notificationId)
    {
        var notification = await _store.GetByIdAsync(notificationId);
        if (notification is null) return;
        notification.IsRead = true;
        await _store.SaveAsync();
    }

    public async Task MarkAllAsReadAsync(int userId)
    {
        var unread = await _store.GetUnreadByUserAsync(userId);
        foreach (var notification in unread)
            notification.IsRead = true;
        await _store.SaveAsync();
    }
}
