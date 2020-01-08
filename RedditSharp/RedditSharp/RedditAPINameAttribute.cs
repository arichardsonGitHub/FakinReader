using System;

namespace RedditSharp
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    internal class RedditAPINameAttribute : Attribute
    {
        #region Constructors

        internal RedditAPINameAttribute(string name)
        {
            Name = name;
        }
        #endregion Constructors

        #region Properties
        internal string Name { get; private set; }
        #endregion Properties

        #region Methods

        public override string ToString()
        {
            return Name;
        }
        #endregion Methods
    }
}