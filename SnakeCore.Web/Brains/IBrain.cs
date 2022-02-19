namespace SnakeCore.Web.Brains
{
    public interface IBrain
    {
        Battlesnake GetBattlesnake();
        void Start(GameState gameState);
        LegalMove Move(GameState gameState);
        void End(GameState gameState);
    }
}