using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            var serviceProvider = new ServiceCollection()
                .AddLogging(x => x.AddConsole().SetMinimumLevel(LogLevel.Debug))
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger<Brainiac>();

            this.brain = new Brainiac(logger);
        }


        [Test]
        public void Test01()
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
                            Id = "1",
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
        public void Test02()
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
                            Id = "1",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(2, 2),
                                new GameState.BodyPartPosition(2, 3),
                                new GameState.BodyPartPosition(3, 3),
                                new GameState.BodyPartPosition(3, 2),
                                new GameState.BodyPartPosition(3, 1),
                                new GameState.BodyPartPosition(2, 1),
                                new GameState.BodyPartPosition(1, 1)
                            }
                        }
                    }
                }
            };

            gameState.You = gameState.Board.Snakes.First();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Left));
        }


        [Test]
        public void Test03()
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
                            Id = "1",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(2, 2),
                                new GameState.BodyPartPosition(3, 2),
                                new GameState.BodyPartPosition(4, 2)
                            }
                        },
                        new GameState.Snake
                        {
                            Id = "2",
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
        public void Test04()
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
                            Id = "1",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(1, 1),
                                new GameState.BodyPartPosition(1, 2),
                                new GameState.BodyPartPosition(1, 3),
                                new GameState.BodyPartPosition(1, 4)
                            }
                        },
                        new GameState.Snake
                        {
                            Id = "2",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(2, 2),
                                new GameState.BodyPartPosition(2, 3),
                                new GameState.BodyPartPosition(2, 4)
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
        public void Test05()
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
                            Id = "1",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(3, 3),
                                new GameState.BodyPartPosition(3, 4),
                                new GameState.BodyPartPosition(4, 4),
                                new GameState.BodyPartPosition(4, 3),
                                new GameState.BodyPartPosition(4, 2),
                                new GameState.BodyPartPosition(4, 1),
                                new GameState.BodyPartPosition(3, 1),
                                new GameState.BodyPartPosition(2, 1),
                                new GameState.BodyPartPosition(2, 2),
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
        public string Test06(int hp)
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
                            Id = "1",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(2, 0),
                                new GameState.BodyPartPosition(2, 1),
                                new GameState.BodyPartPosition(2, 2)
                            }
                        },
                        new GameState.Snake
                        {
                            Id = "2",
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


        [Test]
        public void Test07()
        {
            // Note: This test is a remake of an actual game that was lost
            var gameState = new GameState
            {
                Board = new GameState.BoardData
                {
                    Height = 6,
                    Width = 6,
                    Snakes = new List<GameState.Snake>
                    {
                        new GameState.Snake
                        {
                            Id = "1",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(4, 0),
                                new GameState.BodyPartPosition(4, 1),
                                new GameState.BodyPartPosition(4, 2),
                                new GameState.BodyPartPosition(4, 4),
                                new GameState.BodyPartPosition(3, 4),
                                new GameState.BodyPartPosition(3, 3),
                                new GameState.BodyPartPosition(3, 2),
                                new GameState.BodyPartPosition(3, 1),
                                new GameState.BodyPartPosition(3, 0),
                                new GameState.BodyPartPosition(2, 0),
                                new GameState.BodyPartPosition(1, 0),
                                new GameState.BodyPartPosition(0, 0)
                            }
                        },
                        new GameState.Snake
                        {
                            Id = "2",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(3, 5),
                                new GameState.BodyPartPosition(4, 5),
                                new GameState.BodyPartPosition(5, 5)
                            }
                        }
                    },
                    Food = new List<GameState.FoodPosition>
                    {
                        new GameState.FoodPosition(0, 3)
                    }
                }
            };

            gameState.You = gameState.Board.Snakes.First();
            gameState.You.Health = 95;

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Right));
        }


        [Test]
        public void Test08()
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
                            Id = "1",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(2, 2),
                                new GameState.BodyPartPosition(2, 3),
                                new GameState.BodyPartPosition(3, 3),
                                new GameState.BodyPartPosition(3, 2),
                                new GameState.BodyPartPosition(3, 1),
                                new GameState.BodyPartPosition(2, 1)
                            }
                        }
                    }
                }
            };

            gameState.You = gameState.Board.Snakes.First();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Up));
        }


        [TestCase(true, ExpectedResult = "right")]
        [TestCase(false, ExpectedResult = "up")]
        public string Test09(bool includeFood)
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
                            Id = "1",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(2, 2),
                                new GameState.BodyPartPosition(2, 3),
                                new GameState.BodyPartPosition(2, 4)
                            }
                        },
                        new GameState.Snake
                        {
                            Id = "2",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(4, 1),
                                new GameState.BodyPartPosition(3, 1),
                                new GameState.BodyPartPosition(2, 1)
                            }
                        }
                    }
                }
            };

            if (includeFood) {
                gameState.Board.Food = new List<GameState.FoodPosition>
                    {
                        new GameState.FoodPosition(4, 0)
                    };
            }

            gameState.You = gameState.Board.Snakes.First();
            gameState.You.Health = 50;

            var move = this.brain.Move(gameState);

            return move.Move;
        }


        [Test]
        public void Test10()
        {
            // Note: This test is a remake of an actual game that was lost
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
                            Id = "1",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(4, 3),
                                new GameState.BodyPartPosition(4, 2),
                                new GameState.BodyPartPosition(4, 1)
                            }
                        },
                        new GameState.Snake
                        {
                            Id = "2",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(3, 4),
                                new GameState.BodyPartPosition(3, 3),
                                new GameState.BodyPartPosition(2, 3)
                            }
                        }
                    }
                }
            };

            gameState.You = gameState.Board.Snakes.First();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Down));
        }
    }
}