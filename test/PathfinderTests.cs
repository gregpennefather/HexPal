using System;
using System.Collections.Generic;
using System.Linq;
using HexPal;
using NUnit.Framework;
using static HexPal.Pathfinder;

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

        [Test]
        public void PriorityQueue_Add() {
            // Arrange
            var priorityQueue = new PriorityQueue<int, int>();

            // Act
            priorityQueue.Add(3,3);
            priorityQueue.Add(1,1);
            priorityQueue.Add(4,4);

            // Assert
            var list = priorityQueue.ToList();
            Assert.Multiple(() => {
                Assert.AreEqual(1, list[0]);
                Assert.AreEqual(3, list[1]);
                Assert.AreEqual(4, list[2]);
            });
        }

        [Test]
        public void PriorityQueue_Pop() {
            // Arrange
            var priorityQueue = new PriorityQueue<int, int>();
            priorityQueue.Add(3,3);
            priorityQueue.Add(1,1);
            priorityQueue.Add(4,4);

            // Act
            var result = priorityQueue.Pop();

            // Assert
            var list = priorityQueue.ToList();
            Assert.Multiple(() => {
                Assert.AreEqual(1, result);
                Assert.AreEqual(3, list[0]);
                Assert.AreEqual(4, list[1]);
            });
        }

        [Test]
        public void AStarSearch_SimplePath() {
            // Arrange
            var origin = new Hex(0,0);
            var target = new Hex(1,2);

            var tiles = new List<Tuple<float, Hex>>();
            var radius = 5;
            for (int q = -radius; q <= radius; q++)
            {
                var r1 = Math.Max(-radius, -q - radius);
                var r2 = Math.Min(radius, -q + radius);

                for (int r = r1; r <= r2; r++)
                {
                    tiles.Add(new Tuple<float, Hex>(1, new Hex(q, r)));
                }
            }

            // Act
            var path = Pathfinder.AStar(origin, target, tiles);

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