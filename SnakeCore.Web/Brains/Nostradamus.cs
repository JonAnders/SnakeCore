using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeCore.Web.Brains
{
    public class Nostradamus : IBrain
    {
        private readonly GameEngine gameEngine;


        public Nostradamus()
        {
            this.gameEngine = new GameEngine();
        }

        public StartResponse Start(GameState gameState)
        {
            return new StartResponse
            {
                Color = "#7788aa",
                HeadType = "silly",
                TailType = "freckled"
            };
        }


        public LegalMove Move(GameState gameState)
        {
            var indexOfMyself = gameState.Board.Snakes.IndexOf(gameState.Board.Snakes.First(x => x.Id == gameState.You.Id));
            var move = GetMostSurvivableMove(gameState.Board, indexOfMyself);

            return move;
        }

        private LegalMove GetMostSurvivableMove(GameState.BoardData board, int indexOfMyself)
        {
            var boardArray = new int[board.Width, board.Height];
            foreach (var snake in board.Snakes)
            {
                foreach (var bodyPartPosition in snake.Body)
                    boardArray[bodyPartPosition.X, bodyPartPosition.Y] = 1;
            }

            var snakeBodies = board.Snakes.Select(x => x.Body.ToArray()).ToArray();
            var healths = board.Snakes.Select(x => x.Health).ToArray();

            var permutations = GetPossibleMovesPermutations(board.Snakes.Count);

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

        public void End(GameState gameState)
        {
        }

        public LegalMove[][] GetPossibleMovesPermutations(int numSnakes)
        {
            var allLegalMoves = new LegalMove[] { LegalMove.Up, LegalMove.Down, LegalMove.Left, LegalMove.Right };

            if (numSnakes == 1)
            {
                var moves = new LegalMove[4][];

                for (int i = 0; i < allLegalMoves.Length; i++)
                {
                    moves[i] = new LegalMove[] { allLegalMoves[i] };
                }

                return moves;
            }
            else
            {
                int numPossibleMoves = (int)Math.Pow(4, numSnakes);
                var moves = new LegalMove[numPossibleMoves][];

                for (int i = 0; i < allLegalMoves.Length; i++)
                {
                    var possibleMoves = GetPossibleMovesPermutations(numSnakes - 1);

                    for (int j = 0; j < possibleMoves.Length; j++)
                    {
                        var possibleMove = possibleMoves[j];
                        var newPossibleFuture = new LegalMove[possibleMove.Length + 1];
                        newPossibleFuture[0] = allLegalMoves[i];
                        possibleMove.CopyTo(newPossibleFuture, 1);
                        moves[(i * possibleMoves.Length) + j] = newPossibleFuture;
                    }
                }

                return moves;
            }
        }
    }
}
