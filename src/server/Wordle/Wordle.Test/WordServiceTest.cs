namespace Wordle.Test;

public class WordServiceTest
{
    [Fact]
    public void should_get_a_word()
    {
        WordleService sut = new WordleService();
        var newGame = sut.StartGame();
        Assert.NotNull(newGame);
        Assert.Equal(5, newGame.Word.Length);
        Assert.Equal(0, newGame.Essais);
    }

    [Fact]
    public void try_word()
    {

        WordleService sut = new WordleService();
        var newGame = sut.StartGame();

        var gameWithOneTry = sut.TryWord(newGame, "12345");

        Assert.Equal(5, gameWithOneTry.Lines.Length);
        Assert.Equal("3", gameWithOneTry.Lines[0][2].Value);
    }
}