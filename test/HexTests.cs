using System;
using HexPal;
using NUnit.Framework;

namespace test
{
    public class HexTests
    {
        [Test]
        public void CreateHex_FullCoordsIncorrect_ThrowsArgumentException()
        {
            // Assert
            Assert.Throws<ArgumentException>(() => new Hex(1,1,1), $"Coordinates (1,1,1) are invalid as they do not add up to 0 and thus should throw an ArgumentException");
        }

        [Test]
        public void CreateHex_QRCoords_CoordsTranslatedCorrectly()
        {
            // Arrange + Act
            var hex = new Hex(1,1);

            // Assert
            Assert.Multiple(() => {
                Assert.AreEqual(1, hex.Q);
                Assert.AreEqual(1, hex.R);
                Assert.AreEqual(-2, hex.S);
            });
        }

        [Test]
        public void CompareHex_SameCoords_ReturnsTrue()
        {
            // Arrange
            var hex1 = new Hex(1,1);
            var hex2 = new Hex(1,1);

            // Act + Assert
            Assert.IsTrue(hex1 == hex2);
        }

        [Test]
        public void CompareHex_DifferentCoords_ReturnsFalse()
        {
            // Arrange
            var hex1 = new Hex(1,1);
            var hex2 = new Hex(-3,2);

            // Act + Assert
            Assert.IsFalse(hex1 == hex2);
        }

        [Test]
        public void AddHex_CreatesNewHexWithCorrectCoords()
        {
            // Arrange
            var hex1 = new Hex(1,1);
            var hex2 = new Hex(-3,2);

            // Act + Assert
            var res = hex1 + hex2;

            // Assert
            Assert.AreEqual(new Hex(-2,3), res);
        }

        [Test]
        public void MultiplyHex_CreatesNewHexWithCorrectCoords()
        {
            // Arrange
            var hex1 = new Hex(1,2);

            // Act + Assert
            var res = hex1 * 5;

            // Assert
            Assert.AreEqual(new Hex(5,10), res);
        }

        [Test]
        public void Length_CorrectlyCalculatesLengthFromOrigin()
        {
            // Arrange
            var hex1 = new Hex(2,1);

            // Act + Assert
            var res = hex1.Length;

            // Assert
            Assert.AreEqual(3, res);
        }

        [TestCase(true,true,true)]
        [TestCase(true,false,false)]
        [TestCase(false,true,false)]
        [TestCase(false,false,true)]
        public void Equality_EqualityNullHandling(bool aIsNull, bool bIsNull, bool result) {
            // Arrange
            var a = aIsNull ? null : new Hex(0,0);
            var b = bIsNull ? null : new Hex(0,0);

            // Assert
            Assert.AreEqual(result, a == b);
        }

        [TestCase(true,true,false)]
        [TestCase(false,false,false)]
        [TestCase(true,false,true)]
        [TestCase(false,true,true)]
        public void Inequality_EqualityNullHandling(bool aIsNull, bool bIsNull, bool result) {
            // Arrange
            var a = aIsNull ? null : new Hex(0,0);
            var b = bIsNull ? null : new Hex(0,0);

            // Assert
            Assert.AreEqual(result, a != b);
        }
    }
}