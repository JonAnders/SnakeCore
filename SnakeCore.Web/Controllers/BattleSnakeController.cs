using System;
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
            var brain = GetBrain(brainName);
            
            return Json(brain.Move(gameState));
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
            }

            throw new Exception($"Unkown brain: {brainName}");
        }
    }
}
