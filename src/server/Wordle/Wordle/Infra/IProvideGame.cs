using Wordle.Domain;

namespace Wordle.Infra
{
    public interface IProvideGame
    {
        WordleGame Create(string userId, string word);
        WordleGame Find(string userId);
        bool Delete(string userId);
    }
}