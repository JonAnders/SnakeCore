using System.Collections.Generic;

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
    }
}