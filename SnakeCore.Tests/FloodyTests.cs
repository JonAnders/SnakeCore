using NUnit.Framework;

using SnakeCore.Web;
using SnakeCore.Web.Brains;

namespace SnakeCore.Tests
{
    [TestFixture]
    public class FloodyTests
    {
        #region SetUp/Teardown

        [SetUp]
        public void Setup()
        {
            this.brain = new Floody();
        }

        #endregion

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
        public void Test05()
        {
            GameState gameState = TestCases.Test05();

            var move = this.brain.Move(gameState);

            Assert.That(move, Is.EqualTo(LegalMove.Left));
        }

        [Test]
        public void Test11()
        {
            GameState gameState = TestCases.Test11();

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


        private IBrain brain;
    }
}
