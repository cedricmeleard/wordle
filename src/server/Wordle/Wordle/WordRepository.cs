using System.Reflection;

namespace Wordle
{
    public class WordRepository
    {
        private static Random randomizer = new Random(DateTime.Now.DayOfYear);
        public string GetWord()
        {
            string wordsFromFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"datas/5_letter_words_FR.txt");
            var words = File
                .ReadAllText(wordsFromFile)
                .Split()
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .ToArray();

            return words[randomizer.Next(0, 5069)];
        }
    }
}
