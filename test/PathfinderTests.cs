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
        public void BreathSearch_SimplePath()
        {
            // Arrange
            var origin = new Hex(0, 0);
            var target = new Hex(1, 2);

            // Act
            var path = Pathfinder.BreathSearch(origin, target);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(3, path.Count);
                Assert.AreEqual(new Hex(1, 0), path[0]);
                Assert.AreEqual(new Hex(1, 1), path[1]);
                Assert.AreEqual(new Hex(1, 2), path[2]);
            });
        }

        [Test]
        public void PriorityQueue_Add()
        {
            // Arrange
            var priorityQueue = new PriorityQueue<int, int>();

            // Act
            priorityQueue.Add(3, 3);
            priorityQueue.Add(1, 1);
            priorityQueue.Add(4, 4);

            // Assert
            var list = priorityQueue.ToList();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, list[0]);
                Assert.AreEqual(3, list[1]);
                Assert.AreEqual(4, list[2]);
            });
        }

        [Test]
        public void PriorityQueue_Pop()
        {
            // Arrange
            var priorityQueue = new PriorityQueue<int, int>();
            priorityQueue.Add(3, 3);
            priorityQueue.Add(1, 1);
            priorityQueue.Add(4, 4);

            // Act
            var result = priorityQueue.Pop();

            // Assert
            var list = priorityQueue.ToList();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, result);
                Assert.AreEqual(3, list[0]);
                Assert.AreEqual(4, list[1]);
            });
        }

        [Test]
        public void AStarSearch_NoPath()
        {
            // Arrange
            var origin = new Hex(-1, 0);
            var target = new Hex(5, 5);

            var tiles = new Dictionary<Hex, float>();
            var radius = 1;
            for (int q = -radius; q <= radius; q++)
            {
                var r1 = Math.Max(-radius, -q - radius);
                var r2 = Math.Min(radius, -q + radius);

                for (int r = r1; r <= r2; r++)
                {
                    tiles[new Hex(q,r)] = 1;
                }
            }

            // Act
            var path = Pathfinder.AStar(origin, target, tiles);

            // Assert
            Assert.IsNull(path);
        }

        [Test]
        public void AStarSearch_WeightedPath()
        {
            // Arrange
            var origin = new Hex(-1, 0);
            var target = new Hex(1, 0);

            var tiles = new Dictionary<Hex, float>();
            var radius = 5;
            for (int q = -radius; q <= radius; q++)
            {
                var r1 = Math.Max(-radius, -q - radius);
                var r2 = Math.Min(radius, -q + radius);

                for (int r = r1; r <= r2; r++)
                {
                    tiles[new Hex(q,r)] = q == 0 && (r == 0 || r == -1) ? 5 : 1;
                }
            }

            // Act
            var path = Pathfinder.AStar(origin, target, tiles);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(3, path.Count);
                Assert.AreEqual(new Hex(-1, 1), path[0]);
                Assert.AreEqual(new Hex(0, 1), path[1]);
                Assert.AreEqual(new Hex(1, 0), path[2]);
            });
        }

        [Test]
        public void AStarSearch_MultipleGoalsPath()
        {
            // Arrange
            var origin = new Hex(0, -1);
            var targets = new List<Hex>() { new Hex(0, 1), new Hex(0, -4) };

            var tiles = new Dictionary<Hex, float>();
            var radius = 5;
            for (int q = -radius; q <= radius; q++)
            {
                var r1 = Math.Max(-radius, -q - radius);
                var r2 = Math.Min(radius, -q + radius);

                for (int r = r1; r <= r2; r++)
                {
                    tiles[new Hex(q,r)] = 1;
                }
            }

            // Act
            var path = Pathfinder.AStar(origin, targets, tiles);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, path.Count);
                Assert.AreEqual(new Hex(0, 0), path[0]);
                Assert.AreEqual(new Hex(0, 1), path[1]);
            });
        }

        [Test]
        public void AStarSearch_MultipleGoalsWeightedPath()
        {
            // Arrange
            var origin = new Hex(0, -1);
            var targets = new List<Hex>() { new Hex(0, 1), new Hex(0, -4), new Hex(-1, -3) };

            var tiles = new Dictionary<Hex, float>();
            var radius = 5;
            for (int q = -radius; q <= radius; q++)
            {
                var r1 = Math.Max(-radius, -q - radius);
                var r2 = Math.Min(radius, -q + radius);

                for (int r = r1; r <= r2; r++)
                {
                    var weight = 1;
                    if (q == 0 && r == -4) {
                        weight = 5;
                    } else if (q == 0 && r == 0) {
                        weight = 5;
                    }
                    tiles[new Hex(q,r)] = weight;
                }
            }

            // Act
            var path = Pathfinder.AStar(origin, targets, tiles);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(3, path.Count);
                Assert.AreEqual(new Hex(0, -2), path[0]);
                Assert.AreEqual(new Hex(0, -3), path[1]);
                Assert.AreEqual(new Hex(0, -4), path[2]);
            });
        }

        [Test]
        public void AStarSearch_GoalIsUnpathable()
        {
            // Arrange
            var origin = new Hex(0, -1);
            var targets = new Hex(0, 1);

            var tiles = new Dictionary<Hex, float>();
            var radius = 5;
            for (int q = -radius; q <= radius; q++)
            {
                var r1 = Math.Max(-radius, -q - radius);
                var r2 = Math.Min(radius, -q + radius);

                for (int r = r1; r <= r2; r++)
                {
                    var weight = 1;
                    if (q == 0 && r == 1) {
                        continue;
                    }
                    tiles[new Hex(q,r)] = weight;
                }
            }

            // Act
            var path = Pathfinder.AStar(origin, targets, tiles);

            // Assert
            Assert.IsNull(path);
        }
    }
}