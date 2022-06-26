using System.Reflection;
using Wordle.Domain.Ports;

namespace Wordle.Infra
{
    public class WordInMermoryRepository : IProvideWord
    {
        readonly Random randomizer = new Random(DateTime.Now.DayOfYear);
        readonly List<string> previousWords = new();

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