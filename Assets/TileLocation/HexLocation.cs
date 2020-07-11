using System;
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

        public Vector3 GetPixelLocation() // returns the unity coordinate of the tile as a Vector2
        {
            var xPos = (Z + (0.5 * X));
            var yPos = (0.75 * X);
            return new Vector3((float)xPos, (float)yPos, 0f);
        }

        public static HexLocation FromPixels(Vector3 position)
        {
            var x = (int) Math.Round(position.y / 0.75);
            var z = (int) Math.Round(position.x - (0.5 * x));
            var y = -x - z;
            return new HexLocation(x, y, z);
        }

        public HexDirection NearestDirection(Vector3 position)
        {
            const double angleSize = Math.PI / 3;
            var angle = VectorToAngle(position);
            return (HexDirection) (int) Math.Floor(angle / angleSize);
        }

        private double VectorToAngle(Vector3 position)
        {
            const double sectorSize = Math.PI / 2;
            var currentPos = GetPixelLocation();
            var difference = position - currentPos;
            var angle = Math.Atan(Math.Abs(difference.x) / Math.Abs(difference.y));
            // pi/2 size sectors from top right to top left 0 to 3
            var sector = ((difference.x < 0 ? 2 : 0) + (difference.y < 0 ? 0 : 2)) % 4;
            var offset = sector * sectorSize;
            return sector % 2 == 0 ? offset + angle : offset + sectorSize - angle;
        }

        public int ToDistance()
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

        public static HexLocation operator +(HexLocation a) => a;
        public static HexLocation operator -(HexLocation a) => new HexLocation(-a.X, -a.Y, -a.Z);

        public static HexLocation operator +(HexLocation a, HexLocation b) => 
            new HexLocation(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static HexLocation operator -(HexLocation a, HexLocation b) => 
            new HexLocation(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public static Boolean operator ==(HexLocation a, HexLocation b) => 
            a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        public static bool operator !=(HexLocation a, HexLocation b) => !(a == b);
    }
}