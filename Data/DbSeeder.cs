using Data.Enums;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context, UserManager<User> userManager)
    {
        // Only seed if the database is empty
        if (await userManager.Users.AnyAsync())
            return;

        // ---------------------------------------------------------------
        // Users
        // ---------------------------------------------------------------
        var users = new List<(User user, string password)>
        {
            (new User
            {
                UserName = "luka.horvat",
                Email = "luka.horvat@dicemo.hr",
                FirstName = "Luka",
                LastName = "Horvat",
                Bio = "Veznjak koji voli kratke pasove. Uvijek spreman za utakmicu!",
                PreferredPosition = PositionEnum.MID,
                PreferredFoot = FootEnum.Right,
                SelfReportedSkill = SkillLevelEnum.Intermediate,
                Neighborhood = "Zagreb Centar",
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                EmailConfirmed = true
            }, "Luka1234!"),

            (new User
            {
                UserName = "marko.peric",
                Email = "marko.peric@dicemo.hr",
                FirstName = "Marko",
                LastName = "Perić",
                Bio = "Napadač, gol-mašina. Ne propuštam prilike.",
                PreferredPosition = PositionEnum.FWD,
                PreferredFoot = FootEnum.Left,
                SelfReportedSkill = SkillLevelEnum.Advanced,
                Neighborhood = "Trešnjevka",
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                EmailConfirmed = true
            }, "Marko1234!"),

            (new User
            {
                UserName = "ivan.kovac",
                Email = "ivan.kovac@dicemo.hr",
                FirstName = "Ivan",
                LastName = "Kovač",
                Bio = "Branič koji drži čvrstu obranu. Tek počinjem ali učim brzo.",
                PreferredPosition = PositionEnum.DEF,
                PreferredFoot = FootEnum.Right,
                SelfReportedSkill = SkillLevelEnum.Beginner,
                Neighborhood = "Maksimir",
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                EmailConfirmed = true
            }, "Ivan1234!"),

            (new User
            {
                UserName = "ana.blazic",
                Email = "ana.blazic@dicemo.hr",
                FirstName = "Ana",
                LastName = "Blažić",
                Bio = "Golmanica. Bez obrane nema pobjede!",
                PreferredPosition = PositionEnum.GK,
                PreferredFoot = FootEnum.Right,
                SelfReportedSkill = SkillLevelEnum.Intermediate,
                Neighborhood = "Novi Zagreb",
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                EmailConfirmed = true
            }, "Ana12345!"),

            (new User
            {
                UserName = "tomislav.juric",
                Email = "tomislav.juric@dicemo.hr",
                FirstName = "Tomislav",
                LastName = "Jurić",
                Bio = "Iskusni veznjak, volim organizirati igru i kontrolirati tempo.",
                PreferredPosition = PositionEnum.MID,
                PreferredFoot = FootEnum.Both,
                SelfReportedSkill = SkillLevelEnum.Advanced,
                Neighborhood = "Gornji Grad",
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                EmailConfirmed = true
            }, "Tomo1234!"),

            (new User
            {
                UserName = "petra.simic",
                Email = "petra.simic@dicemo.hr",
                FirstName = "Petra",
                LastName = "Šimić",
                Bio = "Napadačica koja voli dribling i brzinu. Uvijek dobro raspoložena!",
                PreferredPosition = PositionEnum.FWD,
                PreferredFoot = FootEnum.Right,
                SelfReportedSkill = SkillLevelEnum.Beginner,
                Neighborhood = "Črnomerec",
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                EmailConfirmed = true
            }, "Petra123!")
        };

        var createdUsers = new List<User>();
        foreach (var (user, password) in users)
        {
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
                createdUsers.Add(user);
        }

        var luka = createdUsers[0];
        var marko = createdUsers[1];
        var ivan = createdUsers[2];
        var ana = createdUsers[3];
        var tomislav = createdUsers[4];
        var petra = createdUsers[5];

        // ---------------------------------------------------------------
        // Pitches
        // ---------------------------------------------------------------
        var salata = new Pitch
        {
            Name = "Šalata – Mali teren",
            Description = "Legendarni teren na Šalati, asfalt ali s dušom. Idealno za brzu 5v5 igru.",
            Address = "Šalata 1, Zagreb",
            Latitude = 45.8195,
            Longitude = 15.9819,
            Surface = SurfaceEnum.Concrete,
            Size = PitchSizeEnum.FiveVFive,
            HasLighting = true,
            HasParking = false,
            HasChangingRooms = false,
            IsFree = true,
            OperatingHoursStart = new TimeOnly(8, 0),
            OperatingHoursEnd = new TimeOnly(22, 0),
            Status = PitchStatusEnum.Verified,
            SubmittedByUserId = luka.Id,
            CreatedAt = DateTime.UtcNow
        };

        var spansko = new Pitch
        {
            Name = "Špansko – Gradsko Igralište",
            Description = "Lijepo uređen travnjak s rasvjetom i svlačionicama. Idealno za 7v7.",
            Address = "Ulica Špansko 12, Zagreb",
            Latitude = 45.7989,
            Longitude = 15.8892,
            Surface = SurfaceEnum.Grass,
            Size = PitchSizeEnum.SevenVSeven,
            HasLighting = true,
            HasParking = true,
            HasChangingRooms = true,
            IsFree = false,
            CostPerHour = 40.00m,
            OperatingHoursStart = new TimeOnly(9, 0),
            OperatingHoursEnd = new TimeOnly(21, 0),
            ReservationPhone = "+385 1 234 5678",
            Status = PitchStatusEnum.Verified,
            SubmittedByUserId = marko.Id,
            CreatedAt = DateTime.UtcNow
        };

        var siget = new Pitch
        {
            Name = "Siget – Dvorana",
            Description = "Zatvorena dvorana s umjetnom travom. Savršena za zimske utakmice.",
            Address = "Sigetska 3, Zagreb",
            Latitude = 45.7876,
            Longitude = 15.9234,
            Surface = SurfaceEnum.Indoor,
            Size = PitchSizeEnum.FiveVFive,
            HasLighting = true,
            HasParking = true,
            HasChangingRooms = true,
            IsFree = false,
            CostPerHour = 60.00m,
            OperatingHoursStart = new TimeOnly(7, 0),
            OperatingHoursEnd = new TimeOnly(23, 0),
            ReservationLink = "https://siget-dvorana.hr/rezervacija",
            ReservationPhone = "+385 1 987 6543",
            Status = PitchStatusEnum.Verified,
            SubmittedByUserId = tomislav.Id,
            CreatedAt = DateTime.UtcNow
        };

        context.Pitches.AddRange(salata, spansko, siget);
        await context.SaveChangesAsync();

        // ---------------------------------------------------------------
        // Matches
        // ---------------------------------------------------------------
        var openMatch = new Match
        {
            RoomName = "Večernja peta na Šalati",
            Description = "Opuštena igra, svi su dobrodošli. Donosimo loptu.",
            OwnerUserId = luka.Id,
            PitchId = salata.Id,
            ScheduledAt = DateTime.UtcNow.AddDays(2).Date.AddHours(19),
            DurationMinutes = 60,
            MaxPlayers = 10,
            MinPlayersToConfirm = 6,
            SkillPreference = SkillPrefEnum.Casual,
            IsPrivate = false,
            Status = MatchStatusEnum.Open,
            IsRecurring = false,
            CreatedAt = DateTime.UtcNow
        };

        var confirmedMatch = new Match
        {
            RoomName = "Subotnja sedmica – Špansko",
            Description = "Kompetitivna igra, tražimo igrače srednje do napredne razine.",
            OwnerUserId = tomislav.Id,
            PitchId = spansko.Id,
            ScheduledAt = DateTime.UtcNow.AddDays(4).Date.AddHours(10),
            DurationMinutes = 90,
            MaxPlayers = 14,
            MinPlayersToConfirm = 10,
            SkillPreference = SkillPrefEnum.Competitive,
            IsPrivate = false,
            CostPerPlayer = 6.00m,
            Status = MatchStatusEnum.Confirmed,
            IsRecurring = true,
            RecurrenceRule = "FREQ=WEEKLY;BYDAY=SA",
            CreatedAt = DateTime.UtcNow
        };

        var completedMatch = new Match
        {
            RoomName = "Zimska peta – Siget",
            Description = "Zimska liga, dvorana.",
            OwnerUserId = marko.Id,
            PitchId = siget.Id,
            ScheduledAt = DateTime.UtcNow.AddDays(-3).Date.AddHours(18),
            DurationMinutes = 60,
            MaxPlayers = 10,
            MinPlayersToConfirm = 6,
            SkillPreference = SkillPrefEnum.Any,
            IsPrivate = false,
            CostPerPlayer = 8.00m,
            Status = MatchStatusEnum.Completed,
            IsRecurring = false,
            CreatedAt = DateTime.UtcNow.AddDays(-5)
        };

        context.Matches.AddRange(openMatch, confirmedMatch, completedMatch);
        await context.SaveChangesAsync();

        // ---------------------------------------------------------------
        // Match Players
        // ---------------------------------------------------------------
        var matchPlayers = new List<MatchPlayer>
        {
            // Open match – Luka (owner) + Ana + Ivan
            new() { MatchId = openMatch.Id, UserId = luka.Id, JoinedAt = DateTime.UtcNow.AddHours(-2), Status = PlayerStatusEnum.Joined, CreatedAt = DateTime.UtcNow },
            new() { MatchId = openMatch.Id, UserId = ana.Id, JoinedAt = DateTime.UtcNow.AddHours(-1), Status = PlayerStatusEnum.Joined, CreatedAt = DateTime.UtcNow },
            new() { MatchId = openMatch.Id, UserId = ivan.Id, JoinedAt = DateTime.UtcNow.AddMinutes(-30), Status = PlayerStatusEnum.Joined, CreatedAt = DateTime.UtcNow },

            // Confirmed match – Tomislav (owner) + Marko + Luka + Petra + Ana
            new() { MatchId = confirmedMatch.Id, UserId = tomislav.Id, JoinedAt = DateTime.UtcNow.AddDays(-1), Status = PlayerStatusEnum.Joined, CreatedAt = DateTime.UtcNow },
            new() { MatchId = confirmedMatch.Id, UserId = marko.Id, JoinedAt = DateTime.UtcNow.AddDays(-1), Status = PlayerStatusEnum.Joined, CreatedAt = DateTime.UtcNow },
            new() { MatchId = confirmedMatch.Id, UserId = luka.Id, JoinedAt = DateTime.UtcNow.AddDays(-1), Status = PlayerStatusEnum.Joined, CreatedAt = DateTime.UtcNow },
            new() { MatchId = confirmedMatch.Id, UserId = petra.Id, JoinedAt = DateTime.UtcNow.AddHours(-20), Status = PlayerStatusEnum.Joined, CreatedAt = DateTime.UtcNow },
            new() { MatchId = confirmedMatch.Id, UserId = ana.Id, JoinedAt = DateTime.UtcNow.AddHours(-18), Status = PlayerStatusEnum.Joined, CreatedAt = DateTime.UtcNow },

            // Completed match – Marko + Tomislav + Ivan + Petra + Luka (teams assigned)
            new() { MatchId = completedMatch.Id, UserId = marko.Id, JoinedAt = DateTime.UtcNow.AddDays(-5), Team = TeamEnum.A, Status = PlayerStatusEnum.Joined, CreatedAt = DateTime.UtcNow },
            new() { MatchId = completedMatch.Id, UserId = tomislav.Id, JoinedAt = DateTime.UtcNow.AddDays(-5), Team = TeamEnum.A, Status = PlayerStatusEnum.Joined, CreatedAt = DateTime.UtcNow },
            new() { MatchId = completedMatch.Id, UserId = ivan.Id, JoinedAt = DateTime.UtcNow.AddDays(-5), Team = TeamEnum.B, Status = PlayerStatusEnum.Joined, CreatedAt = DateTime.UtcNow },
            new() { MatchId = completedMatch.Id, UserId = petra.Id, JoinedAt = DateTime.UtcNow.AddDays(-5), Team = TeamEnum.B, Status = PlayerStatusEnum.Joined, CreatedAt = DateTime.UtcNow },
            new() { MatchId = completedMatch.Id, UserId = luka.Id, JoinedAt = DateTime.UtcNow.AddDays(-5), Team = TeamEnum.A, Status = PlayerStatusEnum.Joined, CreatedAt = DateTime.UtcNow },
        };

        context.MatchPlayers.AddRange(matchPlayers);
        await context.SaveChangesAsync();

        // ---------------------------------------------------------------
        // Match Result (completed match)
        // ---------------------------------------------------------------
        var matchResult = new MatchResult
        {
            MatchId = completedMatch.Id,
            ScoreTeamA = 4,
            ScoreTeamB = 3,
            MvpUserId = marko.Id,
            CompletedAt = completedMatch.ScheduledAt.AddMinutes(completedMatch.DurationMinutes),
            Notes = "Odlična utakmica, dramatičan kraj!",
            CreatedAt = DateTime.UtcNow
        };

        context.MatchResults.Add(matchResult);
        await context.SaveChangesAsync();

        // ---------------------------------------------------------------
        // Chat Messages (open match room)
        // ---------------------------------------------------------------
        var chatMessages = new List<ChatMessage>
        {
            new() { MatchId = openMatch.Id, SenderUserId = luka.Id, Content = "Hej ekipa, večeras 19h na Šalati! Tko dolazi?", MessageType = MessageTypeEnum.Text, SentAt = DateTime.UtcNow.AddHours(-2), CreatedAt = DateTime.UtcNow },
            new() { MatchId = openMatch.Id, SenderUserId = ana.Id, Content = "Ja sam unutra! 👍", MessageType = MessageTypeEnum.Text, SentAt = DateTime.UtcNow.AddHours(-1).AddMinutes(-50), CreatedAt = DateTime.UtcNow },
            new() { MatchId = openMatch.Id, SenderUserId = ivan.Id, Content = "I ja dolazim, prvi put igram na Šalati.", MessageType = MessageTypeEnum.Text, SentAt = DateTime.UtcNow.AddMinutes(-45), CreatedAt = DateTime.UtcNow },
            new() { MatchId = openMatch.Id, SenderUserId = luka.Id, Content = "Super! Još trebamo 3-4 igrača. Pozovite prijatelje!", MessageType = MessageTypeEnum.Text, SentAt = DateTime.UtcNow.AddMinutes(-30), CreatedAt = DateTime.UtcNow },
            new() { MatchId = openMatch.Id, SenderUserId = ana.Id, Content = "Nosim rezervnu loptu za svaki slučaj 😄", MessageType = MessageTypeEnum.Text, SentAt = DateTime.UtcNow.AddMinutes(-10), CreatedAt = DateTime.UtcNow },
        };

        context.ChatMessages.AddRange(chatMessages);
        await context.SaveChangesAsync();

        // ---------------------------------------------------------------
        // Notifications
        // ---------------------------------------------------------------
        var notifications = new List<Notification>
        {
            new() { RecipientUserId = luka.Id, Type = NotifTypeEnum.PlayerJoined, Title = "Ana se pridružila!", Body = "Ana Blažić se pridružila tvojoj utakmici 'Večernja peta na Šalati'.", LinkedEntityId = openMatch.Id, LinkedEntityType = "Match", IsRead = false, CreatedAt = DateTime.UtcNow.AddHours(-1) },
            new() { RecipientUserId = luka.Id, Type = NotifTypeEnum.PlayerJoined, Title = "Ivan se pridružio!", Body = "Ivan Kovač se pridružio tvojoj utakmici 'Večernja peta na Šalati'.", LinkedEntityId = openMatch.Id, LinkedEntityType = "Match", IsRead = false, CreatedAt = DateTime.UtcNow.AddMinutes(-30) },
            new() { RecipientUserId = tomislav.Id, Type = NotifTypeEnum.MatchConfirmed, Title = "Utakmica potvrđena!", Body = "Subotnja sedmica na Španskom je potvrđena – dovoljno igrača se prijavilo.", LinkedEntityId = confirmedMatch.Id, LinkedEntityType = "Match", IsRead = true, CreatedAt = DateTime.UtcNow.AddHours(-5) },
            new() { RecipientUserId = marko.Id, Type = NotifTypeEnum.RatingReceived, Title = "Nova ocjena!", Body = "Dobio si novu ocjenu nakon utakmice 'Zimska peta – Siget'.", LinkedEntityId = completedMatch.Id, LinkedEntityType = "Match", IsRead = false, CreatedAt = DateTime.UtcNow.AddDays(-2) },
            new() { RecipientUserId = ivan.Id, Type = NotifTypeEnum.MatchReminder, Title = "Utakmica za 2 dana!", Body = "Podsjetnik: 'Večernja peta na Šalati' je za 2 dana u 19:00.", LinkedEntityId = openMatch.Id, LinkedEntityType = "Match", IsRead = false, CreatedAt = DateTime.UtcNow.AddMinutes(-15) },
        };

        context.Notifications.AddRange(notifications);
        await context.SaveChangesAsync();
    }
}
