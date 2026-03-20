using Data.Enums;

namespace Data.Models;

public class ChatMessage : BaseEntity
{
    public int MatchId { get; set; }
    public int SenderUserId { get; set; }
    public string Content { get; set; } = string.Empty;
    public MessageTypeEnum MessageType { get; set; }
    public DateTime SentAt { get; set; }

    public Match Match { get; set; } = null!;
    public User Sender { get; set; } = null!;
}
