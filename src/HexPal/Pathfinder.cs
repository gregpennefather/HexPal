using System.Collections.Generic;
using System.Linq;

namespace HexPal
{
    public static class Pathfinder
    {
        public static IList<Hex> BreathSearch(Hex origin, Hex target) {
            var frontier = new List<Hex>();
            var path = new List<Hex>();
            var cameFrom = new Dictionary<Hex, Hex>();

            cameFrom[origin] = origin;
            frontier.Add(origin);

            while(frontier.Any()) {
                var current = frontier.First();

                if (current == target) {
                    break;
                }

                foreach(var next in current.Neighbours()) {
                    if (!cameFrom.ContainsKey(next)) {
                        frontier.Add(next);
                        cameFrom[next] = current;
                    }
                }

                frontier.RemoveAt(0);
            }

            var step = target;
            while(step != origin) {
                path.Add(step);
                step = cameFrom[step];
            }
            path.Reverse();
            return path.ToList();
        }
    }
}