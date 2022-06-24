using System.Linq;

namespace Wordle
{
    public class WordleGame
    {
        public int Essais { get; private set; } = 0;
        public string Word { get; private set; }
        public Letter[][] Lines { get; private set; } = new Letter[5][];

        public WordleGame(string word)
        {
            Word = word;
        }

        internal void AddLine(Letter[] letters)
        {
            Lines[Essais] = letters;
            this.Essais++;
        }
    }
}