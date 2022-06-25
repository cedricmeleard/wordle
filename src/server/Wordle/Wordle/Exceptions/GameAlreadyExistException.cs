using System.Runtime.Serialization;

namespace Wordle.Exceptions
{
    [Serializable]
    internal class GameAlreadyExistException : Exception
    {
        public GameAlreadyExistException()
        {
        }

        public GameAlreadyExistException(string? message) : base(message)
        {
        }
    }
}