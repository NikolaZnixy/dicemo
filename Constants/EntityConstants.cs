namespace Data.Constants;

public static class EntityConstants
{
    public static class User
    {
        public const int UsernameMaxLength = 30;
        public const int FirstNameMaxLength = 50;
        public const int LastNameMaxLength = 50;
        public const int BioMaxLength = 500;
        public const int AvatarUrlMaxLength = 500;
        public const int NeighborhoodMaxLength = 100;
    }

    public static class Pitch
    {
        public const int NameMaxLength = 100;
        public const int DescriptionMaxLength = 1000;
        public const int AddressMaxLength = 200;
        public const int ReservationLinkMaxLength = 500;
        public const int ReservationPhoneMaxLength = 20;
    }

    public static class Match
    {
        public const int RoomNameMaxLength = 100;
        public const int DescriptionMaxLength = 500;
        public const int RecurrenceRuleMaxLength = 100;
        public const int DefaultDurationMinutes = 90;
    }

    public static class ChatMessage
    {
        public const int ContentMaxLength = 1000;
    }

    public static class Notification
    {
        public const int TitleMaxLength = 100;
        public const int BodyMaxLength = 500;
        public const int LinkedEntityTypeMaxLength = 50;
    }

    public static class UserRating
    {
        public const int CommentMaxLength = 500;
        public const int MinScore = 1;
        public const int MaxScore = 5;
        public const int RatingWindowHours = 48;
    }

    public static class PitchReview
    {
        public const int CommentMaxLength = 500;
        public const int MinStars = 1;
        public const int MaxStars = 5;
    }

    public static class PitchPhoto
    {
        public const int UrlMaxLength = 500;
    }
}
