using System.Runtime.Serialization;

namespace Wordle.Exceptions
{
    [Serializable]
    public class GameEndedException : Exception
    {
        public GameEndedException(string? message) : base(message)
        {
        }
    }
}