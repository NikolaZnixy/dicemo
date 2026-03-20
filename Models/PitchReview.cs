namespace Data.Models;

public class PitchReview : BaseEntity
{
    public int PitchId { get; set; }
    public int UserId { get; set; }
    public int StarRating { get; set; }
    public string? Comment { get; set; }

    public Pitch Pitch { get; set; } = null!;
    public User User { get; set; } = null!;
}
