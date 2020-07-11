using System;
using System.Collections.Generic;
using Action;
using TileLocation;
using UnityEngine;

namespace Drone
{
    public abstract class AbsDrone : MonoBehaviour
    {
        [field: SerializeField] public int Health { get; protected set; }
        [field: SerializeField] public HexDirection Facing { get; protected set; }
        [field: SerializeField] public HexLocation Location { get; protected set; }
        [field: SerializeField] private Queue<IAction> Actions { get; } = new Queue<IAction>();
        
        public abstract void MoveTo(HexLocation newLocation);
        public abstract void LaserAttack(HexDirection direction);
        public abstract void KineticAttack(AbsDrone target);
        public abstract void MissileAttack(AbsDrone target);

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public void PushAction(IAction action)
        {
            Actions.Enqueue(action);
        }

        public void TakeNextAction()
        {
            Actions.Dequeue().TakeAction(this);
        }
    }
}