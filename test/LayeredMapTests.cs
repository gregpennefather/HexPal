using System.Numerics;
using HexPal;
using NUnit.Framework;

namespace test
{
    [TestFixture]
    public class LayeredMapTests
    {
        [Test]
        public void AddMapLayer()
        {
            // Arrange
            var mapLayer = new PathedMapLayer<int>();
            var mapLayer2 = new MapLayer<bool>();

            var map = new LayeredMap(LayoutOrientation.Flat, new Vector2(1,1), new Vector2(0,0), mapLayer);

            // Act
            map.AddLayer(mapLayer2);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(map[0] is PathedMapLayer<int>);
                Assert.IsTrue(map[1] is MapLayer<bool>);
            });
        }

        [Test]
        public void HexToPosition()
        {
            // Arrange
            var mapLayer = new PathedMapLayer<int>();

            var map = new LayeredMap(LayoutOrientation.Pointy, new Vector2(32,32), new Vector2(100,100), mapLayer);

            // Act
            Vector2 position = map.HexToPosition(new Hex(3, 2));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(321.702515f, position.X);
                Assert.AreEqual(196.0f, position.Y);
            });
        }

        [Test]
        public void PositionToHex()
        {
            // Arrange
            var mapLayer = new PathedMapLayer<int>();

            var map = new LayeredMap(LayoutOrientation.Flat, new Vector2(10,10), new Vector2(100,100), mapLayer);

            // Act
            Hex coords = map.PositionToHex(new Vector2(115, 215));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, coords.Q);
                Assert.AreEqual(6, coords.R);
            });
        }

        [Test]
        public void CalculatedTileWeights_PathedAndUnpathedLayer_UnpathedLayerEmpty() {

            // Arrange
            var testHex = new Hex(2,3);
            var pathedMapLayer = new PathedMapLayer<bool>();
            pathedMapLayer[testHex] = new PathedTile<bool>(true, 3, true);

            var unpathedMapLayer = new MapLayer<bool>();

            var map = new LayeredMap(LayoutOrientation.Flat, new Vector2(10,10), new Vector2(100,100), pathedMapLayer);
            map.AddLayer(unpathedMapLayer);

            // Act
            var pathingInfo = map.CalculatedTileWeights();

            // Assert
            Assert.AreEqual(3, pathingInfo[testHex]);
        }

        [Test]
        public void CalculatedTileWeights_PathedAndUnpathedLayer_UnpathedLayerOccupied_ClearsPathing() {

            // Arrange
            var testHex = new Hex(2,3);
            var pathedMapLayer = new PathedMapLayer<bool>();
            pathedMapLayer[testHex] = new PathedTile<bool>(true, 3, true);

            var unpathedMapLayer = new MapLayer<bool>();
            unpathedMapLayer[testHex] = true;

            var map = new LayeredMap(LayoutOrientation.Flat, new Vector2(10,10), new Vector2(100,100), pathedMapLayer);
            map.AddLayer(unpathedMapLayer);

            // Act
            var pathingInfo = map.CalculatedTileWeights();

            // Assert
            Assert.IsTrue(!pathingInfo.ContainsKey(testHex));
        }
    }
}