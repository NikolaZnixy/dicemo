using Data.Enums;

namespace Data.Models;

public class Pitch : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public SurfaceEnum Surface { get; set; }
    public PitchSizeEnum Size { get; set; }
    public bool HasLighting { get; set; }
    public bool HasParking { get; set; }
    public bool HasChangingRooms { get; set; }
    public decimal? CostPerHour { get; set; }
    public bool IsFree { get; set; }
    public TimeOnly? OperatingHoursStart { get; set; }
    public TimeOnly? OperatingHoursEnd { get; set; }
    public string? ReservationLink { get; set; }
    public string? ReservationPhone { get; set; }
    public PitchStatusEnum Status { get; set; }
    public int MatchesPlayedCount { get; set; }
    public int SubmittedByUserId { get; set; }

    public User SubmittedBy { get; set; } = null!;
}
