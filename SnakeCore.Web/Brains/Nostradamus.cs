using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeCore.Web.Brains
{
    public class Nostradamus : IBrain
    {
        private readonly GameEngine gameEngine;
        private readonly NostradamusPrecalc precalc;


        public Nostradamus(NostradamusPrecalc precalc)
        {
            this.gameEngine = new GameEngine();
            this.precalc = precalc;
        }


        public Battlesnake GetBattlesnake()
        {
            return new Battlesnake
            {
                Color = "#7788aa",
                Head = "silly",
                Tail = "freckled"
            };
        }


        public void Start(GameState gameState)
        {
            return;
        }


        public LegalMove Move(GameState gameState)
        {
            var indexOfMyself = gameState.Board.Snakes.IndexOf(gameState.Board.Snakes.First(x => x.Id == gameState.You.Id));

            //var move = GetMostSurvivableMove(gameState.Board, indexOfMyself);

            var move = GetMostSurvivableMoveWithRecursion(gameState.Board, indexOfMyself);

            return move;
        }

        private LegalMove GetMostSurvivableMove(GameState.BoardData board, int indexOfMyself)
        {
            var boardArray = new int[board.Width, board.Height];
            foreach (var snake in board.Snakes)
            {
                for (int i = 0; i < snake.Body.Count - 1; i++)
                    boardArray[snake.Body[i].X, snake.Body[i].Y] = 1;
            }
            foreach (var food in board.Food)
            {
                boardArray[food.X, food.Y] = 1337;
            }

            var snakeBodies = board.Snakes.Select(x => x.Body.ToArray()).ToArray();
            var healths = board.Snakes.Select(x => x.Health).ToArray();

            var permutations = this.precalc.GetPossibleMovesPermutations(board.Snakes.Count);

            var survivableFutures = new Dictionary<LegalMove, int>
            {
                { LegalMove.Up, 0 },
                { LegalMove.Down, 0 },
                { LegalMove.Left, 0 },
                { LegalMove.Right, 0 }
            };

            for (int i = 0; i < permutations.Length; i++)
            {
                (var futureSnakes, var futureHealths) = gameEngine.ProcessMoves(snakeBodies, healths, boardArray, permutations[i]);

                if (futureHealths[indexOfMyself] > 0)
                    survivableFutures[permutations[i][indexOfMyself]]++;
            }

            foreach (var survivableFuture in survivableFutures)
            {
                Console.WriteLine($"{survivableFuture.Key}: {survivableFuture.Value}");
            }

            return survivableFutures.First(x => x.Value == survivableFutures.Max(y => y.Value)).Key;
        }


        private LegalMove GetMostSurvivableMoveWithRecursion(GameState.BoardData board, int indexOfMyself)
        {
            var permutations = this.precalc.GetPossibleMovesPermutations(board.Snakes.Count);

            var survivableFuturesPerMove = new Dictionary<LegalMove, int>
            {
                { LegalMove.Up, 0 },
                { LegalMove.Down, 0 },
                { LegalMove.Left, 0 },
                { LegalMove.Right, 0 }
            };

            var snakeBodies = board.Snakes.Select(x => x.Body.ToArray()).ToArray();
            var healths = board.Snakes.Select(x => x.Health).ToArray();

            var depth = 5 - snakeBodies.Length;
            if (depth < 0)
                depth = 0;
            Console.WriteLine("Depth: " + depth);

            foreach (var permutation in permutations)
            {
                var survivableFutures = GetSurvivableFutures(board.Width, board.Height, snakeBodies, healths, board.Food.ToArray(), permutation, indexOfMyself, depth);
                if (survivableFutures > 0)
                    survivableFuturesPerMove[permutation[indexOfMyself]] += survivableFutures;
            }

            foreach (var survivableFuture in survivableFuturesPerMove)
            {
                Console.WriteLine($"{survivableFuture.Key}: {survivableFuture.Value}");
            }

            return survivableFuturesPerMove.First(x => x.Value == survivableFuturesPerMove.Max(y => y.Value)).Key;
        }


        private int GetSurvivableFutures(int boardWidth, int boardHeight, GameState.BodyPartPosition[][] snakeBodies, int[] healths, GameState.FoodPosition[] foods, LegalMove[] moves, int indexOfMyself, int depth)
        {
            var boardArray = new int[boardWidth, boardHeight];
            foreach (var snake in snakeBodies)
            {
                for (int i = 0; i < snake.Length - 1; i++)
                    boardArray[snake[i].X, snake[i].Y] = 1;
            }
            foreach (var food in foods)
            {
                boardArray[food.X, food.Y] = 1337;
            }

            (var futureSnakes, var futureHealths) = gameEngine.ProcessMoves(snakeBodies, healths, boardArray, moves);

            if (futureHealths[indexOfMyself] == 0)
                return 0;

            if (depth == 0)
            {
                return 1;
            }
            else
            {
                var survivableFutures = 0;

                var survivingFutureHealths = futureHealths.Where(x => x > 0).ToArray();
                if (survivingFutureHealths.Length == 0)
                    return survivableFutures;

                var survivingFutureSnakes = new GameState.BodyPartPosition[survivingFutureHealths.Length][];
                int survivorIndex = 0;
                int newIndexOfMyself = indexOfMyself;
                for (int i = 0; i < futureSnakes.Length; i++)
                {
                    if (futureHealths[i] > 0)
                    {
                        survivingFutureSnakes[survivorIndex++] = futureSnakes[i];
                    }
                    else if (i < indexOfMyself)
                        newIndexOfMyself--;
                }


                var permutations = this.precalc.GetPossibleMovesPermutations(survivingFutureSnakes.Length);

                for (int i = 0; i < permutations.Length; i++)
                {
                    survivableFutures += GetSurvivableFutures(boardWidth, boardHeight, survivingFutureSnakes, survivingFutureHealths,
                                                              foods, permutations[i],
                                                              newIndexOfMyself, depth - 1);
                }

                return survivableFutures;
            }
        }

        public void End(GameState gameState)
        {
        }
    }
}
