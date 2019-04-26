using System;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SnakeCore.Web.Brains;

namespace SnakeCore.Web.Controllers
{
    [Route("{brain}")]
    [ApiController]
    public class BattleSnakeController : Controller
    {
        private readonly ILogger logger;


        public BattleSnakeController(ILogger<BattleSnakeController> logger)
        {
            this.logger = logger;
        }


        [Route("start")]
        public IActionResult Start([FromRoute(Name = "brain")]string brainName, GameState gameState)
        {
            var brain = GetBrain(brainName);

            return Json(brain.Start(gameState));
        }


        [HttpPost("move")]
        public IActionResult Move([FromRoute(Name = "brain")]string brainName, GameState gameState)
        {
            PrintBoard(gameState);

            var brain = GetBrain(brainName);

            var move = brain.Move(gameState);

            this.logger.LogDebug($"\nSelected move: {move}\n");

            return Json(move);
        }

        
        [HttpPost("end")]
        public IActionResult End([FromRoute(Name = "brain")]string brainName, GameState gameState)
        {
            var brain = GetBrain(brainName);

            brain.End(gameState);

            return Ok();
        }
        

        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok();
        }
        

        private IBrain GetBrain(string brainName)
        {
            switch (brainName)
            {
                case "lefty":
                    return new Lefty();
                case "spinner":
                    return new Spinner(this.logger);
                case "brainiac":
                    return new Brainiac(this.logger);
            }

            throw new Exception($"Unkown brain: {brainName}");
        }


        private void PrintBoard(GameState gameState)
        {
            this.logger.LogDebug($"Turn: {gameState.Turn}");

            // Init board arrays
            var board = new char[gameState.Board.Width][];
            for (int i = 0; i < gameState.Board.Width; i++)
                board[i] = new char[gameState.Board.Height];

            // Add snakes
            StringBuilder sb = new StringBuilder();
            var snakes = gameState.Board.Snakes;
            if (snakes != null)
            {
                this.logger.LogDebug("Snakes:");

                for (int i = 0; i < snakes.Count; i++)
                {
                    sb.Clear();
                    sb.Append($"{i}: ");

                    foreach (var position in snakes[i].Body)
                    {
                        board[position.X][position.Y] = char.Parse(i.ToString());
                        sb.Append($"({position.X}, {position.Y}) ");
                    }

                    this.logger.LogDebug(sb.ToString());
                }
            }

            // Add food
            var food = gameState.Board.Food;
            if (food != null)
            {
                foreach (var position in food)
                {
                    board[position.X][position.Y] = 'X';
                }
            }

            sb.Clear();
            for (int y = 0; y < board[0].Length; y++)
            {
                sb.Append('|');
                for (int x = 0; x < board.Length; x++)
                {
                    var c = board[x][y];
                    sb.Append(c == '\0' ? '_' : c);
                    sb.Append('|');
                }

                sb.AppendLine();
            }

            this.logger.LogDebug($"\nBoard:\n{sb}\n");
        }
    }
}
