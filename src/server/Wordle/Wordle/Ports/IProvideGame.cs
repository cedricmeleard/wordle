namespace Wordle.Domain.Ports
{
    public interface IProvideGame
    {
        WordleGame Create(string userId, string word);
        WordleGame Find(string userId);
        bool Delete(string userId);
    }
}