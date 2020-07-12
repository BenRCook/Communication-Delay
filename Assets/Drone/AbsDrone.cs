using System.Collections.Generic;
using System.Linq;
using Action;
using Common;
using JetBrains.Annotations;
using TileLocation;
using UI;
using UnityEngine;

namespace Drone
{
    public abstract class AbsDrone : MonoBehaviour
    {
        [field: SerializeField] public int Health { get; protected set; } = 10;
        [field: SerializeField] public HexDirection Facing { get; protected set; }
        [field: SerializeField] public HexLocation Location { get; protected set; }
        [field: SerializeField] public virtual Queue<IAction> Actions { get; } = new Queue<IAction>();
        

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
            ActionFrameController.Instance.UpdateFrames();
        }

        public void TakeNextAction()
        {
            Actions.Dequeue().TakeAction(this);
        }

        public abstract void TakeTurn();
    }
}