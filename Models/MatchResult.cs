namespace Data.Models;

public class MatchResult : BaseEntity
{
    public int MatchId { get; set; }
    public int? ScoreTeamA { get; set; }
    public int? ScoreTeamB { get; set; }
    public int? MvpUserId { get; set; }
    public DateTime CompletedAt { get; set; }
    public string? Notes { get; set; }

    public Match Match { get; set; } = null!;
    public User? Mvp { get; set; }
}
