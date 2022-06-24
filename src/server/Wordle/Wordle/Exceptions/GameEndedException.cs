﻿using System.Runtime.Serialization;

namespace Wordle.Exceptions
{
    [Serializable]
    public class GameEndedException : Exception
    {
        public GameEndedException()
        {
        }

        public GameEndedException(string? message) : base(message)
        {
        }

        public GameEndedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected GameEndedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}