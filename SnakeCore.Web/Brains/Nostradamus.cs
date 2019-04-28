namespace SnakeCore.Web.Brains
{
    public class Nostradamus : IBrain
    {
        public StartResponse Start(GameState gameState)
        {
            return new StartResponse
            {
                Color = "#7788aa",
                HeadType = "silly",
                TailType = "freckled"
            };
        }


        public LegalMove Move(GameState gameState)
        {
            return LegalMove.Down;
        }


        public void End(GameState gameState)
        {
        }
    }
}
