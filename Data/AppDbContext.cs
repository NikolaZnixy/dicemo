using Data.Constants;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pitch> Pitches => Set<Pitch>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<MatchPlayer> MatchPlayers => Set<MatchPlayer>();
    public DbSet<MatchResult> MatchResults => Set<MatchResult>();
    public DbSet<UserRating> UserRatings => Set<UserRating>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<PitchPhoto> PitchPhotos => Set<PitchPhoto>();
    public DbSet<PitchReview> PitchReviews => Set<PitchReview>();
    public DbSet<UserAvailability> UserAvailabilities => Set<UserAvailability>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConfigureUser(builder);
        ConfigurePitch(builder);
        ConfigureMatch(builder);
        ConfigureMatchPlayer(builder);
        ConfigureMatchResult(builder);
        ConfigureUserRating(builder);
        ConfigureChatMessage(builder);
        ConfigureNotification(builder);
        ConfigurePitchPhoto(builder);
        ConfigurePitchReview(builder);
        ConfigureUserAvailability(builder);
    }

    private static void ConfigureUser(ModelBuilder builder)
    {
        builder.Entity<User>(e =>
        {
            e.Property(u => u.UserName)
                .HasMaxLength(EntityConstants.User.UsernameMaxLength);

            e.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(EntityConstants.User.FirstNameMaxLength);

            e.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(EntityConstants.User.LastNameMaxLength);

            e.Property(u => u.Bio)
                .HasMaxLength(EntityConstants.User.BioMaxLength);

            e.Property(u => u.AvatarUrl)
                .HasMaxLength(EntityConstants.User.AvatarUrlMaxLength);

            e.Property(u => u.Neighborhood)
                .HasMaxLength(EntityConstants.User.NeighborhoodMaxLength);

            e.Property(u => u.CreatedAt)
                .IsRequired();

            e.HasIndex(u => u.Email)
                .IsUnique();
        });
    }

    private static void ConfigurePitch(ModelBuilder builder)
    {
        builder.Entity<Pitch>(e =>
        {
            e.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(EntityConstants.Pitch.NameMaxLength);

            e.Property(p => p.Description)
                .HasMaxLength(EntityConstants.Pitch.DescriptionMaxLength);

            e.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(EntityConstants.Pitch.AddressMaxLength);

            e.Property(p => p.ReservationLink)
                .HasMaxLength(EntityConstants.Pitch.ReservationLinkMaxLength);

            e.Property(p => p.ReservationPhone)
                .HasMaxLength(EntityConstants.Pitch.ReservationPhoneMaxLength);

            e.HasOne(p => p.SubmittedBy)
                .WithMany()
                .HasForeignKey(p => p.SubmittedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(p => new { p.Latitude, p.Longitude });
        });
    }

    private static void ConfigureMatch(ModelBuilder builder)
    {
        builder.Entity<Match>(e =>
        {
            e.Property(m => m.RoomName)
                .IsRequired()
                .HasMaxLength(EntityConstants.Match.RoomNameMaxLength);

            e.Property(m => m.Description)
                .HasMaxLength(EntityConstants.Match.DescriptionMaxLength);

            e.Property(m => m.RecurrenceRule)
                .HasMaxLength(EntityConstants.Match.RecurrenceRuleMaxLength);

            e.Property(m => m.DurationMinutes)
                .HasDefaultValue(EntityConstants.Match.DefaultDurationMinutes);

            e.HasOne(m => m.Owner)
                .WithMany()
                .HasForeignKey(m => m.OwnerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(m => m.Pitch)
                .WithMany()
                .HasForeignKey(m => m.PitchId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(m => m.ScheduledAt);
            e.HasIndex(m => m.Status);
        });
    }

    private static void ConfigureMatchPlayer(ModelBuilder builder)
    {
        builder.Entity<MatchPlayer>(e =>
        {
            // A user can only appear once per match
            e.HasIndex(mp => new { mp.MatchId, mp.UserId })
                .IsUnique();

            e.HasOne(mp => mp.Match)
                .WithMany()
                .HasForeignKey(mp => mp.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(mp => mp.User)
                .WithMany()
                .HasForeignKey(mp => mp.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureMatchResult(ModelBuilder builder)
    {
        builder.Entity<MatchResult>(e =>
        {
            // One result per match
            e.HasIndex(mr => mr.MatchId)
                .IsUnique();

            e.HasOne(mr => mr.Match)
                .WithMany()
                .HasForeignKey(mr => mr.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(mr => mr.Mvp)
                .WithMany()
                .HasForeignKey(mr => mr.MvpUserId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }

    private static void ConfigureUserRating(ModelBuilder builder)
    {
        builder.Entity<UserRating>(e =>
        {
            e.Property(r => r.Comment)
                .HasMaxLength(EntityConstants.UserRating.CommentMaxLength);

            // A rater can only rate a specific player once per match
            e.HasIndex(r => new { r.RaterUserId, r.RatedUserId, r.MatchId })
                .IsUnique();

            e.HasOne(r => r.Rater)
                .WithMany()
                .HasForeignKey(r => r.RaterUserId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(r => r.Rated)
                .WithMany()
                .HasForeignKey(r => r.RatedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(r => r.Match)
                .WithMany()
                .HasForeignKey(r => r.MatchId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureChatMessage(ModelBuilder builder)
    {
        builder.Entity<ChatMessage>(e =>
        {
            e.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(EntityConstants.ChatMessage.ContentMaxLength);

            e.HasOne(c => c.Match)
                .WithMany()
                .HasForeignKey(c => c.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(c => c.Sender)
                .WithMany()
                .HasForeignKey(c => c.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(c => c.MatchId);
            e.HasIndex(c => c.SentAt);
        });
    }

    private static void ConfigureNotification(ModelBuilder builder)
    {
        builder.Entity<Notification>(e =>
        {
            e.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(EntityConstants.Notification.TitleMaxLength);

            e.Property(n => n.Body)
                .IsRequired()
                .HasMaxLength(EntityConstants.Notification.BodyMaxLength);

            e.Property(n => n.LinkedEntityType)
                .HasMaxLength(EntityConstants.Notification.LinkedEntityTypeMaxLength);

            e.HasOne(n => n.Recipient)
                .WithMany()
                .HasForeignKey(n => n.RecipientUserId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(n => new { n.RecipientUserId, n.IsRead });
        });
    }

    private static void ConfigurePitchPhoto(ModelBuilder builder)
    {
        builder.Entity<PitchPhoto>(e =>
        {
            e.Property(p => p.Url)
                .IsRequired()
                .HasMaxLength(EntityConstants.PitchPhoto.UrlMaxLength);

            e.HasOne(p => p.Pitch)
                .WithMany()
                .HasForeignKey(p => p.PitchId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(p => p.UploadedBy)
                .WithMany()
                .HasForeignKey(p => p.UploadedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigurePitchReview(ModelBuilder builder)
    {
        builder.Entity<PitchReview>(e =>
        {
            e.Property(r => r.Comment)
                .HasMaxLength(EntityConstants.PitchReview.CommentMaxLength);

            // One review per user per pitch
            e.HasIndex(r => new { r.PitchId, r.UserId })
                .IsUnique();

            e.HasOne(r => r.Pitch)
                .WithMany()
                .HasForeignKey(r => r.PitchId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureUserAvailability(ModelBuilder builder)
    {
        builder.Entity<UserAvailability>(e =>
        {
            e.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(a => new { a.UserId, a.DayOfWeek });
        });
    }
}
