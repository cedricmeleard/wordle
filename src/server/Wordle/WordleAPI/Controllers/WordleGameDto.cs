using Wordle.Domain;

namespace WordleAPI.Controllers
{
    public class WordleGameDto
    {
        public int Essais { get; }
        public Letter[][] Lines { get; }

        public WordleGameDto(WordleGame newgame)
        {
            this.Essais = newgame.Essais;
            this.Lines = newgame.Lines;
        }
    }
}