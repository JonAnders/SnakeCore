using System.Collections.Generic;
using System.Linq;

namespace SnakeCore.Web
{
    public class GameState
    {
        public GameData Game;
        public int Turn;
        public BoardData Board;
        public Snake You;

        public class GameData
        {
            public string Id;
        }

        public class BoardData
        {
            public int Height;
            public int Width;
            public IList<FoodPosition> Food;
            public IList<Snake> Snakes;


            public BoardData Copy()
            {
                return new BoardData
                {
                    Height = this.Height,
                    Width = this.Width,
                    Food = this.Food?
                        .ToList(),
                    Snakes = this.Snakes?
                        .Select(x => new Snake
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Health = x.Health,
                            Body = x.Body?.ToList()
                        })
                        .ToList()
                };
            }
        }

        public struct FoodPosition
        {
            public FoodPosition(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }


            public override bool Equals(object obj)
            {
                return obj is FoodPosition && this == (FoodPosition)obj;
            }


            public static bool operator ==(FoodPosition x, FoodPosition y)
            {
                return x.X == y.X && x.Y == y.Y;
            }


            public static bool operator !=(FoodPosition x, FoodPosition y)
            {
                return !(x == y);
            }


            public int X;
            public int Y;
        }

        public class Snake
        {
            public string Id;
            public string Name;
            public int Health;
            public IList<BodyPartPosition> Body;


            public Snake Copy()
            {
                return new Snake
                {
                    Id = this.Id,
                    Name = this.Name,
                    Health = this.Health,
                    Body = this.Body?
                        .ToList()
                };
            }
        }

        public struct BodyPartPosition
        {
            public BodyPartPosition(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }


            public override bool Equals(object obj)
            {
                return obj is BodyPartPosition && this == (BodyPartPosition)obj;
            }


            public static bool operator ==(BodyPartPosition x, BodyPartPosition y)
            {
                return x.X == y.X && x.Y == y.Y;
            }


            public static bool operator !=(BodyPartPosition x, BodyPartPosition y)
            {
                return !(x == y);
            }


            public override string ToString()
            {
                return $"({X}, {Y})";
            }


            public int X;
            public int Y;
        }


        public GameState Copy()
        {
            return new GameState
            {
                Game = this.Game,
                Turn = this.Turn,
                Board = this.Board.Copy(),
                You = this.You?.Copy()
            };
        }
    }
}