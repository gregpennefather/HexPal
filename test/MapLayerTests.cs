using HexPal;
using NUnit.Framework;

namespace test
{
    [TestFixture]
    public class MapLayerTests
    {
        [Test]
        public void MapLayer_PathingSnapshot() {
            // Arrange
            var layer = new MapLayer<int>();
            var hex = new Hex(1,0);

            // Act
            layer[hex] = 1;

            // Assert
            Assert.AreEqual(1, layer.PathingSnapshot[hex]);
        }

        [Test]
        public void MapLayer_PathingSnapshot_ClearedTile() {
            // Arrange
            var layer = new MapLayer<int?>();
            var hex = new Hex(1,0);

            // Act
            layer[hex] = null;

            // Assert
            Assert.IsTrue(!layer.PathingSnapshot.ContainsKey(hex));
        }

        [Test]
        public void PathedMapLayer_PathingSnapshot() {
            // Arrange
            var layer = new PathedMapLayer<bool>();
            var hex = new Hex(4,2);

            // Act
            layer[hex] = new PathedTile<bool>(true, 5, true);

            // Assert
            Assert.AreEqual(5, layer.PathingSnapshot[hex]);
        }

        [Test]
        public void PathedMapLayer_NonPathedTile() {
            // Arrange
            var layer = new PathedMapLayer<bool>();
            var hex = new Hex(4,2);

            // Act
            layer[hex] = new PathedTile<bool>(false, 5, true);

            // Assert
            Assert.IsTrue(!layer.PathingSnapshot.ContainsKey(hex));
        }

        [Test]
        public void PathedMapLayer_ClearedTile() {
            // Arrange
            var layer = new PathedMapLayer<bool>();
            var hex = new Hex(4,2);

            // Act
            layer[hex] = null;

            // Assert
            Assert.IsTrue(!layer.PathingSnapshot.ContainsKey(hex));
        }
    }
}