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
                        .Select(x => new FoodPosition(x.X, x.Y))
                        .ToList(),
                    Snakes = this.Snakes?
                        .Select(x => x.Copy())
                        .ToList()
                };
            }
        }

        public class FoodPosition
        {
            public FoodPosition(int x, int y)
            {
                this.X = x;
                this.Y = y;
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
                    Body = this.Body
                        .Select(x => new BodyPartPosition(x.X, x.Y))
                        .ToList()
                };
            }
        }

        public class BodyPartPosition
        {
            public BodyPartPosition(int x, int y)
            {
                this.X = x;
                this.Y = y;
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
                You = this.You.Copy()
            };
        }
    }
}