using Wordle.Domain.Ports;

namespace Wordle.Domain.UseCases
{
    public class DeleteAGame
    {
        private readonly IProvideGame gameProvider;
        public DeleteAGame(IProvideGame gameProvider)
        {
            this.gameProvider = gameProvider;
        }

        public bool Reset(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));

            return gameProvider.Delete(userId);
        }
    }
}
