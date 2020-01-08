namespace RedditSharp
{
    public class SpamFilterSettings
    {
        #region Constructors

        /// <summary>
        /// Creates a listing of the default filter lengths (all on high)
        /// </summary>
        public SpamFilterSettings()
        {
            LinkPostStrength = SpamFilterStrength.High;
            SelfPostStrength = SpamFilterStrength.High;
            CommentStrength = SpamFilterStrength.High;
        }
        #endregion Constructors

        #region Properties
        public SpamFilterStrength CommentStrength { get; set; }
        public SpamFilterStrength LinkPostStrength { get; set; }
        public SpamFilterStrength SelfPostStrength { get; set; }
        #endregion Properties
    }
}