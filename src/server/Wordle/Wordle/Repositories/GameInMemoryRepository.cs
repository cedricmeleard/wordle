using Wordle.Domain;
using Wordle.Exceptions;
using Wordle.Infra;

namespace Wordle.Repositories
{
    public class GameInMemoryRepository : IProvideGame
    {
        static Dictionary<string, WordleGame> _games;

        public GameInMemoryRepository()
        {
            _games = new Dictionary<string, WordleGame>();
        }

        public WordleGame Find(string userId)
        {
            if (!_games.ContainsKey(userId))
                return null;

            return _games[userId];
        }

        public WordleGame Create(string userId, string word)
        {
            if (_games.ContainsKey(userId))
                throw new GameAlreadyExistException();
            
            var newGame = new WordleGame(word);
            _games.Add(userId, newGame);

            return newGame;
        }

        public bool Delete(string userId)
        {
            if (!_games.ContainsKey(userId))
                return false;
            _games.Remove(userId);
            return true;
        }
    }
}