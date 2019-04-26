using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public IActionResult Start()
        {
            return Json(new
            {
                Color = "#aaaaaa",
                HeadType = "bendr",
                TailType = "pixel"
            });
        }
        

        [HttpPost("move")]
        public IActionResult Move()
        {
            var jsonBody = new StreamReader(Request.Body).ReadToEnd();

            this.logger.LogDebug(jsonBody);

            return Json(new
            {
                Move = "left"
            });
        }

        
        [HttpPost("end")]
        public IActionResult End()
        {
            return Ok();
        }
        

        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
