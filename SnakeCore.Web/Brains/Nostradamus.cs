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
            var move = GetMostSurvivableMove(gameState.Board, indexOfMyself);

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

        public void End(GameState gameState)
        {
        }
    }
}
