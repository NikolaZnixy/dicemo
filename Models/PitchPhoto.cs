namespace Data.Models;

public class PitchPhoto
{
    public int Id { get; set; }
    public int PitchId { get; set; }
    public int UploadedByUserId { get; set; }
    public string Url { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }

    public Pitch Pitch { get; set; } = null!;
    public User UploadedBy { get; set; } = null!;
}
