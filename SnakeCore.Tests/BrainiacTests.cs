using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using SnakeCore.Web;
using SnakeCore.Web.Brains;

namespace SnakeCore.Tests
{
    public class BrainiacTests
    {
        private IBrain brain;

        [SetUp]
        public void Setup()
        {
            this.brain = new Brainiac();
        }


        [Test]
        public void Test1()
        {
            var gameState = new GameState
            {
                Board = new GameState.BoardData
                {
                    Height = 5,
                    Width = 5,
                    Snakes = new List<GameState.Snake>
                    {
                        new GameState.Snake
                        {
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(2, 0),
                                new GameState.BodyPartPosition(2, 1),
                                new GameState.BodyPartPosition(2, 2)
                            }
                        }
                    }
                }
            };

            gameState.You = gameState.Board.Snakes.First();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Right));
        }


        [Test]
        public void Test2()
        {
            var gameState = new GameState
            {
                Board = new GameState.BoardData
                {
                    Height = 5,
                    Width = 5,
                    Snakes = new List<GameState.Snake>
                    {
                        new GameState.Snake
                        {
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(2, 2),
                                new GameState.BodyPartPosition(3, 2),
                                new GameState.BodyPartPosition(4, 2)
                            }
                        },
                        new GameState.Snake
                        {
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(3, 1),
                                new GameState.BodyPartPosition(2, 1),
                                new GameState.BodyPartPosition(1, 1),
                                new GameState.BodyPartPosition(1, 2),
                                new GameState.BodyPartPosition(1, 3)
                            }
                        }
                    }
                }
            };

            gameState.You = gameState.Board.Snakes.First();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Down));
        }


        [Test]
        public void Test3()
        {
            var gameState = new GameState
            {
                Board = new GameState.BoardData
                {
                    Height = 5,
                    Width = 5,
                    Snakes = new List<GameState.Snake>
                    {
                        new GameState.Snake
                        {
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(2, 3),
                                new GameState.BodyPartPosition(2, 4),
                                new GameState.BodyPartPosition(3, 4),
                                new GameState.BodyPartPosition(3, 3),
                                new GameState.BodyPartPosition(3, 2),
                                new GameState.BodyPartPosition(3, 1),
                                new GameState.BodyPartPosition(2, 1),
                                new GameState.BodyPartPosition(1, 1),
                                new GameState.BodyPartPosition(1, 2),
                                new GameState.BodyPartPosition(0, 2)
                            }
                        }
                    }
                }
            };

            gameState.You = gameState.Board.Snakes.First();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Left));
        }


        [TestCase(90, ExpectedResult = "right")]
        [TestCase(10, ExpectedResult = "left")]
        public string Test4(int hp)
        {
            var gameState = new GameState
            {
                Board = new GameState.BoardData
                {
                    Height = 5,
                    Width = 5,
                    Snakes = new List<GameState.Snake>
                    {
                        new GameState.Snake
                        {
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(2, 0),
                                new GameState.BodyPartPosition(2, 1),
                                new GameState.BodyPartPosition(2, 2)
                            }
                        },
                        new GameState.Snake
                        {
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(1, 1),
                                new GameState.BodyPartPosition(1, 2),
                                new GameState.BodyPartPosition(1, 3)
                            }
                        }
                    },
                    Food = new List<GameState.FoodPosition>
                    {
                        new GameState.FoodPosition(0, 0)
                    }
                }
            };

            gameState.You = gameState.Board.Snakes.First();
            gameState.You.Health = hp;

            var move = this.brain.Move(gameState);

            return move.Move;
        }
    }
}