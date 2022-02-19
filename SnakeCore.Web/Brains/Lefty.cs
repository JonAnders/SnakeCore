namespace SnakeCore.Web.Brains
{
    public class Lefty : IBrain
    {
        public Battlesnake GetBattlesnake()
        {
            return new Battlesnake
            {
                Color = "#aaaaaa",
                Head = "bendr",
                Tail = "pixel"
            };
        }


        public void Start(GameState gameState)
        {
            return;
        }


        public LegalMove Move(GameState gameState)
        {
            return LegalMove.Left;
        }


        public void End(GameState gameState)
        {

        }
    }
}