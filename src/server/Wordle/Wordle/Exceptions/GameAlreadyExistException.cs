using System.Runtime.Serialization;

namespace Wordle.Domain.Exceptions
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