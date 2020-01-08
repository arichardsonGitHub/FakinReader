namespace RedditSharp
{
    internal class TextData : SubmitData
    {
        #region Constructors

        internal TextData()
        {
            Kind = "self";
        }
        #endregion Constructors

        #region Properties

        /// <summary>
        /// Markdown text of the self post.
        /// </summary>
        [RedditAPIName("text")]
        internal string Text { get; set; }

        #endregion Properties
    }
}