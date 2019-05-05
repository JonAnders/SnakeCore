using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace SnakeCore.Web.Brains
{
    public class Spinner : IBrain
    {
        private readonly ILogger logger;


        public Spinner(ILogger<Spinner> logger)
        {
            this.logger = logger;
        }


        public StartResponse Start(GameState gameState)
        {
            return new StartResponse
            {
                Color = "#aa0000"
            };
        }


        public LegalMove Move(GameState gameState)
        {
            var board = gameState.Board;
            var body = gameState.You.Body;

            var moves = new List<LegalMove> { LegalMove.Up, LegalMove.Right, LegalMove.Down, LegalMove.Left };

            if (body == null)
                return moves.First();

            if (body.Count > 1)
            {
                if (body[0].X == body[1].X && body[0].Y == body[1].Y - 1)
                {
                    // Going up
                    this.logger.LogDebug("Going up!");
                    moves.Remove(LegalMove.Down);

                    PromoteMove(moves, LegalMove.Right);
                }
                else if (body[0].X == body[1].X && body[0].Y == body[1].Y + 1)
                {
                    // Going down
                    this.logger.LogDebug("Going down!");
                    moves.Remove(LegalMove.Up);

                    PromoteMove(moves, LegalMove.Left);
                }
                else if (body[0].X == body[1].X + 1 && body[0].Y == body[1].Y)
                {
                    // Going right
                    this.logger.LogDebug("Going right!");
                    moves.Remove(LegalMove.Left);

                    PromoteMove(moves, LegalMove.Down);
                }
                else if (body[0].X == body[1].X - 1 && body[0].Y == body[1].Y)
                {
                    // Going left
                    this.logger.LogDebug("Going left!");
                    moves.Remove(LegalMove.Right);

                    PromoteMove(moves, LegalMove.Up);
                }
            }

            // Remove moves that go over the board edge
            if (body[0].X < 1)
                moves.Remove(LegalMove.Left);
            if (body[0].X >= board.Width - 1)
                moves.Remove(LegalMove.Right);
            if (body[0].Y < 1)
                moves.Remove(LegalMove.Up);
            if (body[0].Y >= board.Height - 1)
                moves.Remove(LegalMove.Down);

            return moves.First();
        }


        public void End(GameState gameState)
        {
        }


        private void PromoteMove(IList<LegalMove> moves, LegalMove moveToPromote)
        {
            moves.Remove(moveToPromote);
            moves.Insert(0, moveToPromote);
        }
    }
}