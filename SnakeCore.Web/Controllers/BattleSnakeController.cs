using System;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SnakeCore.Web.Brains;

namespace SnakeCore.Web.Controllers
{
    [Route("{brain}")]
    [ApiController]
    public class BattleSnakeController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IServiceProvider serviceProvider;


        public BattleSnakeController(ILogger<BattleSnakeController> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }


        [HttpGet]
        public IActionResult GetBattlesnake([FromRoute(Name = "brain")] string brainName)
        {
            var brain = GetBrain(brainName);

            var battlesnake = brain.GetBattlesnake();

            return Ok(battlesnake);
        }


        [HttpPost("start")]
        public IActionResult Start([FromRoute(Name = "brain")]string brainName, GameState gameState)
        {
            var brain = GetBrain(brainName);

            brain.Start(gameState);

            return Ok();
        }


        [HttpPost("move")]
        public IActionResult Move([FromRoute(Name = "brain")]string brainName, GameState gameState)
        {
            PrintBoard(gameState);

            var brain = GetBrain(brainName);

            var move = brain.Move(gameState);

            this.logger.LogDebug($"\nSelected move: {move}\n");

            return Ok(move);
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
            Type brainType = null;
            switch (brainName)
            {
                case "lefty":
                    brainType = typeof(Lefty);
                    break;
                case "spinner":
                    brainType = typeof(Spinner);
                    break;
                case "brainiac":
                    brainType = typeof(Brainiac);
                    break;
                case "nostradamus":
                    brainType = typeof(Nostradamus);
                    break;
                case "floody":
                    brainType = typeof(Floody);
                    break;
                default:
                    throw new Exception($"Unkown brain: {brainName}");
            }

            return (IBrain)this.serviceProvider.GetService(brainType);
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
            sb.AppendLine();
            for (int y = board[0].Length - 1; y >= 0; y--)
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
