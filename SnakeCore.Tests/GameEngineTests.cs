using NUnit.Framework;
using SnakeCore.Web;
using System;
using System.Collections.Generic;
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
                Snakes = new List<Snake>
                {
                    new Snake(),
                    new Snake()
                }
            };

            var moves = new LegalMove[] { LegalMove.Down, LegalMove.Down, LegalMove.Down };

            var exception = Assert.Throws<Exception>(() => gameEngine.ProcessMoves(board, moves));
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

            var moves = new LegalMove[] { LegalMove.Down, LegalMove.Right };

            var newBoard = this.gameEngine.ProcessMoves(board, moves);

            Assert.That(newBoard, Is.Not.Null);
            Assert.That(newBoard.Snakes, Is.Not.Null);
            Assert.That(newBoard.Snakes.Count, Is.EqualTo(2));

            Assert.That(newBoard.Snakes[0].Body, Is.EqualTo(new List<BodyPartPosition> { new BodyPartPosition(1, 3), new BodyPartPosition(1, 2), new BodyPartPosition(1, 2) }));
            Assert.That(newBoard.Snakes[1].Body, Is.EqualTo(new List<BodyPartPosition> { new BodyPartPosition(4, 2), new BodyPartPosition(3, 2), new BodyPartPosition(3, 2) }));
        }


        [Test]
        public void ProcessMoves_MoveIntoUpperWallWithFullHealth_SnakeGetsZeroHealth()
        {
            var board = TestCases.Test01().Board;

            var moves = new LegalMove[] { LegalMove.Up };

            var newBoard = this.gameEngine.ProcessMoves(board, moves);

            Assert.That(newBoard.Snakes[0].Health, Is.EqualTo(0));
        }


        [Test]
        public void ProcessMoves_MoveIntoOwnBodyWithFullHealth_SnakeGetsZeroHealth()
        {
            var board = TestCases.Test02().Board;

            var moves = new LegalMove[] { LegalMove.Up };

            var newBoard = this.gameEngine.ProcessMoves(board, moves);

            Assert.That(newBoard.Snakes[0].Health, Is.EqualTo(0));
        }


        [Test]
        public void ProcessMoves_MoveIntoOtherSnakeBodyWithFullHealth_SnakeGetsZeroHealth()
        {
            var board = TestCases.Test03().Board;

            var moves = new LegalMove[] { LegalMove.Up, LegalMove.Up };

            var newBoard = this.gameEngine.ProcessMoves(board, moves);

            Assert.That(newBoard.Snakes[0].Health, Is.EqualTo(0));
        }


        [Test]
        public void ProcessMoves_HeadOnCollisionWithFullHealth_ShortestSnakeGetsZeroHealth()
        {
            var board = TestCases.Test04().Board;

            var moves = new LegalMove[] { LegalMove.Right, LegalMove.Up };

            var newBoard = this.gameEngine.ProcessMoves(board, moves);

            Assert.That(newBoard.Snakes[0].Health, Is.EqualTo(100));
            Assert.That(newBoard.Snakes[1].Health, Is.EqualTo(0));
        }
    }
}
