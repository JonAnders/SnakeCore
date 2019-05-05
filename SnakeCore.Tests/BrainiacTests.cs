using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NUnit.Framework;

using SnakeCore.Web;
using SnakeCore.Web.Brains;

namespace SnakeCore.Tests
{
    public class BrainiacTests
    {
        private IBrain brain;

        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(x => x.AddConsole().SetMinimumLevel(LogLevel.Debug))
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger<Brainiac>();

            this.brain = new Brainiac(logger);
        }


        [Test]
        public void Test01()
        {
            GameState gameState = TestCases.Test01();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Right));
        }

        [Test]
        public void Test02()
        {
            GameState gameState = TestCases.Test02();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Left));
        }

        [Test]
        public void Test03()
        {
            GameState gameState = TestCases.Test03();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Down));
        }

        [Test]
        public void Test04()
        {
            GameState gameState = TestCases.Test04();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Right));
        }

        [Test]
        public void Test05()
        {
            GameState gameState = TestCases.Test05();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Left));
        }

        [TestCase(90, ExpectedResult = "right")]
        [TestCase(10, ExpectedResult = "left")]
        public string Test06(int hp)
        {
            GameState gameState = TestCases.Test06();
            gameState.You.Health = hp;

            var move = this.brain.Move(gameState);

            return move.Move;
        }

        [Test]
        public void Test07()
        {
            GameState gameState = TestCases.Test07();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Right));
        }

        [Test]
        public void Test08()
        {
            GameState gameState = TestCases.Test08();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Up));
        }

        [TestCase(true, ExpectedResult = "right")]
        [TestCase(false, ExpectedResult = "up")]
        public string Test09(bool includeFood)
        {
            GameState gameState = TestCases.Test09(includeFood);
            gameState.You.Health = 50;

            var move = this.brain.Move(gameState);

            return move.Move;
        }

        [Test]
        public void Test10()
        {
            GameState gameState = TestCases.Test10();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Down));
        }

        [Test]
        public void Test11()
        {
            GameState gameState = TestCases.Test11();

            var move = this.brain.Move(gameState);

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
            GameState gameState = TestCases.Test13();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Left));
        }

        [Test]
        public void Test14()
        {
            GameState gameState = TestCases.Test14();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Right));
        }
    }
}