using System.Collections.Generic;
using System.Linq;

namespace SnakeCore.Web.Brains
{
    /// <summary>
    /// This brain implements the flood fill algorithm.
    /// It will keep away from walls and usually other snakes as well,
    /// but there are cases where it will loose against an unresponsive snake timing out.
    /// (Both moving into the same position at the same time)
    /// When no other snakes are interfering, it will loop up and down in the two columns to the right until starvation.
    /// The simplicity and fast performance of this algorithm is interesting, but it is not very good on its own.
    /// </summary>
    public class Floody : IBrain
    {
        public Battlesnake GetBattlesnake()
        {
            return new Battlesnake
            {
                Color = "#2299cc",
                Head = "ski",
                Tail = "curled"
            };
        }


        public void Start(GameState gameState)
        {
        }


        public LegalMove Move(GameState gameState)
        {
            var board = new int[gameState.Board.Width, gameState.Board.Height];
            foreach (var snake in gameState.Board.Snakes)
            {
                // Remove the tail of all snakes, as it will have moved in the next round
                for (int i = 0; i < snake.Body.Count - 1; i++)
                {
                    board[snake.Body[i].X, snake.Body[i].Y] = 1;
                }
            }

            var moveAreas = new Dictionary<LegalMove, int>
            {
                { LegalMove.Up, FillMaxArea((int[,])board.Clone(), gameState.You.Body[0].X, gameState.You.Body[0].Y + 1) },
                { LegalMove.Right, FillMaxArea((int[,])board.Clone(), gameState.You.Body[0].X + 1, gameState.You.Body[0].Y) },
                { LegalMove.Down, FillMaxArea((int[,])board.Clone(), gameState.You.Body[0].X, gameState.You.Body[0].Y - 1) },
                { LegalMove.Left, FillMaxArea((int[,])board.Clone(), gameState.You.Body[0].X - 1, gameState.You.Body[0].Y) },
            };

            return moveAreas
                .OrderByDescending(x => x.Value)
                .First()
                .Key;
        }


        private int FillMaxArea(int[,] board, int x, int y)
        {
            if (x < 0 || x >= board.GetLength(0) || y < 0 || y >= board.GetLength(1))
                return 0;

            if (board[x, y] > 0)
                return 0;

            // Mark visited board positions.
            // Value doesn't matter, as long as it's higher than 1.
            board[x, y] = 42;

            return 1 +
                FillMaxArea(board, x, y + 1) +
                FillMaxArea(board, x + 1, y) +
                FillMaxArea(board, x, y - 1) +
                FillMaxArea(board, x - 1, y);
        }


        public void End(GameState gameState)
        {
        }
    }
}
