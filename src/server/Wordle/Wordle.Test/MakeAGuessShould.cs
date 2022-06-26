using Wordle.Domain.Exceptions;
using Wordle.Domain.UseCases;
using Wordle.Infra;

namespace Wordle.Test
{
    public class MakeAGuessShould
    {
        const string userID = "fakeid";
        readonly GameInMemoryRepository gameRepo = new();

        private StartANewGame CreateWordleService()
        {
            return new StartANewGame(
                //should be a fake repo
                new WordInMermoryRepository(),
                gameRepo
                );
        }

        private MakeAGuess CreateMakeAGuess()
        {
            return new MakeAGuess(
                gameRepo
                );
        }


        [Fact]
        public void Try_word()
        {
            CreateWordleService().StartGame(userID);
            var gameWithOneTry = CreateMakeAGuess().Make(userID, "poule");

            Assert.Equal(6, gameWithOneTry.Lines.Length);
            Assert.Equal('u', gameWithOneTry.Lines[0][2].Value);
        }

        [Fact]
        public void Try_word_when_word_is_empty_throw()
        {
            CreateWordleService().StartGame(userID);
            Assert.Throws<ArgumentNullException>(() => CreateMakeAGuess().Make(userID, String.Empty));
        }

        [Fact]
        public void Try_word_when_user_is_empty_throw()
        {
            Assert.Throws<ArgumentNullException>(() => CreateMakeAGuess().Make(String.Empty, "poule"));
        }

        [Fact]
        public void Try_word_when_no_game_should_throw()
        {
            Assert.Throws<GameNotFoundException>(() => CreateMakeAGuess().Make(userID, "aaaaa"));
        }

        [Fact]
        public void Try_word_twice()
        {
            CreateWordleService().StartGame(userID);
            
            var sut = CreateMakeAGuess();
            sut.Make(userID, "poule");
            var gameWithTwoTry = sut.Make(userID, "tarie");

            Assert.Equal(6, gameWithTwoTry.Lines.Length);
            Assert.Equal('l', gameWithTwoTry.Lines[0][3].Value);
            Assert.Equal('i', gameWithTwoTry.Lines[1][3].Value);
            Assert.Null(gameWithTwoTry.Lines[2]);
        }

        [Fact]
        public void Try_word_too_much_should_throw()
        {
            CreateWordleService().StartGame(userID);

            var sut = CreateMakeAGuess();

            sut.Make(userID, "aaaaa");
            sut.Make(userID, "aaaaa");
            sut.Make(userID, "aaaaa");
            sut.Make(userID, "aaaaa");
            sut.Make(userID, "aaaaa");
            sut.Make(userID, "aaaaa");
            
            Assert.Throws<GameEndedException>(() => sut.Make(userID, "aaaaa"));
        }
    }
}
