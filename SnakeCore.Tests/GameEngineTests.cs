using NUnit.Framework;
using SnakeCore.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static SnakeCore.Web.GameState;

namespace SnakeCore.Tests
{
    [TestFixture]
    public class GameEngineTests
    {
        private readonly GameEngine gameEngine;

        public GameEngineTests()
        {
            this.gameEngine = new GameEngine();
        }

        [Test]
        public void ProcessMoves_WrongNumberOfMoves_ThrowsException()
        {
            var board = new BoardData
            {
                Height = 5,
                Width = 5,
                Snakes = new List<Snake>
                {
                    new Snake { Body = new List<BodyPartPosition> { new BodyPartPosition(1, 2), new BodyPartPosition(1, 2) , new BodyPartPosition(1, 2) }},
                    new Snake { Body = new List<BodyPartPosition> { new BodyPartPosition(3, 2), new BodyPartPosition(3, 2) , new BodyPartPosition(3, 2) }}
                }
            };
            int[,] boardArray = GetBoardArray(board);
            var snakeBodies = GetSnakeBodies(board);
            var healths = GetHealths(board);

            var moves = new LegalMove[] { LegalMove.Down, LegalMove.Down, LegalMove.Down };

            var exception = Assert.Throws<Exception>(() => gameEngine.ProcessMoves(snakeBodies, healths, boardArray, moves));
            Assert.That(exception.Message, Is.EqualTo($"3 moves was provided, but the board has 2 snakes"));
        }


        [Test]
        public void ProcessMoves_TwoSnakesAndMoves_HeadsAreUpdatedCorrectly()
        {
            var board = new BoardData
            {
                Height = 5,
                Width = 5,
                Snakes = new List<Snake>
                {
                    new Snake { Body = new List<BodyPartPosition> { new BodyPartPosition(1, 2), new BodyPartPosition(1, 2) , new BodyPartPosition(1, 2) }},
                    new Snake { Body = new List<BodyPartPosition> { new BodyPartPosition(3, 2), new BodyPartPosition(3, 2) , new BodyPartPosition(3, 2) }}
                }
            };
            int[,] boardArray = GetBoardArray(board);
            var snakeBodies = GetSnakeBodies(board);
            var healths = GetHealths(board);

            var moves = new LegalMove[] { LegalMove.Down, LegalMove.Right };

            (var futureSnakes, var futureHealths) = this.gameEngine.ProcessMoves(snakeBodies, healths, boardArray, moves);
            
            Assert.That(futureSnakes, Is.Not.Null);
            Assert.That(futureSnakes.Count, Is.EqualTo(2));

            Assert.That(futureSnakes[0], Is.EqualTo(new BodyPartPosition[] { new BodyPartPosition(1, 3), new BodyPartPosition(1, 2), new BodyPartPosition(1, 2) }));
            Assert.That(futureSnakes[1], Is.EqualTo(new BodyPartPosition[] { new BodyPartPosition(4, 2), new BodyPartPosition(3, 2), new BodyPartPosition(3, 2) }));
        }


        [Test]
        public void ProcessMoves_MoveIntoUpperWallWithFullHealth_SnakeGetsZeroHealth()
        {
            var board = TestCases.Test01().Board;
            int[,] boardArray = GetBoardArray(board);
            var snakeBodies = GetSnakeBodies(board);
            var healths = GetHealths(board);

            var moves = new LegalMove[] { LegalMove.Up };

            (var futureSnakes, var futureHealths) = this.gameEngine.ProcessMoves(snakeBodies, healths, boardArray, moves);

            Assert.That(futureHealths[0], Is.EqualTo(0));
        }


        [Test]
        public void ProcessMoves_MoveIntoOwnBodyWithFullHealth_SnakeGetsZeroHealth()
        {
            var board = TestCases.Test02().Board;
            int[,] boardArray = GetBoardArray(board);
            var snakeBodies = GetSnakeBodies(board);
            var healths = GetHealths(board);

            var moves = new LegalMove[] { LegalMove.Up };

            (var futureSnakes, var futureHealths) = this.gameEngine.ProcessMoves(snakeBodies, healths, boardArray, moves);

            Assert.That(futureHealths[0], Is.EqualTo(0));
        }


        [Test]
        public void ProcessMoves_MoveIntoOtherSnakeBodyWithFullHealth_SnakeGetsZeroHealth()
        {
            var board = TestCases.Test03().Board;
            int[,] boardArray = GetBoardArray(board);
            var snakeBodies = GetSnakeBodies(board);
            var healths = GetHealths(board);

            var moves = new LegalMove[] { LegalMove.Up, LegalMove.Up };

            (var futureSnakes, var futureHealths) = this.gameEngine.ProcessMoves(snakeBodies, healths, boardArray, moves);

            Assert.That(futureHealths[0], Is.EqualTo(0));
        }


        [Test]
        public void ProcessMoves_HeadOnCollisionWithFullHealth_ShortestSnakeGetsZeroHealth()
        {
            var board = TestCases.Test04().Board;
            int[,] boardArray = GetBoardArray(board);
            var snakeBodies = GetSnakeBodies(board);
            var healths = GetHealths(board);

            var moves = new LegalMove[] { LegalMove.Right, LegalMove.Up };

            (var futureSnakes, var futureHealths) = this.gameEngine.ProcessMoves(snakeBodies, healths, boardArray, moves);

            Assert.That(futureHealths[0], Is.EqualTo(100));
            Assert.That(futureHealths[1], Is.EqualTo(0));
        }


        [Test]
        public void ProcessMoves_EightSnakes_IsNotTooSlow()
        {
            var board = TestCases.Test12().Board;
            int[,] boardArray = GetBoardArray(board);
            var snakeBodies = GetSnakeBodies(board);
            var healths = GetHealths(board);

            var moves = new LegalMove[] { LegalMove.Left, LegalMove.Up, LegalMove.Up, LegalMove.Right, LegalMove.Left, LegalMove.Down, LegalMove.Down, LegalMove.Right };

            BodyPartPosition[][] futureSnakes = null;
            int[] futureHealths = null;
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 65536; i++)
            {
                (futureSnakes, futureHealths) = this.gameEngine.ProcessMoves(snakeBodies, healths, boardArray, moves);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(100));

            Assert.That(futureSnakes.Count, Is.EqualTo(8));
            Assert.That(futureHealths.All(x => x == 100), Is.True);
        }


        private static int[,] GetBoardArray(BoardData board)
        {
            var boardArray = new int[board.Width, board.Height];
            foreach (var snake in board.Snakes)
            {
                foreach (var bodyPartPosition in snake.Body)
                    boardArray[bodyPartPosition.X, bodyPartPosition.Y] = 1;
            }

            return boardArray;
        }


        private static BodyPartPosition[][] GetSnakeBodies(BoardData board)
        {
            return board.Snakes.Select(x => x.Body.ToArray()).ToArray();
        }


        private static int[] GetHealths(BoardData board)
        {
            return board.Snakes.Select(x => x.Health).ToArray();
        }
    }
}
