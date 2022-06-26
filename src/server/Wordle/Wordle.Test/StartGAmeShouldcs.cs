using Wordle.Domain.UseCases;
using Wordle.Infra;

namespace Wordle.Test
{
    public class StartGAmeShouldcs
    {
        private StartANewGame CreateStartNewGame()
        {
            return new StartANewGame(
                new WordInMermoryRepository(),
                new GameInMemoryRepository()
                );
        }

        [Fact]
        public void Create_new_game()
        {
            var userID = "fakeid";
            StartANewGame sut = CreateStartNewGame();

            var newGame = sut.StartGame(userID);

            Assert.NotNull(newGame);
            Assert.Equal(5, newGame.Word.Length);
            Assert.Equal(0, newGame.Essais);
        }

        [Fact]
        public void Return_game_when_started()
        {
            var userID = "fakeid";
            StartANewGame sut = CreateStartNewGame();

            var newGame = sut.StartGame(userID);
            var sameGame = sut.StartGame(userID);

            Assert.Equal(newGame, sameGame);
        }
        [Fact]
        public void Create_a_gmae_for_each_user()
        {
            StartANewGame sut = CreateStartNewGame();

            var newGame = sut.StartGame("fakeid");
            var otherGame = sut.StartGame("fakeid2");

            Assert.NotEqual(newGame, otherGame);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Throw_when_userId_is_nullOrEmpty(string? userId)
        {
            var userID = "";
            StartANewGame sut = CreateStartNewGame();

            Assert.Throws<ArgumentNullException>(() => sut.StartGame(userID));
        }
    }
}
