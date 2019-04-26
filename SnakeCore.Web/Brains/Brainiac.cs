using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Extensions.Logging;

namespace SnakeCore.Web.Brains
{
    public class Brainiac : IBrain
    {
        private readonly ILogger logger;


        public Brainiac(ILogger logger)
        {
            this.logger = logger;
        }


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
            EatIfHungry(moves, gameState);
            PredictFuture(moves, gameState);

            PrintWeightedMoves(moves);

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
                if (weightedMove.NewX < 0 || weightedMove.NewX >= boardSize
                    || weightedMove.NewY < 0|| weightedMove.NewY >= boardSize)
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

            var victims = gameState.Board.Snakes.Where(x => x.Id != gameState.You.Id).ToList();
            foreach (var weightedMove in weightedMoves)
            {
                foreach (var victim in victims)
                {
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


        private void EatIfHungry(List<WeightedMove> weightedMoves, GameState gameState)
        {
            if (gameState.Board.Food == null)
                return;

            var minFoodDist = int.MaxValue;
            var moveClosestToFood = weightedMoves[0];
            foreach (var weightedMove in weightedMoves)
            {
                foreach (var foodPosition in gameState.Board.Food)
                {
                    var dist = Math.Abs(foodPosition.X - weightedMove.NewX) + Math.Abs(foodPosition.Y - weightedMove.NewY);
                    if (dist < minFoodDist)
                    {
                        minFoodDist = dist;
                        moveClosestToFood = weightedMove;
                    }
                }
            }

            moveClosestToFood.Weight += 100 - gameState.You.Health;
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


        private void PrintWeightedMoves(List<WeightedMove> weightedMoves)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("Weights:");
            foreach (var weightedMove in weightedMoves)
            {
                sb.AppendLine($"{weightedMove.Move}: {weightedMove.Weight}");
            }

            this.logger.LogDebug(sb.ToString());
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