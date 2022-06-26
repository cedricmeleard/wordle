using Wordle.Domain;
using Wordle.Domain.Ports;

namespace Wordle.Infra
{
    public class GameInMemoryRepository : IProvideGame
    {
        Dictionary<string, WordleGame> _games;

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