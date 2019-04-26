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
            var moves = InitWeightedMoves(gameState);

            AvoidWalls(moves, gameState);
            AvoidSnakes(moves, gameState);
            ConsiderKilling(moves, gameState);
            PredictFuture(moves, gameState);

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

            foreach (var weightedMove in weightedMoves)
            {
                if (weightedMove.NewX < 0 || weightedMove.NewX >= boardSize - 1 
                    || weightedMove.NewY < 0|| weightedMove.NewY >= boardSize - 1)
                    weightedMove.Weight -= 100;
            }
        }


        private void AvoidSnakes(List<WeightedMove> weightedMoves, GameState gameState)
        {
            foreach (var weightedMove in weightedMoves)
            {
                foreach (var snake in gameState.Board.Snakes)
                {
                    foreach (var bodyPartPosition in snake.Body)
                    {
                        if (bodyPartPosition.X == weightedMove.NewX && bodyPartPosition.Y == weightedMove.NewY)
                        {
                            if (snake.Id == gameState.You.Id)
                                weightedMove.Weight -= 100;
                            else
                                weightedMove.Weight -= 80;
                        }
                    }
                }
            }
        }


        private void ConsiderKilling(List<WeightedMove> weightedMoves, GameState gameState)
        {
            var selfSize = gameState.You.Body.Count;

            foreach (var weightedMove in weightedMoves)
            {
                for (int i = 1; i < gameState.Board.Snakes.Count; i++)
                {
                    var victim = gameState.Board.Snakes[i];
                    var victimHead = victim.Body[0];
                    var possibleVictimHeadPositions = new List<GameState.BodyPartPosition>
                    {
                        new GameState.BodyPartPosition(victimHead.X + 1, victimHead.Y),
                        new GameState.BodyPartPosition(victimHead.X - 1, victimHead.Y),
                        new GameState.BodyPartPosition(victimHead.X, victimHead.Y + 1),
                        new GameState.BodyPartPosition(victimHead.X, victimHead.Y - 1)
                    };
                    possibleVictimHeadPositions.Remove(victim.Body[1]);

                    if (possibleVictimHeadPositions.Any(x => x.X == weightedMove.NewX && x.Y == weightedMove.NewY))
                    {
                        if (selfSize > victim.Body.Count)
                            weightedMove.Weight += 5;
                        else
                            weightedMove.Weight -= 50;
                    }
                }
            }
        }


        private void PredictFuture(List<WeightedMove> weightedMoves, GameState gameState)
        {
            foreach (var weightedMove in weightedMoves)
            {
                // Adjust gameState
                var futureGameState = gameState.Copy();
                futureGameState.Turn++;
                foreach (var snake in futureGameState.Board.Snakes)
                {
                    if (snake.Body.Count > 0)
                        snake.Body.RemoveAt(snake.Body.Count - 1);
                }
                futureGameState.You.Body.RemoveAt(futureGameState.You.Body.Count - 1);
                futureGameState.Board.Snakes[0].Body.Insert(0, new GameState.BodyPartPosition(weightedMove.NewX, weightedMove.NewY));
                futureGameState.You.Body.Insert(0, new GameState.BodyPartPosition(weightedMove.NewX, weightedMove.NewY));

                // Call AvoidWalls and AvoidSnakes
                var futureWeightedMoves = InitWeightedMoves(futureGameState);
                AvoidWalls(futureWeightedMoves, futureGameState);
                AvoidSnakes(futureWeightedMoves, futureGameState);

                // TODO: Call PredictFuture recursively if weightedMove >= 0

                // Adjust weight
                var bestFutureMove = futureWeightedMoves
                    .OrderByDescending(x => x.Weight)
                    .First();

                weightedMove.Weight += (int)(bestFutureMove.Weight * 0.75);
            }
        }


        private List<WeightedMove> InitWeightedMoves(GameState gameState)
        {
            var head = gameState.You.Body[0];

            return new List<WeightedMove>
            {
                new WeightedMove(LegalMove.Up, head.X, head.Y - 1),
                new WeightedMove(LegalMove.Right, head.X + 1, head.Y),
                new WeightedMove(LegalMove.Down, head.X, head.Y + 1),
                new WeightedMove(LegalMove.Left, head.X - 1, head.Y)
            };
        }


        private class WeightedMove
        {
            public WeightedMove(LegalMove move, int newX, int newY)
            {
                this.Move = move;
                this.NewX = newX;
                this.NewY = newY;
            }


            public LegalMove Move;
            public int Weight;
            public int NewX;
            public int NewY;
        }
    }
}