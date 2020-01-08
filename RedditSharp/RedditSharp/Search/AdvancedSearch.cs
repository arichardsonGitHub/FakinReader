namespace RedditSharp.Search
{
    public sealed class AdvancedSearchFilter
    {
        #region Constructors

        private AdvancedSearchFilter()
        {
        }
        #endregion Constructors

        #region Fields
        public string Author;
        public string Flair;
        public bool IsNsfw;
        public bool IsSelf;
        public string SelfText;
        public string Site;
        public string Subreddit;
        public string Title;
        public string Url;
        #endregion Fields
    }
}