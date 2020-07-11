using System;
using System.Collections.Generic;
using Action;
using TileLocation;

namespace Drone
{
    public interface IDrone
    {
        int Health { get; }
        HexDirection Facing { get; }
        HexLocation Location { get; }
        Queue<IAction> Actions { get; }
        
        void MoveTo(HexLocation newLocation);
        void LaserAttack(HexDirection direction);
        void KineticAttack(IDrone target);
        void MissileAttack(IDrone target);
        void TakeDamage(int damage);
        void PushAction(IAction action);
        void TakeNextAction();
    }
}