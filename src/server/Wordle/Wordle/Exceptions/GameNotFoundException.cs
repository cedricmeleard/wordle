using System.Runtime.Serialization;

namespace Wordle.Exceptions
{
    [Serializable]
    public class GameNotFoundException : Exception
    {
        public GameNotFoundException(string? message) : base(message)
        {
        }
    }
}