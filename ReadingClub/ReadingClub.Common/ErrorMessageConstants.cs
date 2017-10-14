namespace ReadingClub.Common
{
    public class ErrorMessageConstants
    {
        public const string InvaliStringLengthErrorMessage = "The {0} must be between {2} and {1} characters long";
        public const string InvaliRangeValueErrorMessage = "The {0} must be between {1} and {2}";
        public const string InvalidEndDate = "End Date must be greater than Starting Date";
        public const string InvalidDifferenceBetweenStartAndEndTime = "The difference between Start and End Time must be at least 1 hour";
        public const string InvalidDiscussionStartDate = "The starting date must be at least 2 days from now";
    }
}
