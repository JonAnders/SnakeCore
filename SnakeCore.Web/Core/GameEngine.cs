using System;
using System.Linq;

namespace SnakeCore.Web
{
    public class GameEngine
    {
        public GameState.BoardData ProcessMoves(GameState.BoardData board, LegalMove[] moves)
        {
            if (board.Snakes.Count != moves.Length)
                throw new Exception($"{moves.Length} moves was provided, but the board has {board.Snakes.Count} snakes");
            
            var newBoard = new GameState.BoardData
            {
                Height = board.Height,
                Width = board.Width,
                Food = board.Food?.ToList(),
                Snakes = board.Snakes?
                        .Select(x => new GameState.Snake
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Health = x.Health,
                            Body = x.Body?.ToList()
                        })
                        .ToList()
            };

            for (int i = 0; i < newBoard.Snakes.Count; i++)
            {
                var snake = newBoard.Snakes[i];

                var oldHead = snake.Body[0];
                var newHead = new GameState.BodyPartPosition(-1, -1);

                if (moves[i] == LegalMove.Up)
                    newHead = new GameState.BodyPartPosition(oldHead.X, oldHead.Y - 1);
                else if (moves[i] == LegalMove.Down)
                    newHead = new GameState.BodyPartPosition(oldHead.X, oldHead.Y + 1);
                else if (moves[i] == LegalMove.Left)
                    newHead = new GameState.BodyPartPosition(oldHead.X - 1, oldHead.Y);
                else if (moves[i] == LegalMove.Right)
                    newHead = new GameState.BodyPartPosition(oldHead.X + 1, oldHead.Y);

                snake.Body.Insert(0, newHead);
                snake.Body.RemoveAt(snake.Body.Count - 1);

                if (newHead.X == snake.Body[2].X && newHead.Y == snake.Body[2].Y)
                    // If going back into itself
                    snake.Health = 0;
                else if (newHead.X < 0 || newHead.X > board.Width - 1 || newHead.Y < 0 || newHead.Y > board.Height - 1)
                    // If colliding with wall
                    snake.Health = 0;
            }

            for (int i = 0; i < newBoard.Snakes.Count; i++)
            {
                var snake = newBoard.Snakes[i];
                if (snake.Health == 0)
                    break;

                for (int j = 0; j < newBoard.Snakes.Count; j++)
                {
                    var otherSnake = newBoard.Snakes[j];

                    if (i != j)
                    {
                        if (snake.Body[0].X == otherSnake.Body[0].X && snake.Body[0].Y == otherSnake.Body[0].Y)
                        {
                            // Head on collision
                            if (snake.Body.Count < otherSnake.Body.Count)
                            {
                                snake.Health = 0;
                                break;
                            }
                            else if (snake.Body.Count > otherSnake.Body.Count)
                            {
                                otherSnake.Health = 0;
                            }
                            else
                            {
                                snake.Health = otherSnake.Health = 0;
                                break;
                            }
                        }
                    }

                    for (int k = 1; k < otherSnake.Body.Count; k++)
                    {
                        if (snake.Body[0].X == otherSnake.Body[k].X && snake.Body[0].Y == otherSnake.Body[k].Y)
                            // If colliding with the body of another snake
                            snake.Health = 0;
                    }
                }
            }

            return newBoard;
        }
    }
}
