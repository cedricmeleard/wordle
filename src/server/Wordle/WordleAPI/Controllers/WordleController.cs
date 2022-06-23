using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Wordle;

namespace WordleAPI.Controllers
{
    [EnableCors("AllowCorsPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class WordleController : ControllerBase
    {
        private static string currentWord;
        private static readonly Dictionary<string, WordleGame> games = new();
        private readonly ILogger<WordleController> _logger;

        public WordleController(ILogger<WordleController> logger)
        {
            _logger = logger;
        }

        
        [HttpPost(Name = "Start")]
        public WordleGameDto Start(string userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            if (games.ContainsKey(userId))
                return new WordleGameDto(games[userId]);

            var newgame = new WordleService().StartGame();
            games.Add(userId, newgame);

            return new WordleGameDto( newgame );
        }

        [HttpPut(Name = "Try")]
        public WordleGameDto Try(string userId, string word)
        {
            if (!games.ContainsKey(userId))
                throw new KeyNotFoundException(nameof(userId));

            var game = games[userId];

            game  = new WordleService().TryWord(game, word);
            return new WordleGameDto(game);
        }
    }
}