namespace SnakeCore.Web
{
    public class LegalMove
    {
        private LegalMove(string move)
        {
            this.Move = move;
        }


        public override string ToString()
        {
            return this.Move;
        }


        public string Move { get; }

        public static readonly LegalMove Up = new LegalMove("up");
        public static readonly LegalMove Down = new LegalMove("down");
        public static readonly LegalMove Left = new LegalMove("left");
        public static readonly LegalMove Right = new LegalMove("right");
    }
}