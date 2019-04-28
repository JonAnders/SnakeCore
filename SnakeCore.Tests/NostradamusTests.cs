using NUnit.Framework;
using SnakeCore.Web.Brains;

namespace SnakeCore.Tests
{
    [TestFixture]
    public class NostradamusTests
    {
        private IBrain brain;

        [SetUp]
        public void Setup()
        {
            this.brain = new Nostradamus();
        }
    }
}
