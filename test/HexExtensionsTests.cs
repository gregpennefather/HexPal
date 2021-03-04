using System;
using HexPal;
using NUnit.Framework;

namespace test
{
    [TestFixture]
    public class HexExtensions
    {
        [Test]
        public void Distance()
        {
            // Arrange
            var hex1 = new Hex(0, -1);
            var hex2 = new Hex(2, -1);

            // Act
            var res = hex1.DistanceTo(hex2);

            // Assert
            Assert.AreEqual(2, res);
        }


        [TestCase(0, 0, 0, 1, 0)]
        [TestCase(0, 0, 1, 0, 1)]
        [TestCase(0, 0, 2, -1, 1)]
        [TestCase(0, 0, 3, -1, 0)]
        [TestCase(0, 0, 4, 0, -1)]
        [TestCase(0, 0, 5, 1, -1)]
        [TestCase(0, -1, 1, 0, 0)]
        [TestCase(1, -2, 4, 1, -3)]
        [TestCase(-2, 2, 2, -3, 3)]
        [TestCase(-2, 1, PointyDirection.UpRight, -1, 0)]
        [TestCase(-2, 1, FlatDirection.RightUp, -1, 0)]
        public void Neighbour(int q, int r, int direction, int resultQ, int resultR)
        {
            // Arrange
            var hex1 = new Hex(q, r);

            // Act
            var res = hex1.Neighbour(direction);

            // Assert
            Assert.AreEqual(new Hex(resultQ, resultR), res, $"{hex1}'s {direction} neighbour is {new Hex(resultQ, resultR)} ({hex1} + {Hex.Directions[direction]})");
        }

        [TestCase(-1)]
        [TestCase(7)]
        public void Neighbour_OutOfRange_ThrowsException(int direction) {
            // Arrange
            var hex = new Hex(0,0);

            // Act + Assert
            Assert.Throws<IndexOutOfRangeException>(() => hex.Neighbour(direction));
        }
    }
}