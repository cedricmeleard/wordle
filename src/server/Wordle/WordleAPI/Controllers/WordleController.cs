using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Wordle.Domain;
using Wordle.Domain.UseCases;

namespace WordleAPI.Controllers
{
    [EnableCors("AllowCorsPolicy")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class WordleController : ControllerBase
    {
        private readonly ILogger<WordleController> _logger;
        private readonly StartANewGame startGame;
        private readonly MakeAGuess makeGame;
        private readonly DeleteAGame deleteGame;
        
        public WordleController(ILogger<WordleController> logger, 
            StartANewGame startGame,
            MakeAGuess makeGame,
            DeleteAGame deleteGame)
        {
            _logger = logger;
            this.startGame = startGame;
            this.makeGame = makeGame;
            this.deleteGame = deleteGame;
        }


        [HttpPost(Name = "Start")]
        public WordleGameDto Start([FromQuery()]string userId)
        {
            _logger.LogDebug("Starting new game");

            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var game = startGame.StartGame(userId);

            return new WordleGameDto(game);
        }

        [HttpPost(Name = "Try")]
        public WordleGameDto Try([FromQuery()] string userId, [FromQuery()] string word)
        {
            _logger.LogDebug($"Trying the word {word}");

            var game = makeGame.Make(userId, word);
            return new WordleGameDto(game);
        }

        [HttpPost(Name = "Reset")]
        public bool Reset([FromQuery()] string userId)
        {
            _logger.LogDebug($"Reset game for user {userId}");
            return deleteGame.Reset(userId);
        }
    }
}