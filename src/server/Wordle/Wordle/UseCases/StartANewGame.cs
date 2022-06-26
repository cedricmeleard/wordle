using Wordle.Domain.Ports;

namespace Wordle.Domain.UseCases
{
    public class StartANewGame
    {
        private readonly IProvideGame gameProvider;
        private readonly IProvideWord wordProvider;

        public StartANewGame(IProvideWord wordProvider, IProvideGame gameProvider)
        {
            this.wordProvider = wordProvider;
            this.gameProvider = gameProvider;
        }

        public WordleGame StartGame(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));

            //game may already exist then shoudl return it
            var game = gameProvider.Find(userId);
            if (game != null)
                return game;

            return gameProvider
                .Create(userId, wordProvider.NewWord());
        }
    }
}
