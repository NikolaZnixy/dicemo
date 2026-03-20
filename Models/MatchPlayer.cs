using Data.Enums;

namespace Data.Models;

public class MatchPlayer : BaseEntity
{
    public int MatchId { get; set; }
    public int UserId { get; set; }
    public DateTime JoinedAt { get; set; }
    public TeamEnum? Team { get; set; }
    public PlayerStatusEnum Status { get; set; }
    public bool WasNoShow { get; set; }

    public Match Match { get; set; } = null!;
    public User User { get; set; } = null!;
}
