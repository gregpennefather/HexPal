using HexPal;
using NUnit.Framework;

namespace test
{
    [TestFixture]
    public class PathfinderTests
    {
        [Test]
        public void BreathSearch_SimplePath() {
            // Arrange
            var origin = new Hex(0,0);
            var target = new Hex(1,2);

            // Act
            var path = Pathfinder.BreathSearch(origin, target);

            // Assert
            Assert.Multiple(() => {
               Assert.AreEqual(3, path.Count);
               Assert.AreEqual(new Hex(1,0), path[0]);
               Assert.AreEqual(new Hex(1,1), path[1]);
               Assert.AreEqual(new Hex(1,2), path[2]);
            });
        }
    }
}