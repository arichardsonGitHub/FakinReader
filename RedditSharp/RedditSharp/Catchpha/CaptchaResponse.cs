namespace RedditSharp
{
    /// <summary>
    /// Response for a captcha challenge.
    /// </summary>
    public class CaptchaResponse
    {
        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="answer"></param>
        public CaptchaResponse(string answer = null)
        {
            Answer = answer;
        }
        #endregion Constructors

        #region Fields

        /// <summary>
        /// Captcha answer.
        /// </summary>
        public readonly string Answer;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Set to true to cancel.
        /// </summary>
        public bool Cancel { get { return string.IsNullOrEmpty(Answer); } }

        #endregion Properties
    }
}