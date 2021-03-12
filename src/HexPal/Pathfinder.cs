using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HexPal
{
    public static class Pathfinder
    {
        public static IList<Hex> BreathSearch(Hex origin, Hex target)
        {
            var frontier = new List<Hex>();
            var path = new List<Hex>();
            var cameFrom = new Dictionary<Hex, Hex>();

            cameFrom[origin] = origin;
            frontier.Add(origin);

            while (frontier.Any())
            {
                var current = frontier.First();

                if (current == target)
                {
                    break;
                }

                foreach (var next in current.Neighbours())
                {
                    if (!cameFrom.ContainsKey(next))
                    {
                        frontier.Add(next);
                        cameFrom[next] = current;
                    }
                }

                frontier.RemoveAt(0);
            }

            var step = target;
            while (step != origin)
            {
                path.Add(step);
                step = cameFrom[step];
            }
            path.Reverse();
            return path.ToList();
        }

        public static IList<Hex> AStar(Hex start, Hex goal, Dictionary<Hex, float> pathingInfo)
        {
            return AStar(start, new List<Hex>() { goal }, pathingInfo);
        }

        public static IList<Hex> AStar(Hex start, IList<Hex> goal, Dictionary<Hex, float> pathingInfo)
        {
            var frontier = new PriorityQueue<float, Hex>();
            var path = new List<Hex>();

            var cameFrom = new Dictionary<Hex, Hex>();
            var costSoFar = new Dictionary<Hex, float>();

            var pathableGoals = goal.Where(g => pathingInfo.ContainsKey(g));
            if (pathableGoals.Count() == 0) {
                return null;
            }

            frontier.Add(0, start);
            cameFrom[start] = start;
            costSoFar[start] = 0.0f;

            Hex foundTarget = null;

            while (frontier.Any())
            {
                var current = frontier.Pop();

                if (pathableGoals.Contains(current))
                {
                    foundTarget = current;
                    break;
                }

                if (!pathingInfo.ContainsKey(current)) {
                    continue;
                }

                foreach (var next in current.Neighbours())
                {
                    var newCost = costSoFar[current] + pathingInfo[current];
                    if (!cameFrom.ContainsKey(next) || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;

                        var closestTarget = pathableGoals.Select(t => next.DistanceTo(t)).Min();

                        var priority = newCost + closestTarget;
                        frontier.Add(priority, next);
                        cameFrom[next] = current;
                    }
                }
            }

            if (foundTarget == null) {
                return null;
            }

            while (foundTarget != start)
            {
                path.Add(foundTarget);
                foundTarget = cameFrom[foundTarget];
            }
            path.Reverse();


            return path;
        }

        public static IList<Hex> AStar(Hex start, Hex goal, IList<Hex> pathingInfo) {
            var dict = new Dictionary<Hex, float>();

            foreach(var tile in pathingInfo) {
                dict[tile] = 1;
            }

            return AStar(start, goal, dict);
        }

        public static IList<Hex> Range(Hex start, float range, Dictionary<Hex, float> pathingInfo) {
            var result = new List<Hex>();

            var frontier = new PriorityQueue<float, Hex>();
            frontier.Add(0, start);
            pathingInfo.Remove(start);

            while(frontier.Any()) {
                var current = frontier.PopFull();

                foreach (var next in current.Item2.Neighbours())
                {
                    if(!pathingInfo.ContainsKey(next))
                    {
                        continue;
                    }

                    var nextWeight = pathingInfo[next];

                    var newDistance = current.Item1 + nextWeight;
                    if (next.DistanceTo(start) <= range && newDistance <= range) {
                        frontier.Add(newDistance, next);
                        result.Add(next);
                        pathingInfo.Remove(next);
                    }
                }
            }

            return result;
        }


        internal class PriorityQueue<F, T> : IEnumerable<T> where F: IComparable
        {
            private LinkedList<(F,T)> list = new LinkedList<(F,T)>();

            public void Add(F key, T value) {
                LinkedListNode<(F,T)> addBefore = null;

                var current = list.First;
                while(current != null) {
                    if(key.CompareTo(current.Value.Item1) <= 0) {
                        addBefore = current;
                        break;
                    }

                    current = current.Next;
                }

                (F,T) pairValue = (key, value);

                if (addBefore != null) {
                    list.AddBefore(addBefore, pairValue);
                } else {
                    list.AddLast(pairValue);
                }
            }

            public T Pop() {
                var value = list.First.Value.Item2;
                list.RemoveFirst();
                return value;
            }

            public (F, T) PopFull() {
                var value = list.First.Value;
                list.RemoveFirst();
                return value;
            }

            public IEnumerator<T> GetEnumerator()
            {
                return list.Select(item => item.Item2).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return list.GetEnumerator();
            }
        }
    }

}