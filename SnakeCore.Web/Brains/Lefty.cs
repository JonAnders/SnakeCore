namespace SnakeCore.Web.Brains
{
    public class Lefty : IBrain
    {
        public StartResponse Start(GameState gameState)
        {
            return new StartResponse
            {
                Color = "#aaaaaa",
                HeadType = "bendr",
                TailType = "pixel"
            };
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