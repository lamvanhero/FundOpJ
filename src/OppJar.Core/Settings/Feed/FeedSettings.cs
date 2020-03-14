namespace OppJar.Core.Settings
{
    public class FeedSettings
    {
        internal class Verb
        {
            public const string POST = "post";
            public const string COMMENT = "comment";
            public const string LIKE = "like";
        }

        internal class FeedGroup
        {
            public const string TIMELINE = "timeline";
            public const string TIMELINE_AGGREGATED = "timeline_aggregated";
            public const string USER = "User";
            public const string NOTIFICATION = "notification";
        }
    }
}
