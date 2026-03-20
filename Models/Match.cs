using Data.Enums;

namespace Data.Models;

public class Match : BaseEntity
{
    public string RoomName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int OwnerUserId { get; set; }
    public int PitchId { get; set; }
    public DateTime ScheduledAt { get; set; }
    public int DurationMinutes { get; set; } = 90;
    public int MaxPlayers { get; set; }
    public int MinPlayersToConfirm { get; set; }
    public SkillPrefEnum SkillPreference { get; set; }
    public bool IsPrivate { get; set; }
    public decimal? CostPerPlayer { get; set; }
    public DateTime? JoinDeadline { get; set; }
    public MatchStatusEnum Status { get; set; }
    public bool IsRecurring { get; set; }
    public string? RecurrenceRule { get; set; }

    public User Owner { get; set; } = null!;
    public Pitch Pitch { get; set; } = null!;
}
