using Data.Enums;

namespace Data.Models;

public class UserModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public PositionEnum PreferredPosition { get; set; }
    public FootEnum PreferredFoot { get; set; }
    public SkillLevelEnum SelfReportedSkill { get; set; }
    public string? Neighborhood { get; set; }
}
