using NUnit.Framework;
using SnakeCore.Web;
using SnakeCore.Web.Brains;
using System.Collections.Generic;

namespace SnakeCore.Tests
{
    [TestFixture]
    public class NostradamusTests
    {
        private Nostradamus brain;

        [SetUp]
        public void Setup()
        {
            this.brain = new Nostradamus();
        }


        [Test]
        public void GetPossibleMoves_TwoSnakes_Returns16Moves()
        {
            var moves = this.brain.GetPossibleMoves(2);

            Assert.That(moves.Length, Is.EqualTo(16));

            Assert.That(moves[0], Is.EqualTo(new LegalMove[] { LegalMove.Up, LegalMove.Up }));
            Assert.That(moves[1], Is.EqualTo(new LegalMove[] { LegalMove.Up, LegalMove.Down }));
            Assert.That(moves[2], Is.EqualTo(new LegalMove[] { LegalMove.Up, LegalMove.Left }));
            Assert.That(moves[3], Is.EqualTo(new LegalMove[] { LegalMove.Up, LegalMove.Right }));
            Assert.That(moves[4], Is.EqualTo(new LegalMove[] { LegalMove.Down, LegalMove.Up }));
            Assert.That(moves[5], Is.EqualTo(new LegalMove[] { LegalMove.Down, LegalMove.Down }));
            Assert.That(moves[6], Is.EqualTo(new LegalMove[] { LegalMove.Down, LegalMove.Left }));
            Assert.That(moves[7], Is.EqualTo(new LegalMove[] { LegalMove.Down, LegalMove.Right }));
            Assert.That(moves[8], Is.EqualTo(new LegalMove[] { LegalMove.Left, LegalMove.Up }));
            Assert.That(moves[9], Is.EqualTo(new LegalMove[] { LegalMove.Left, LegalMove.Down }));
            Assert.That(moves[10], Is.EqualTo(new LegalMove[] { LegalMove.Left, LegalMove.Left }));
            Assert.That(moves[11], Is.EqualTo(new LegalMove[] { LegalMove.Left, LegalMove.Right }));
            Assert.That(moves[12], Is.EqualTo(new LegalMove[] { LegalMove.Right, LegalMove.Up }));
            Assert.That(moves[13], Is.EqualTo(new LegalMove[] { LegalMove.Right, LegalMove.Down }));
            Assert.That(moves[14], Is.EqualTo(new LegalMove[] { LegalMove.Right, LegalMove.Left }));
            Assert.That(moves[15], Is.EqualTo(new LegalMove[] { LegalMove.Right, LegalMove.Right }));
        }
    }
}
