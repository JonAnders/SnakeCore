using SnakeCore.Web;
using System.Collections.Generic;
using System.Linq;

namespace SnakeCore.Tests
{
    public static class TestCases
    {
        public static GameState Test01()
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
            gameState.You.Health = 100;

            return gameState;
        }


        public static GameState Test02()
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
            gameState.You.Health = 100;

            return gameState;
        }


        public static GameState Test03()
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
                            },
                            Health = 100
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
                            },
                            Health = 100
                        }
                    }
                }
            };

            gameState.You = gameState.Board.Snakes.First();
            return gameState;
        }


        public static GameState Test04()
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
                            },
                            Health = 100
                        },
                        new GameState.Snake
                        {
                            Id = "2",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(2, 2),
                                new GameState.BodyPartPosition(2, 3),
                                new GameState.BodyPartPosition(2, 4)
                            },
                            Health = 100
                        }
                    }
                }
            };

            gameState.You = gameState.Board.Snakes.First();
            return gameState;
        }


        public static GameState Test05()
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
            return gameState;
        }


        public static GameState Test06()
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
            return gameState;
        }


        public static GameState Test07()
        {
            // Note: This test case is a remake of an actual game that was lost
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
            return gameState;
        }


        public static GameState Test08()
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
            return gameState;
        }


        public static GameState Test09(bool includeFood)
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

            if (includeFood)
            {
                gameState.Board.Food = new List<GameState.FoodPosition>
                    {
                        new GameState.FoodPosition(4, 0)
                    };
            }

            gameState.You = gameState.Board.Snakes.First();
            return gameState;
        }


        public static GameState Test10()
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
            return gameState;
        }


        public static GameState Test11()
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
                                new GameState.BodyPartPosition(4, 0),
                                new GameState.BodyPartPosition(3, 0),
                                new GameState.BodyPartPosition(2, 0),
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
            return gameState;
        }
    }
}
