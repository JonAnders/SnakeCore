namespace SnakeCore.Web.Brains
{
    public interface IBrain
    {
        StartResponse Start(GameState gameState);
        LegalMove Move(GameState gameState);
        void End(GameState gameState);
    }
}