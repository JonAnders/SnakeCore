using System;

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
                                new GameState.BodyPartPosition(2, 4),
                                new GameState.BodyPartPosition(2, 3),
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
                                new GameState.BodyPartPosition(2, 1),
                                new GameState.BodyPartPosition(3, 1),
                                new GameState.BodyPartPosition(3, 2),
                                new GameState.BodyPartPosition(3, 3),
                                new GameState.BodyPartPosition(2, 3),
                                new GameState.BodyPartPosition(1, 3)
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
                                new GameState.BodyPartPosition(3, 3),
                                new GameState.BodyPartPosition(2, 3),
                                new GameState.BodyPartPosition(1, 3),
                                new GameState.BodyPartPosition(1, 2),
                                new GameState.BodyPartPosition(1, 1)
                            },
                            Health = 100
                        }
                    },
                    Food = new List<GameState.FoodPosition>()
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
                                new GameState.BodyPartPosition(1, 3),
                                new GameState.BodyPartPosition(1, 2),
                                new GameState.BodyPartPosition(1, 1),
                                new GameState.BodyPartPosition(1, 0)
                            },
                            Health = 100
                        },
                        new GameState.Snake
                        {
                            Id = "2",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(2, 2),
                                new GameState.BodyPartPosition(2, 1),
                                new GameState.BodyPartPosition(2, 0)
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
                                new GameState.BodyPartPosition(3, 1),
                                new GameState.BodyPartPosition(3, 0),
                                new GameState.BodyPartPosition(4, 0),
                                new GameState.BodyPartPosition(4, 1),
                                new GameState.BodyPartPosition(4, 2),
                                new GameState.BodyPartPosition(4, 3),
                                new GameState.BodyPartPosition(3, 3),
                                new GameState.BodyPartPosition(2, 3),
                                new GameState.BodyPartPosition(2, 2),
                                new GameState.BodyPartPosition(1, 2),
                                new GameState.BodyPartPosition(0, 2)
                            },
                            Health = 100
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
                                new GameState.BodyPartPosition(2, 4),
                                new GameState.BodyPartPosition(2, 3),
                                new GameState.BodyPartPosition(2, 2)
                            }
                        },
                        new GameState.Snake
                        {
                            Id = "2",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(1, 3),
                                new GameState.BodyPartPosition(1, 2),
                                new GameState.BodyPartPosition(1, 1)
                            }
                        }
                    },
                    Food = new List<GameState.FoodPosition>
                    {
                        new GameState.FoodPosition(0, 4)
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
                                new GameState.BodyPartPosition(4, 5),
                                new GameState.BodyPartPosition(4, 4),
                                new GameState.BodyPartPosition(4, 3),
                                new GameState.BodyPartPosition(4, 2),
                                new GameState.BodyPartPosition(4, 1),
                                new GameState.BodyPartPosition(3, 1),
                                new GameState.BodyPartPosition(3, 2),
                                new GameState.BodyPartPosition(3, 3),
                                new GameState.BodyPartPosition(3, 4),
                                new GameState.BodyPartPosition(3, 5),
                                new GameState.BodyPartPosition(2, 5),
                                new GameState.BodyPartPosition(1, 5),
                                new GameState.BodyPartPosition(0, 5)
                            }
                        },
                        new GameState.Snake
                        {
                            Id = "2",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(3, 0),
                                new GameState.BodyPartPosition(4, 0),
                                new GameState.BodyPartPosition(5, 0)
                            }
                        }
                    },
                    Food = new List<GameState.FoodPosition>
                    {
                        new GameState.FoodPosition(0, 2)
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
                                new GameState.BodyPartPosition(2, 1),
                                new GameState.BodyPartPosition(3, 1),
                                new GameState.BodyPartPosition(3, 2),
                                new GameState.BodyPartPosition(3, 3),
                                new GameState.BodyPartPosition(2, 3)
                            },
                            Health = 100
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
                                new GameState.BodyPartPosition(2, 1),
                                new GameState.BodyPartPosition(2, 0)
                            }
                        },
                        new GameState.Snake
                        {
                            Id = "2",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(4, 3),
                                new GameState.BodyPartPosition(3, 3),
                                new GameState.BodyPartPosition(2, 3)
                            }
                        }
                    }
                }
            };

            if (includeFood)
            {
                gameState.Board.Food = new List<GameState.FoodPosition>
                    {
                        new GameState.FoodPosition(4, 4)
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
                                new GameState.BodyPartPosition(4, 1),
                                new GameState.BodyPartPosition(4, 2),
                                new GameState.BodyPartPosition(4, 3)
                            }
                        },
                        new GameState.Snake
                        {
                            Id = "2",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(3, 0),
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
                                new GameState.BodyPartPosition(3, 1),
                                new GameState.BodyPartPosition(3, 0),
                                new GameState.BodyPartPosition(4, 0),
                                new GameState.BodyPartPosition(4, 1),
                                new GameState.BodyPartPosition(4, 2),
                                new GameState.BodyPartPosition(4, 3),
                                new GameState.BodyPartPosition(4, 4),
                                new GameState.BodyPartPosition(3, 4),
                                new GameState.BodyPartPosition(2, 4),
                                new GameState.BodyPartPosition(2, 3),
                                new GameState.BodyPartPosition(2, 2),
                                new GameState.BodyPartPosition(1, 2),
                                new GameState.BodyPartPosition(0, 2)
                            },
                            Health = 100
                        }
                    }
                }
            };

            gameState.You = gameState.Board.Snakes.First();
            return gameState;
        }


        public static GameState Test12()
        {
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
                                new GameState.BodyPartPosition(1, 4),
                                new GameState.BodyPartPosition(1, 4),
                                new GameState.BodyPartPosition(1, 4)
                            },
                            Health = 100
                        },
                        new GameState.Snake
                        {
                            Id = "2",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(2, 4),
                                new GameState.BodyPartPosition(2, 4),
                                new GameState.BodyPartPosition(2, 4)
                            },
                            Health = 100
                        },
                        new GameState.Snake
                        {
                            Id = "3",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(3, 4),
                                new GameState.BodyPartPosition(3, 4),
                                new GameState.BodyPartPosition(3, 4)
                            },
                            Health = 100
                        },
                        new GameState.Snake
                        {
                            Id = "4",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(4, 4),
                                new GameState.BodyPartPosition(4, 4),
                                new GameState.BodyPartPosition(4, 4)
                            },
                            Health = 100
                        },
                        new GameState.Snake
                        {
                            Id = "5",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(1, 3),
                                new GameState.BodyPartPosition(1, 3),
                                new GameState.BodyPartPosition(1, 3)
                            },
                            Health = 100
                        },
                        new GameState.Snake
                        {
                            Id = "6",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(2, 3),
                                new GameState.BodyPartPosition(2, 3),
                                new GameState.BodyPartPosition(2, 3)
                            },
                            Health = 100
                        },
                        new GameState.Snake
                        {
                            Id = "7",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(3, 3),
                                new GameState.BodyPartPosition(3, 3),
                                new GameState.BodyPartPosition(3, 3)
                            },
                            Health = 100
                        },
                        new GameState.Snake
                        {
                            Id = "8",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(4, 3),
                                new GameState.BodyPartPosition(4, 3),
                                new GameState.BodyPartPosition(4, 3)
                            },
                            Health = 100
                        }
                    }
                }
            };

            gameState.You = gameState.Board.Snakes[1];

            return gameState;
        }

        
        public static GameState Test13()
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
                                new GameState.BodyPartPosition(1, 3),
                                new GameState.BodyPartPosition(1, 2),
                                new GameState.BodyPartPosition(1, 1)
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
                                new GameState.BodyPartPosition(2, 4),
                                new GameState.BodyPartPosition(1, 4),
                                new GameState.BodyPartPosition(0, 4),
                                new GameState.BodyPartPosition(0, 3)
                            },
                            Health = 100
                        }
                    }
                }
            };

            gameState.You = gameState.Board.Snakes.First();
            return gameState;
        }


        public static GameState Test14()
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
                                new GameState.BodyPartPosition(1, 3),
                                new GameState.BodyPartPosition(1, 2),
                                new GameState.BodyPartPosition(1, 1),
                                new GameState.BodyPartPosition(2, 1),
                                new GameState.BodyPartPosition(3, 1),
                                new GameState.BodyPartPosition(3, 2)
                            }
                        }
                    }
                }
            };

            gameState.You = gameState.Board.Snakes.First();
            gameState.You.Health = 100;

            return gameState;
        }


        public static GameState Test15()
        {
            // Note: This test is a remake of an actual game that was lost by Brainiac
            // The other snake was non-responsive and was only going straight up.
            // https://play.battlesnake.com/g/a74a7df6-4ca5-4125-a636-913faef789e9/

            var gameState = new GameState
            {
                Board = new GameState.BoardData
                {
                    Height = 7,
                    Width = 7,
                    Snakes = new List<GameState.Snake>
                    {
                        new GameState.Snake
                        {
                            Id = "1",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(3, 6),
                                new GameState.BodyPartPosition(2, 6),
                                new GameState.BodyPartPosition(1, 6)
                            },
                            Health = 93
                        },
                        new GameState.Snake
                        {
                            Id = "2",
                            Body = new List<GameState.BodyPartPosition>
                            {
                                new GameState.BodyPartPosition(5, 4),
                                new GameState.BodyPartPosition(5, 3),
                                new GameState.BodyPartPosition(5, 2),
                                new GameState.BodyPartPosition(5, 1)
                            },
                            Health = 97
                        }
                    },
                    Food = new List<GameState.FoodPosition>
                    {
                        new GameState.FoodPosition(4, 6)
                    }
                }
            };

            gameState.You = gameState.Board.Snakes.First();
            return gameState;
        }


        public static GameState Test16()
        {
            // https://play.battlesnake.com/g/ecc50070-5537-4bb8-b732-e38a3c122f9b/

            throw new NotImplementedException();
        }


        public static GameState Test17()
        {
            // https://play.battlesnake.com/g/98de8170-268d-4d6f-99da-42af21c993ed/

            throw new NotImplementedException();
        }


        public static GameState Test18()
        {
            // https://play.battlesnake.com/g/8bcf672a-ad03-4c71-b5d3-184edbeb7b56/

            throw new NotImplementedException();
        }


        public static GameState Test19()
        {
            // https://play.battlesnake.com/g/e2848d8d-62e7-44fe-a7d8-a8b7ab5054be/

            throw new NotImplementedException();
        }
    }
}
