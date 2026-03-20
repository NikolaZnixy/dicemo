using Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace Data.Models;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string? Bio { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public PositionEnum PreferredPosition { get; set; }
    public FootEnum PreferredFoot { get; set; }
    public SkillLevelEnum SelfReportedSkill { get; set; }
    public string? Neighborhood { get; set; }
    public int MatchesPlayed { get; set; }
    public int NoShowCount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}
