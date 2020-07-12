using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;

namespace TileLocation
{
    public readonly struct HexLocation
    {
        public HexLocation(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Bottom left to top right
        private int X { get; }
        // Bottom right to top left
        private int Y { get; }
        // Top to bottom
        private int Z { get; }

        public static readonly Dictionary<HexDirection, HexLocation> DirectionMappings = 
            new Dictionary<HexDirection, HexLocation>
            {
                [HexDirection.TopRight] = new HexLocation(1, 0, -1),
                [HexDirection.Right] = new HexLocation(1, -1, 0),
                [HexDirection.BottomRight] = new HexLocation(0, -1, 1),
                [HexDirection.BottomLeft] = new HexLocation(-1, 0, 1),
                [HexDirection.Left] = new HexLocation(-1, 1, 0),
                [HexDirection.TopLeft] = new HexLocation(0, 1, -1),
            };
        
        public Vector3 GetPixelLocation()
        {
            // Get Unity location vector
            var xPos = (X + (0.5 * Z));
            var yPos = -(0.75 * Z);
            return new Vector3((float)xPos, (float)yPos, 0f);
        }

        public static HexLocation FromPixels(Vector3 position)
        {
            var z = (int) Math.Round((-position.y) / 0.75);
            var x = (int) Math.Round(position.x - (0.5 * z));
            var y = -x - z;
            return new HexLocation(x, y, z);
        }

        public HexDirection NearestDirection(Vector3 position)
        {
            const double angleSize = Math.PI / 3;
            var angle = VectorToAngle(position);
            var direction = (HexDirection) (int) Math.Floor(angle / angleSize);
            return direction;
        }

        private double VectorToAngle(Vector3 position)
        {
            var currentPos = GetPixelLocation();
            var angle =  Math.PI - Mathf.Atan2(position.y - currentPos.y, position.x - currentPos.x);
            var adjusted = (angle + 1.5 * Math.PI) % (2 * Math.PI);
            return adjusted;
        }

        private int ToDistance()
        {
            return (Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z)) / 2;
        }

        public int DistanceFrom(HexLocation other)
        {
            return (this - other).ToDistance();
        }
        
        public bool IsVisibleFrom(HexLocation location, HexDirection direction)
        {
            switch (direction)
            {
                case HexDirection.TopRight:
                    return location.X > X && location.Y == Y && location.Z < Z;
                case HexDirection.Right:
                    return location.X > X && location.Y < Y && location.Z == Z;
                case HexDirection.BottomRight:
                    return location.X == X && location.Y < Y && location.Z > Z;
                case HexDirection.BottomLeft:
                    return location.X < X && location.Y == Y && location.Z > Z;
                case HexDirection.Left:
                    return location.X < X && location.Y > Y && location.Z == Z;
                case HexDirection.TopLeft:
                    return location.X == X && location.Y > Y && location.Z < Z;
                default:
                    // Should never happen
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public IEnumerable<HexDirection> ShortestPath(HexLocation target)
        {
            var cameFrom = new Dictionary<HexLocation, List<HexDirection>> {{this, new List<HexDirection>()}};
            var options = new List<(HexLocation, int)> {(this, 0)};
            while (options.Count > 0)
            {
                var current = options.First().Item1;
                options.RemoveAt(0);
                var route = cameFrom[current];

                if (current == target)
                {
                    return route;
                }

                var walkableNeighbours = current.Neighbours()
                    .Where(t => Utilities.Instance.IsTileWalkable(t.Item1));
                foreach (var (next, direction) in walkableNeighbours)
                {
                    var nextRoute = route.Append(direction).ToList();
                    if (cameFrom.ContainsKey(next) && nextRoute.Count >= cameFrom[next].Count) continue;
                    cameFrom[next] = nextRoute;
                    options.Add((next, nextRoute.Count + next.DistanceFrom(target)));
                    options.Sort((a, b) => a.Item2.CompareTo(b.Item2));
                }
            }
            return new List<HexDirection>();
        }

        public IEnumerable<(HexLocation, HexDirection)> Neighbours()
        {
            var origin = new HexLocation(X, Y, Z);
            return DirectionMappings.
                Select(neighbour => (neighbour.Value + origin, neighbour.Key));
        }

        public HexLocation DrawLineTo(Vector3 mousePosition, int length)
        {
            var direction = NearestDirection(mousePosition);
            var offset = DirectionMappings[direction] * length;
            return this + offset;
        }

        private bool Equals(HexLocation other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            return obj is HexLocation other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Z;
                return hashCode;
            }
        }

        public static HexLocation operator +(HexLocation a) => a;
        public static HexLocation operator -(HexLocation a) => new HexLocation(-a.X, -a.Y, -a.Z);

        public static HexLocation operator +(HexLocation a, HexLocation b) => 
            new HexLocation(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static HexLocation operator -(HexLocation a, HexLocation b) => 
            new HexLocation(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        
        public static HexLocation operator *(HexLocation a, int b) =>
            new HexLocation(a.X * b, a.Y * b, a.Z * b);

        public static bool operator ==(HexLocation a, HexLocation b) => a.Equals(b);
        public static bool operator !=(HexLocation a, HexLocation b) => !(a == b);
    }
}