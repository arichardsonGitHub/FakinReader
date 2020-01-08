namespace RedditSharp
{
    /// <summary>
    /// A captcha solver.
    /// </summary>
    public interface ICaptchaSolver
    {
        #region Methods

        /// <summary>
        /// A method to handle the captcha.
        /// </summary>
        /// <param name="captcha"></param>
        /// <returns></returns>
        CaptchaResponse HandleCaptcha(Captcha captcha);
        #endregion Methods
    }
}