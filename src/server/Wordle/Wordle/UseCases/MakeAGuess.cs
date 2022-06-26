using Wordle.Domain.Exceptions;
using Wordle.Domain.Ports;

namespace Wordle.Domain.UseCases
{
    public class MakeAGuess
    {
        private readonly IProvideGame gameProvider;
        public MakeAGuess(IProvideGame gameProvider)
        {
            this.gameProvider = gameProvider;
        }

        public WordleGame Make(string userId, string word)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException(nameof(word));

            var game = gameProvider.Find(userId);
            if (game == null)
                throw new GameNotFoundException(nameof(userId));
            if (game.Essais == 6)
                throw new GameEndedException(nameof(game));

            var line = CreateLine(word, game.Word);
            game.AddLine(line);

            return game;
        }

        private Letter[] CreateLine(string word, string wordToFind)
        {
            var length = word.Length;
            var line = new Letter[length];

            //first we check only correct & inccorect lettters
            for (int i = 0; i < length; i++)
            {
                var currentLetter = word[i];
                if (IsCorrectlyPlaced(wordToFind, i, currentLetter))
                {
                    line[i] = new Letter(currentLetter, 1);
                }
                if (NotExistInWord(wordToFind, currentLetter))
                {
                    line[i] = new Letter(currentLetter, 0);
                }
            }

            //last we check misplaced
            for (int i = 0; i < length; i++)
            {
                // already found 1 or 0
                if (line[i] != null)
                    continue;

                var currentLetter = word[i];
                // total number of this letter to find
                var nbPresentInWordToGuess = wordToFind.Count(p => p == currentLetter);
                // minus already found
                var nbPresentActuallyFound = line.Count(p => p != null && p.Value == currentLetter && p.Validity != 0);
                // so there is still letter to find
                line[i] = new Letter(currentLetter, nbPresentInWordToGuess - nbPresentActuallyFound > 0 ? 2 : 0);
            }

            return line;

        }

        private static bool NotExistInWord(string wordToFind, char currentLetter)
        {
            return !wordToFind.Contains(currentLetter);
        }

        private static bool IsCorrectlyPlaced(string wordToFind, int i, char currentLetter)
        {
            return wordToFind[i] == currentLetter;
        }
    }
}
