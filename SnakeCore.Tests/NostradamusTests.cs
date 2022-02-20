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
        private NostradamusPrecalc precalc;

        [SetUp]
        public void Setup()
        {
            this.precalc = new NostradamusPrecalc();
            this.brain = new Nostradamus(this.precalc);
        }


        [Test]
        public void GetPossibleMovesPermutations_TwoSnakes_Returns16Permutations()
        {
            var permutations = this.precalc.GetPossibleMovesPermutations(2);

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
            var permutations = this.precalc.GetPossibleMovesPermutations(3);

            Assert.That(permutations.Length, Is.EqualTo(64));

            Assert.That(permutations[0], Is.EqualTo(new LegalMove[] { LegalMove.Up, LegalMove.Up, LegalMove.Up }));
            Assert.That(permutations[4], Is.EqualTo(new LegalMove[] { LegalMove.Up, LegalMove.Down, LegalMove.Up }));
            Assert.That(permutations[63], Is.EqualTo(new LegalMove[] { LegalMove.Right, LegalMove.Right, LegalMove.Right }));
        }


        [Test]
        public void GetPossibleMovesPermutations_EightSnakes_Returns65536Permutations()
        {
            var stopwatch = Stopwatch.StartNew();
            var permutations = this.precalc.GetPossibleMovesPermutations(8);
            Console.WriteLine(stopwatch.Elapsed);

            Assert.That(permutations.Length, Is.EqualTo(65536));

            Assert.That(permutations[0], Is.EqualTo(new LegalMove[] { LegalMove.Up, LegalMove.Up, LegalMove.Up, LegalMove.Up, LegalMove.Up, LegalMove.Up, LegalMove.Up, LegalMove.Up }));
            Assert.That(permutations[4], Is.EqualTo(new LegalMove[] { LegalMove.Up, LegalMove.Up, LegalMove.Up, LegalMove.Up, LegalMove.Up, LegalMove.Up, LegalMove.Down, LegalMove.Up }));
            Assert.That(permutations[65535], Is.EqualTo(new LegalMove[] { LegalMove.Right, LegalMove.Right, LegalMove.Right, LegalMove.Right, LegalMove.Right, LegalMove.Right, LegalMove.Right, LegalMove.Right }));
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
            var gameState = TestCases.Test02();

            var stopwatch = Stopwatch.StartNew();
            var move = this.brain.Move(gameState);
            Console.WriteLine(stopwatch.Elapsed);

            Assert.That(move, Is.EqualTo(LegalMove.Left));
        }


        [Test]
        public void Test03()
        {
            var gameState = TestCases.Test03();

            var stopwatch = Stopwatch.StartNew();
            var move = this.brain.Move(gameState);
            Console.WriteLine(stopwatch.Elapsed);

            Assert.That(move, Is.EqualTo(LegalMove.Down));
        }


        [Test]
        public void Test05()
        {
            var gameState = TestCases.Test05();

            var stopwatch = Stopwatch.StartNew();
            var move = this.brain.Move(gameState);
            Console.WriteLine(stopwatch.Elapsed);

            Assert.That(move, Is.EqualTo(LegalMove.Left));
        }


        [Test]
        public void Test07()
        {
            var gameState = TestCases.Test07();

            var stopwatch = Stopwatch.StartNew();
            var move = this.brain.Move(gameState);
            Console.WriteLine(stopwatch.Elapsed);

            Assert.That(move, Is.EqualTo(LegalMove.Right));
        }


        [Test]
        public void Test08()
        {
            var gameState = TestCases.Test08();

            var stopwatch = Stopwatch.StartNew();
            var move = this.brain.Move(gameState);
            Console.WriteLine(stopwatch.Elapsed);

            Assert.That(move, Is.EqualTo(LegalMove.Up));
        }


        [Test]
        [Ignore("Longest path to certain death is not supported yet")]
        public void Test10()
        {
            var gameState = TestCases.Test10();

            var stopwatch = Stopwatch.StartNew();
            var move = this.brain.Move(gameState);
            Console.WriteLine(stopwatch.Elapsed);

            Assert.That(move, Is.EqualTo(LegalMove.Down));
        }


        [Test]
        public void Test11()
        {
            var gameState = TestCases.Test11();

            var stopwatch = Stopwatch.StartNew();
            var move = this.brain.Move(gameState);
            Console.WriteLine(stopwatch.Elapsed);

            Assert.That(move, Is.EqualTo(LegalMove.Left));
        }


        [Test]
        public void Test12()
        {
            var gameState = TestCases.Test12();

            var stopwatch = Stopwatch.StartNew();
            var move = this.brain.Move(gameState);
            Console.WriteLine(stopwatch.Elapsed);

            Assert.That(move, Is.EqualTo(LegalMove.Up));
        }


        [Test]
        public void Test13()
        {
            var gameState = TestCases.Test13();

            var stopwatch = Stopwatch.StartNew();
            var move = this.brain.Move(gameState);
            Console.WriteLine(stopwatch.Elapsed);

            Assert.That(move, Is.EqualTo(LegalMove.Left));
        }


        [Test]
        public void Test14()
        {
            var gameState = TestCases.Test14();

            var stopwatch = Stopwatch.StartNew();
            var move = this.brain.Move(gameState);
            Console.WriteLine(stopwatch.Elapsed);

            Assert.That(move, Is.EqualTo(LegalMove.Right));
        }


        [TestCase(93, ExpectedResult = "down")]
        [TestCase(2, ExpectedResult = "right")]
        public string Test15(int hp)
        {
            var gameState = TestCases.Test15();
            gameState.You.Health = hp;

            var stopwatch = Stopwatch.StartNew();
            var move = this.brain.Move(gameState);
            Console.WriteLine(stopwatch.Elapsed);

            return move.Move;
        }
    }
}
