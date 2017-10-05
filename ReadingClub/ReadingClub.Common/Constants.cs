namespace ReadingClub.Common
{
    public class Constants
    {
        // String lengths
        public const int MinAuthorNameLength = 2;
        public const int MaxAuthorNameLength = 100;
        public const int MinBookTitleLength = 2;
        public const int MaxBookTitleLength = 100;
        public const int MinBookDescriptionLength = 10;
        public const int MaxBookDescriptionLength = 300;
        public const int MinCommentContentLength = 10;
        public const int MaxCommentContentLength = 300;
        public const int MinDiscussionSubjectLength = 10;
        public const int MaxDiscussionSubjectLength = 150;

        // int values

        public const int MinNumberOfParticipants = 3;
        public const int MaxNumberOfParticipants = 30;

        // controllers constants

        public const string DiscussionStatusUpcoming = "upcoming";
        public const string DiscussionStatusCurrent = "current";
    }
}
