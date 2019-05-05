using System;

namespace SnakeCore.Web.Brains
{
    public class NostradamusPrecalc
    {
        private readonly LegalMove[][][] allTheThings;

        public NostradamusPrecalc()
        {
            this.allTheThings = new LegalMove[8][][];

            for (int i = 0; i < 8; i++)
            {
                this.allTheThings[i] = GeneratePossibleMovesPermutations(i + 1);
            }
        }


        public LegalMove[][] GetPossibleMovesPermutations(int numSnakes)
        {
            if (numSnakes <= 0)
                throw new Exception($"Too few snakes! Must be at least 1, but was {numSnakes}");

            if (numSnakes <= 8)
                return this.allTheThings[numSnakes - 1];

            return GeneratePossibleMovesPermutations(numSnakes);
        }


        private LegalMove[][] GeneratePossibleMovesPermutations(int numSnakes)
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
                    var possibleMoves = GeneratePossibleMovesPermutations(numSnakes - 1);

                    for (int j = 0; j < possibleMoves.Length; j++)
                    {
                        var possibleMove = possibleMoves[j];
                        var newPossibleFuture = new LegalMove[possibleMove.Length + 1];
                        newPossibleFuture[0] = allLegalMoves[i];
                        possibleMove.CopyTo(newPossibleFuture, 1);
                        moves[(i * possibleMoves.Length) + j] = newPossibleFuture;
                    }
                }

                return moves;
            }
        }
    }
}
