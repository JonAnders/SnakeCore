using System;

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

        public LegalMove[][] GetPossibleMoves(int numSnakes)
        {
            var allLegalMoves = new LegalMove[] { LegalMove.Up, LegalMove.Down, LegalMove.Left, LegalMove.Right };

            if (numSnakes == 1)
            {
                var moves = new LegalMove[4][];

                for (int i = 0; i < allLegalMoves.Length; i++)
                {
                    moves[i] = new LegalMove[] { allLegalMoves[i] };
                }

                return moves;
            }
            else
            {
                int numPossibleMoves = (int)Math.Pow(4, numSnakes);
                var moves = new LegalMove[numPossibleMoves][];

                for (int i = 0; i < allLegalMoves.Length; i++)
                {
                    var possibleMoves = GetPossibleMoves(numSnakes - 1);

                    for (int j = 0; j < possibleMoves.Length; j++)
                    {
                        var possibleMove = possibleMoves[j];
                        var newPossibleFuture = new LegalMove[possibleMove.Length + 1];
                        newPossibleFuture[0] = allLegalMoves[i];
                        possibleMove.CopyTo(newPossibleFuture, 1);
                        moves[(i * allLegalMoves.Length) + j] = newPossibleFuture;
                    }
                }

                return moves;
            }
        }
    }
}
