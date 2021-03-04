using System;
using HexPal;
using NUnit.Framework;

namespace test
{
    public class FractionalHexTests
    {

        [Test]
        public void Create_FullCoordsIncorrect_ThrowsArgumentException()
        {
            // Assert
            Assert.Throws<ArgumentException>(() => new FractionalHex(1, 1, 1), $"Coordinates (1,1,1) are invalid as they do not add up to 0 and thus should throw an ArgumentException");
        }

        [Test]
        public void Create_QRCoords_CoordsTranslatedCorrectly()
        {
            // Arrange + Act
            var hex = new FractionalHex(1, 1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, hex.Q);
                Assert.AreEqual(1, hex.R);
                Assert.AreEqual(-2, hex.S);
            });
        }

        [Test]
        public void Compare_SameCoords_ReturnsTrue()
        {
            // Arrange
            var hex1 = new FractionalHex(1, 1);
            var hex2 = new FractionalHex(1, 1);

            // Act + Assert
            Assert.IsTrue(hex1 == hex2);
        }

        [Test]
        public void Compare_DifferentCoords_ReturnsFalse()
        {
            // Arrange
            var hex1 = new FractionalHex(1, 1);
            var hex2 = new FractionalHex(-3, 2);

            // Act + Assert
            Assert.IsFalse(hex1 == hex2);
        }



        [Test]
        public void Add_CreatesNewHexWithCorrectCoords()
        {
            // Arrange
            var hex1 = new FractionalHex(1.5, 1);
            var hex2 = new FractionalHex(-3, 2.25);

            // Act
            var res = hex1 + hex2;

            // Assert
            Assert.AreEqual(new FractionalHex(-1.5, 3.25), res);
        }

        [Test]
        public void Multiply_CreatesNewHexWithCorrectCoords()
        {
            // Arrange
            var hex1 = new FractionalHex(1.0, 2.0);

            // Act
            var res = hex1 * 5.5;

            // Assert
            Assert.AreEqual(new FractionalHex(5.5, 11), res);
        }
    }
}