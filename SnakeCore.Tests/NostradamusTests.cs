using NUnit.Framework;
using SnakeCore.Web;
using SnakeCore.Web.Brains;
using System;
using System.Diagnostics;

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
        public void GetPossibleMovesPermutations_TwoSnakes_Returns16Permutations()
        {
            var permutations = this.brain.GetPossibleMovesPermutations(2);

            Assert.That(permutations.Length, Is.EqualTo(16));

            Assert.That(permutations[0], Is.EqualTo(new LegalMove[] { LegalMove.Up, LegalMove.Up }));
            Assert.That(permutations[1], Is.EqualTo(new LegalMove[] { LegalMove.Up, LegalMove.Down }));
            Assert.That(permutations[2], Is.EqualTo(new LegalMove[] { LegalMove.Up, LegalMove.Left }));
            Assert.That(permutations[3], Is.EqualTo(new LegalMove[] { LegalMove.Up, LegalMove.Right }));
            Assert.That(permutations[4], Is.EqualTo(new LegalMove[] { LegalMove.Down, LegalMove.Up }));
            Assert.That(permutations[5], Is.EqualTo(new LegalMove[] { LegalMove.Down, LegalMove.Down }));
            Assert.That(permutations[6], Is.EqualTo(new LegalMove[] { LegalMove.Down, LegalMove.Left }));
            Assert.That(permutations[7], Is.EqualTo(new LegalMove[] { LegalMove.Down, LegalMove.Right }));
            Assert.That(permutations[8], Is.EqualTo(new LegalMove[] { LegalMove.Left, LegalMove.Up }));
            Assert.That(permutations[9], Is.EqualTo(new LegalMove[] { LegalMove.Left, LegalMove.Down }));
            Assert.That(permutations[10], Is.EqualTo(new LegalMove[] { LegalMove.Left, LegalMove.Left }));
            Assert.That(permutations[11], Is.EqualTo(new LegalMove[] { LegalMove.Left, LegalMove.Right }));
            Assert.That(permutations[12], Is.EqualTo(new LegalMove[] { LegalMove.Right, LegalMove.Up }));
            Assert.That(permutations[13], Is.EqualTo(new LegalMove[] { LegalMove.Right, LegalMove.Down }));
            Assert.That(permutations[14], Is.EqualTo(new LegalMove[] { LegalMove.Right, LegalMove.Left }));
            Assert.That(permutations[15], Is.EqualTo(new LegalMove[] { LegalMove.Right, LegalMove.Right }));
        }


        [Test]
        public void GetPossibleMovesPermutations_ThreeSnakes_Returns64Permutations()
        {
            var permutations = this.brain.GetPossibleMovesPermutations(3);

            Assert.That(permutations.Length, Is.EqualTo(64));

            Assert.That(permutations[0], Is.EqualTo(new LegalMove[] { LegalMove.Up, LegalMove.Up, LegalMove.Up }));
            Assert.That(permutations[4], Is.EqualTo(new LegalMove[] { LegalMove.Up, LegalMove.Down, LegalMove.Up }));
            Assert.That(permutations[63], Is.EqualTo(new LegalMove[] { LegalMove.Right, LegalMove.Right, LegalMove.Right }));
        }


        [Test]
        public void Test01()
        {
            var gameState = TestCases.Test01();

            var stopwatch = Stopwatch.StartNew();
            var move = this.brain.Move(gameState);
            Console.WriteLine(stopwatch.Elapsed);
            
            Assert.That(move, Is.EqualTo(LegalMove.Right).Or.EqualTo(LegalMove.Left));
        }


        [Test]
        public void Test02()
        {
            var gameState = TestCases.Test01();

            var stopwatch = Stopwatch.StartNew();
            var move = this.brain.Move(gameState);
            Console.WriteLine(stopwatch.Elapsed);

            Assert.That(move, Is.EqualTo(LegalMove.Left));
        }
    }
}
