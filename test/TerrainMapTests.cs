using System.Collections.Generic;
using HexPal;
using NUnit.Framework;

namespace test
{
    [TestFixture]
    public class TerrainMapTests
    {


        [Test]
        public void Navigate_UnpathableHexProvided_HexUnpathableInCalculation()
        {

            // Arrange
            var pathedMapLayer = new PathedMapLayer<bool>();
            pathedMapLayer[new Hex(0,0)] = new PathedTile<bool>(true, 3, true);
            pathedMapLayer[new Hex(0,1)] = new PathedTile<bool>(true, 3, true);

            var map = BuildTerrainMap(pathedMapLayer);

            // Act
            var pathingInfo = map.Navigate(new Hex(0, 0), new Hex(0, 1), new List<Hex>() { new Hex(0, 1) });

            // Assert
            Assert.IsNull(pathingInfo);
        }

        private TerrainMap BuildTerrainMap(IPathableMapLayer terrainLayer)
        {
            (float x, float y) positionZero = (0, 0);
            return new TerrainMap(LayoutOrientation.Flat, positionZero, positionZero, terrainLayer);
        }
    }
}