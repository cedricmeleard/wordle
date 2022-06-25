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

        var line = new Letter[5];
        for (int i = 0; i < 5; i++)
        {
            if (game.Word[i] == word[i])
            {
                line[i] = new Letter(word[i].ToString(), 1);
            }
            else if (game.Word.Contains(word[i]))
            {
                line[i] = new Letter(word[i].ToString(), 2);
            }
            else
            {
                line[i] = new Letter(word[i].ToString(), 0);
            }
        }

        game.AddLine(line);
        return game;
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
