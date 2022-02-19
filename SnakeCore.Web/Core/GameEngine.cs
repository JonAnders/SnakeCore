using System;
using System.Linq;

namespace SnakeCore.Web
{
    public class GameEngine
    {
        public (GameState.BodyPartPosition[][], int[]) ProcessMoves(GameState.BodyPartPosition[][] snakeBodies, int[] healths, int[,] boardArray, LegalMove[] moves)
        {
            if (snakeBodies.Length != moves.Length)
                throw new Exception($"{moves.Length} moves was provided, but the board has {snakeBodies.Length} snakes");

            var futureSnakeBodies = new GameState.BodyPartPosition[snakeBodies.Length][];
            var futureHealths = new int[healths.Length];

            for (int i = 0; i < futureSnakeBodies.Length; i++)
            {
                var snakeBody = snakeBodies[i];

                var oldHead = snakeBody[0];
                var newHead = new GameState.BodyPartPosition(-1, -1);

                if (moves[i] == LegalMove.Up)
                    newHead = new GameState.BodyPartPosition(oldHead.X, oldHead.Y + 1);
                else if (moves[i] == LegalMove.Down)
                    newHead = new GameState.BodyPartPosition(oldHead.X, oldHead.Y - 1);
                else if (moves[i] == LegalMove.Left)
                    newHead = new GameState.BodyPartPosition(oldHead.X - 1, oldHead.Y);
                else if (moves[i] == LegalMove.Right)
                    newHead = new GameState.BodyPartPosition(oldHead.X + 1, oldHead.Y);

                var futureSnakeBody = new GameState.BodyPartPosition[snakeBody.Length];
                futureSnakeBody[0] = newHead;
                Array.Copy(snakeBody, 0, futureSnakeBody, 1, snakeBody.Length - 1);
                futureSnakeBodies[i] = futureSnakeBody;
                
                var futureHealth = healths[i];
                if (snakeBody.Length > 2 && newHead.X == snakeBody[2].X && newHead.Y == snakeBody[2].Y)
                    // If going back into itself
                    futureHealth = 0;
                else if (newHead.X < 0 || newHead.X > boardArray.GetLength(0) - 1 || newHead.Y < 0 || newHead.Y > boardArray.GetLength(1) - 1)
                    // If colliding with wall
                    futureHealth = 0;
                else if (boardArray[newHead.X, newHead.Y] > 0)
                    // If colliding with the body of another snake
                    futureHealth = 0;

                futureHealths[i] = futureHealth;
            }

            for (int i = 0; i < futureSnakeBodies.Length; i++)
            {
                var futureSnakeBody = futureSnakeBodies[i];
                if (futureHealths[i] == 0)
                    break;

                for (int j = i + 1; j < futureSnakeBodies.Length; j++)
                {
                    var otherSnake = futureSnakeBodies[j];

                    if (futureSnakeBody[0].X == otherSnake[0].X && futureSnakeBody[0].Y == otherSnake[0].Y)
                    {
                        // Head on collision
                        if (futureSnakeBody.Length < otherSnake.Length)
                        {
                            futureHealths[i] = 0;
                            break;
                        }
                        else if (futureSnakeBody.Length > otherSnake.Length)
                        {
                            futureHealths[j] = 0;
                        }
                        else
                        {
                            futureHealths[i] = futureHealths[j] = 0;
                            break;
                        }
                    }
                }
            }

            return (futureSnakeBodies, futureHealths);
        }
    }
}
