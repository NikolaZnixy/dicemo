namespace Data.Models;

public class UserRating : BaseEntity
{
    public int RaterUserId { get; set; }
    public int RatedUserId { get; set; }
    public int MatchId { get; set; }
    public int SkillScore { get; set; }
    public int SportsmanshipScore { get; set; }
    public int PunctualityScore { get; set; }
    public string? Comment { get; set; }

    public User Rater { get; set; } = null!;
    public User Rated { get; set; } = null!;
    public Match Match { get; set; } = null!;
}
