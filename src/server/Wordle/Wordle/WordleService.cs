namespace Wordle;
public class WordleService
{
    WordRepository wordRepository = new();
    List<string> previousWords = new();

    public WordleGame StartGame()
    {
        /* Call repository to get a new currentWord */
        var word = wordRepository.GetWord();
        while (previousWords.Contains(word))
        {
            word = wordRepository.GetWord();
        }
        previousWords.Add(word);

        return new WordleGame(word);
    }

    public WordleGame TryWord(WordleGame game, string word)
    {
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

}
