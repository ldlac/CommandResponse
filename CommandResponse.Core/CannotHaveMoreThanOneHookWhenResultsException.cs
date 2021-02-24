using System;
using System.Runtime.Serialization;

namespace CommandResponse.Core
{
    [Serializable]
    public class CannotHaveMoreThanOneHookWhenResultsException : Exception
    {
        internal CannotHaveMoreThanOneHookWhenResultsException()
        {
        }

        protected CannotHaveMoreThanOneHookWhenResultsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}