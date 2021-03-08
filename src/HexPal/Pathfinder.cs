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

        public static IList<Hex> AStar(Hex origin, Hex target, IList<Tuple<float, Hex>> validPositions)
        {
            var frontier = new PriorityQueue<float, Hex>();
            var path = new List<Hex>();

            var cameFrom = new Dictionary<Hex, Hex>();
            var costSoFar = new Dictionary<Hex, float>();


            frontier.Add(0, origin);
            cameFrom[origin] = origin;
            costSoFar[origin] = 0.0f;

            while (frontier.Any())
            {
                var current = frontier.Pop();

                if (current == target)
                {
                    break;
                }

                foreach (var next in current.Neighbours())
                {
                    var newCost = costSoFar[current] + current.DistanceTo(next);
                    if (!cameFrom.ContainsKey(next) || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        var priority = newCost + next.DistanceTo(target);
                        frontier.Add(priority, next);
                        cameFrom[next] = current;
                    }
                }
            }


            var step = target;
            while (step != origin)
            {
                path.Add(step);
                step = cameFrom[step];
            }
            path.Reverse();


            return path;
        }


        internal class PriorityQueue<F, T> : IEnumerable<T> where F: IComparable
        {
            private LinkedList<Tuple<F, T>> list = new LinkedList<Tuple<F, T>>();

            public void Add(F key, T value) {
                LinkedListNode<Tuple<F,T>> addBefore = null;

                var current = list.First;
                while(current != null) {
                    if(key.CompareTo(current.Value.Item1) <= 0) {
                        addBefore = current;
                        break;
                    }

                    current = current.Next;
                }

                var pairValue = new Tuple<F,T>(key, value);

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