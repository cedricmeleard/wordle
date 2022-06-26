using System.Runtime.Serialization;

namespace Wordle.Domain.Exceptions
{
    [Serializable]
    public class GameNotFoundException : Exception
    {
        public GameNotFoundException(string? message) : base(message)
        {
        }
    }
}