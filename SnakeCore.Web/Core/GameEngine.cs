﻿using System;

namespace SnakeCore.Web
{
    public class GameEngine
    {
        public GameState.BoardData ProcessMoves(GameState.BoardData board, LegalMove[] moves)
        {
            if (board.Snakes.Count != moves.Length)
                throw new Exception($"{moves.Length} moves was provided, but the board has {board.Snakes.Count} snakes");

            GameState.BoardData newBoard = board.Copy();

            for (int i = 0; i < newBoard.Snakes.Count; i++)
            {
                var snake = newBoard.Snakes[i];

                var newHead = new GameState.BodyPartPosition(snake.Body[0].X, snake.Body[0].Y);

                if (moves[i] == LegalMove.Up)
                    newHead.Y--;
                else if (moves[i] == LegalMove.Down)
                    newHead.Y++;
                else if (moves[i] == LegalMove.Left)
                    newHead.X--;
                else if (moves[i] == LegalMove.Right)
                    newHead.X++;

                snake.Body.Insert(0, newHead);
                snake.Body.RemoveAt(snake.Body.Count - 1);
            }

            return newBoard;
        }
    }
}