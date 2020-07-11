using System;

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

        private int X { get; }
        private int Y { get; }
        private int Z { get; }

        public (int, int) GetPixelLocation()
        {
            int radius = 50;
            double x = Math.Sqrt(3.0) * radius * (Z / 2 + X);
            double y = 3 / 2 * radius * Z;
            return ((int, int)) (x, y);
        }

        public static HexLocation operator +(HexLocation a) => a;
        public static HexLocation operator -(HexLocation a) => new HexLocation(-a.X, -a.Y, -a.Z);

        public static HexLocation operator +(HexLocation a, HexLocation b) => 
            new HexLocation(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static HexLocation operator -(HexLocation a, HexLocation b) => 
            new HexLocation(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }
}