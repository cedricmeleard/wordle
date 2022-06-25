using System.Reflection;
using Wordle.Infra;

namespace Wordle.Repositories
{
    public class WordInMermoryRepository : IProvideWord
    {
        static readonly Random randomizer = new Random(DateTime.Now.DayOfYear);
        static readonly List<string> previousWords = new();

        public string NewWord()
        {
            string wordsFromFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"datas\5_letter_words_FR.txt");
            var words = File
                .ReadAllText(wordsFromFile)
                .Split()
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .ToArray();

            var newWord = words[randomizer.Next(0, 5069)];
            while (previousWords.Contains(newWord))
            {
                newWord = words[randomizer.Next(0, 5069)];
            }
            previousWords.Add(newWord);

            return newWord;
        }
    }
}