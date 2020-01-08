using System;

namespace RedditSharp
{
    public class TBUserNote
    {
        #region Fields
        private DateTime _timestamp;
        #endregion Fields

        #region Properties
        public string AppliesToUsername { get; set; }
        public string Message { get; set; }
        public string NoteType { get; set; }
        public int NoteTypeIndex { get; set; }
        public string Submitter { get; set; }
        public int SubmitterIndex { get; set; }
        public string SubName { get; set; }

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set
            {
                _timestamp = DateTime.SpecifyKind(value, DateTimeKind.Utc);
            }
        }

        public string Url { get; set; }
        #endregion Properties
    }
}