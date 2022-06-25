using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Wordle.Domain;
using Wordle.Repositories;

namespace WordleAPI.Controllers
{
    [EnableCors("AllowCorsPolicy")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class WordleController : ControllerBase
    {
        private readonly ILogger<WordleController> _logger;
        private static WordleService _service;
        WordleService WordleService
        {
            get
            {
                if (_service == null)
                {
                    _service = new(
                        new WordInMermoryRepository(),
                        new GameInMemoryRepository()
                        );
                }
                return _service;
            }
        }

        public WordleController(ILogger<WordleController> logger)
        {
            _logger = logger;
        }


        [HttpPost(Name = "Start")]
        public WordleGameDto Start([FromQuery()]string userId)
        {
            _logger.LogDebug("Starting new game");

            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var game = WordleService.StartGame(userId);

            return new WordleGameDto(game);
        }

        [HttpPost(Name = "Try")]
        public WordleGameDto Try([FromQuery()] string userId, [FromQuery()] string word)
        {
            _logger.LogDebug($"Trying the word {word}");

            var game = WordleService.TryWord(userId, word);
            return new WordleGameDto(game);
        }

        [HttpPost(Name = "Reset")]
        public bool Reset([FromQuery()] string userId)
        {
            _logger.LogDebug($"Reset game for user {userId}");
            return WordleService.Reset(userId);
        }
    }
}