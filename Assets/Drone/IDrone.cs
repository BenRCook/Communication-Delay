using System;
using TileLocation;

namespace Drone
{
    public interface IDrone
    {
        void MoveTo(HexLocation newLocation);
        void LaserAttack(HexDirection direction);
        void KineticAttack(IDrone target);
        void MissileAttack(IDrone target);
        HexLocation GetLocation();
        bool IsVisibleFrom(HexLocation location, HexDirection direction);
        int DistanceFrom(HexLocation location);
        int GetHealth();
        void TakeDamage(int damage);
    }
}