using Data.Enums;

namespace Data.Models;

public class Notification : BaseEntity
{
    public int RecipientUserId { get; set; }
    public NotifTypeEnum Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public int? LinkedEntityId { get; set; }
    public string? LinkedEntityType { get; set; }
    public bool IsRead { get; set; }

    public User Recipient { get; set; } = null!;
}
