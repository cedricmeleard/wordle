using System.Runtime.Serialization;

namespace Wordle.Domain.Exceptions
{
    [Serializable]
    public class GameEndedException : Exception
    {
        public GameEndedException(string? message) : base(message)
        {
        }
    }
}