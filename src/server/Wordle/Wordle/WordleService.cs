using Wordle.Exceptions;

namespace Wordle;
public class WordleService
{
    readonly WordRepository wordRepository;
    readonly List<string> previousWords;
    static Dictionary<string, WordleGame> _games;

    public WordleService()
    {
        wordRepository = new WordRepository();
        previousWords = new List<string>();
        _games = new Dictionary<string, WordleGame>();
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

        if (_games.ContainsKey(userId))
            return _games[userId];

        /* Call repository to get a new currentWord */
        var word = wordRepository.GetWord();
        while (previousWords.Contains(word))
        {
            word = wordRepository.GetWord();
        }
        previousWords.Add(word);

        var game = new WordleGame(word);
        _games.Add(userId, game); ;

        return game;
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
        if (!_games.ContainsKey(userId))
            throw new GameNotFoundException(nameof(_games));
        var game = _games[userId];
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
        if (!_games.ContainsKey(userId))
            return false;

        return _games.Remove(userId);
    }
}
