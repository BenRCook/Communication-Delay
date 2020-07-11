using System;
using TileLocation;

namespace Drone
{
    using HexLocation = Tuple<int, int, int>;
    public interface IDrone
    {
        void MoveTo(HexLocation newLocation);
        void LaserAttack(HexDirection direction);
        void KineticAttack(IDrone target);
        void MissileAttack(IDrone target);
        HexLocation GetLocation();
        bool IsVisibleFrom(HexLocation location, HexDirection direction);
        int DistanceFrom(HexLocation location);
    }
}