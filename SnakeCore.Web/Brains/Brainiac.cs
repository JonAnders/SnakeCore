using System.Collections.Generic;
using System.Linq;

namespace SnakeCore.Web.Brains
{
    public class Brainiac : IBrain
    {
        public StartResponse Start(GameState gameState)
        {
            return new StartResponse
            {
                Color = "#f9812a",
                HeadType = "dead",
                TailType = "bolt"
            };
        }


        public LegalMove Move(GameState gameState)
        {
            var moves = new List<WeightedMove>
            {
                new WeightedMove(LegalMove.Up),
                new WeightedMove(LegalMove.Right),
                new WeightedMove(LegalMove.Down),
                new WeightedMove(LegalMove.Left)
            };

            AvoidWalls(moves, gameState);

            var orderedMoves = moves
                .OrderByDescending(x => x.Weight);

            var selectedMove = orderedMoves.First();

            return selectedMove.Move;
        }


        public void End(GameState gameState)
        {
        }
        

        private void AvoidWalls(List<WeightedMove> weightedMoves, GameState gameState)
        {
            int boardSize = gameState.Board.Height;
            var headPosition = gameState.You.Body[0];

            foreach (var weightedMove in weightedMoves)
            {
                if (weightedMove.Move == LegalMove.Up)
                {
                    if (headPosition.Y < 1)
                        weightedMove.Weight -= 100;
                }
                else if (weightedMove.Move == LegalMove.Down)
                {
                    if (headPosition.Y >= boardSize - 1)
                        weightedMove.Weight -= 100;
                }
                else if (weightedMove.Move == LegalMove.Right)
                {
                    if (headPosition.X >= boardSize - 1)
                        weightedMove.Weight -= 100;
                }
                else if (weightedMove.Move == LegalMove.Left)
                {
                    if (headPosition.X < 1)
                        weightedMove.Weight -= 100;
                }
            }
        }


        private class WeightedMove
        {
            public WeightedMove(LegalMove move)
            {
                this.Move = move;
            }


            public LegalMove Move;
            public int Weight;
        }
    }
}