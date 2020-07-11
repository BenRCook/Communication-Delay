using System;
using Action;
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
        int GetHealth();
        void TakeDamage(int damage);
        void PushAction(IAction action);
        void TakeNextAction();
    }
}