using System;

namespace RedditSharp
{
    internal class ToolBoxUserNotesException : Exception
    {
        #region Constructors

        public ToolBoxUserNotesException()
        {
        }

        public ToolBoxUserNotesException(string message)
            : base(message)
        {
        }

        public ToolBoxUserNotesException(string message, Exception inner)
            : base(message, inner)
        {
        }
        #endregion Constructors
    }
}