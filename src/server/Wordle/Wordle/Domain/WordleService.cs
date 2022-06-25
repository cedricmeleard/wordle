using Wordle.Exceptions;
using Wordle.Infra;

namespace Wordle.Domain;
public class WordleService
{
    readonly IProvideWord wordAdapter;
    readonly IProvideGame gameAdapter;

    public WordleService(IProvideWord wordProvider, IProvideGame gameProvider)
    {
        wordAdapter = wordProvider;
        gameAdapter = gameProvider;
    }

    /// <summary>
    /// Start a new game with a new word for a user, if game already exist it should return it
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public WordleGame StartGame(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentNullException(nameof(userId));

        //game may already exist then shoudl return it
        var game = gameAdapter.Find(userId);
        if (game != null)
            return game;

        return gameAdapter
            .Create(userId, wordAdapter.NewWord());
    }

    /// <summary>
    /// Add a new try for the word in params
    /// </summary>
    /// <param name="userId">current user</param>
    /// <param name="word">new word</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="GameNotFoundException"></exception>
    public WordleGame TryWord(string userId, string word)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentNullException(nameof(userId));
        if (string.IsNullOrWhiteSpace(word))
            throw new ArgumentNullException(nameof(word));

        var game = gameAdapter.Find(userId);
        if (game == null)
            throw new GameNotFoundException(nameof(userId));
        if (game.Essais == 5)
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
            line[i] = new Letter(currentLetter, nbPresentInWordToGuess - nbPresentActuallyFound  > 0 ? 2 : 0);
        }

        return line;

    }

    private bool NotExistInWord(string wordToFind, char currentLetter)
    {
        return !wordToFind.Contains(currentLetter);
    }

    private bool IsCorrectlyPlaced(string wordToFind, int i, char currentLetter)
    {
        return wordToFind[i] == currentLetter;
    }

    /// <summary>
    /// Delete a game
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public bool Reset(string userId)
    {
        return gameAdapter.Delete(userId);
    }
}
