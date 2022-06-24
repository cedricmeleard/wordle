using Wordle.Exceptions;

namespace Wordle.Test;

public class WordServiceTest
{
    [Fact]
    public void start_new_game()
    {
        var userID = "fakeid";
        WordleService sut = new WordleService();
        
        var newGame = sut.StartGame(userID);

        Assert.NotNull(newGame);
        Assert.Equal(5, newGame.Word.Length);
        Assert.Equal(0, newGame.Essais);
    }

    [Fact]
    public void try_word()
    {
        var userID = "fakeid";
        WordleService sut = new WordleService();

        sut.StartGame(userID);
        var gameWithOneTry = sut.TryWord(userID, "poule");

        Assert.Equal(5, gameWithOneTry.Lines.Length);
        Assert.Equal("u", gameWithOneTry.Lines[0][2].Value);
    }

    [Fact]
    public void try_word_twice()
    {
        var userID = "fakeid";
        WordleService sut = new WordleService();
        sut.StartGame(userID);

        sut.TryWord(userID, "poule");
        var gameWithTwoTry = sut.TryWord(userID, "tarie");

        Assert.Equal(5, gameWithTwoTry.Lines.Length);
        Assert.Equal("l", gameWithTwoTry.Lines[0][3].Value);
        Assert.Equal("i", gameWithTwoTry.Lines[1][3].Value);
        Assert.Null(gameWithTwoTry.Lines[2]);
    }

    [Fact]
    public void try_word_too_much_should_throw()
    {
        var userID = "fakeid";
        WordleService sut = new WordleService();
        sut.StartGame(userID);

        sut.TryWord(userID, "aaaaa");
        sut.TryWord(userID, "aaaaa");
        sut.TryWord(userID, "aaaaa");
        sut.TryWord(userID, "aaaaa");
        sut.TryWord(userID, "aaaaa");
        Assert.Throws<GameEndedException>(() => sut.TryWord(userID, "aaaaa"));
    }
}