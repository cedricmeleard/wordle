using Wordle.Domain;
using Wordle.Exceptions;
using Wordle.Repositories;

namespace Wordle.Test;

public class WordServiceTest
{
    private static WordleService CreateWordleService()
    {
        return new WordleService(
            new WordInMermoryRepository(),
            new GameInMemoryRepository()
            );
    }

    [Fact]
    public void start_new_game()
    {
        var userID = "fakeid";
        WordleService sut = CreateWordleService();
        
        var newGame = sut.StartGame(userID);

        Assert.NotNull(newGame);
        Assert.Equal(5, newGame.Word.Length);
        Assert.Equal(0, newGame.Essais);
    }

    [Fact]
    public void start_new_game_when_already_started_should_return_same()
    {
        var userID = "fakeid";
        WordleService sut = CreateWordleService();

        var newGame = sut.StartGame(userID);
        var sameGame = sut.StartGame(userID);

        Assert.Equal(newGame, sameGame);
    }
    [Fact]
    public void start_2_games()
    {
        WordleService sut = CreateWordleService();

        var newGame = sut.StartGame("fakeid");
        var otherGame = sut.StartGame("fakeid2");

        Assert.NotEqual(newGame, otherGame);
    }

    [Fact]
    public void start_new_game_with_user_null_should_thow()
    {
        var userID = "";
        WordleService sut = CreateWordleService();

        Assert.Throws<ArgumentNullException>(() => sut.StartGame(userID));
    }

    [Fact]
    public void try_word()
    {
        var userID = "fakeid";
        WordleService sut = CreateWordleService();

        sut.StartGame(userID);
        var gameWithOneTry = sut.TryWord(userID, "poule");

        Assert.Equal(5, gameWithOneTry.Lines.Length);
        Assert.Equal("u", gameWithOneTry.Lines[0][2].Value);
    }

    [Fact]
    public void try_word_when_word_is_empty_throw()
    {
        var userID = "fakeid";
        WordleService sut = CreateWordleService();
        sut.StartGame(userID);
        Assert.Throws<ArgumentNullException>(() => sut.TryWord(userID, String.Empty));
    }

    [Fact]
    public void try_word_when_user_is_empty_throw()
    {
        WordleService sut = CreateWordleService();
        Assert.Throws<ArgumentNullException>(() => sut.TryWord(String.Empty, "poule"));
    }

    [Fact]
    public void try_word_when_no_game_should_throw()
    {
        var userID = "fakeid";
        WordleService sut = CreateWordleService();
        Assert.Throws<GameNotFoundException>(() => sut.TryWord(userID, "aaaaa"));
    }

    [Fact]
    public void try_word_twice()
    {
        var userID = "fakeid";
        WordleService sut = CreateWordleService();
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
        WordleService sut = CreateWordleService();
        sut.StartGame(userID);

        sut.TryWord(userID, "aaaaa");
        sut.TryWord(userID, "aaaaa");
        sut.TryWord(userID, "aaaaa");
        sut.TryWord(userID, "aaaaa");
        sut.TryWord(userID, "aaaaa");
        Assert.Throws<GameEndedException>(() => sut.TryWord(userID, "aaaaa"));
    }

    [Fact]
    public void Reset_game_when_no_game_should_return_false()
    {
        var userID = "fakeid";
        WordleService sut = CreateWordleService();
        Assert.False( sut.Reset(userID));
    }

    [Fact]
    public void Reset_game_when_game_exist_should_return_true()
    {
        var userID = "fakeid";
        WordleService sut = CreateWordleService();
        sut.StartGame(userID);
        var game = sut.TryWord(userID, "aaaaa");
        Assert.Equal("a", game.Lines[0][3].Value);
        
        Assert.True(sut.Reset(userID));
        game = sut.StartGame(userID);

        Assert.Null(game.Lines[0]);
    }
}