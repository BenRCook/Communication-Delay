using System;
using UnityEngine;

namespace TileLocation
{
    public readonly struct HexLocation
    {
        private const double TileWidth = 1;
        private const double TileHeight = 1;
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

        public Vector2 GetPixelLocation() // returns the unity coordinate of the tile as a Vector2
        {
            double xPos = ((X - Y) * (TileWidth / 2));
            double yPos = ((0.5 * (X + Y)) ) * TileHeight;
            return new Vector2((float)xPos, (float)yPos);
        }

        public int ToDistance()
        {
            return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
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
    }
}