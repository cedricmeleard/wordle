using Wordle.Domain.UseCases;
using Wordle.Infra;

namespace Wordle.Test
{
    public class DeleteAGameShould
    {
        private readonly GameInMemoryRepository gameRepo = new GameInMemoryRepository();
        const string userID = "fakeid";
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

        private DeleteAGame CreateDeleteAGame()
        {
            return new DeleteAGame(
                gameRepo
                );
        }

        [Fact]
        public void Reset_game_when_no_game_should_return_false()
        {
            var sut = CreateDeleteAGame();
            Assert.False(sut.Reset(userID));
        }

        [Fact]
        public void Reset_game_when_game_exist_should_return_true()
        {
            CreateWordleService().StartGame(userID);
            var game = CreateMakeAGuess().Make(userID, "aaaaa");
            
            Assert.Equal('a', game.Lines[0][3].Value);

            var sut = CreateDeleteAGame();
            Assert.True(sut.Reset(userID));
            game = CreateWordleService().StartGame(userID);

            Assert.Null(game.Lines[0]);
        }
    }
}
