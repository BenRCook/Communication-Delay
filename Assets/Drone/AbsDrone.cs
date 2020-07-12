using System.Collections.Generic;
using Action;
using GameController;
using TileLocation;
using UnityEngine;

namespace Drone
{
    public abstract class AbsDrone : MonoBehaviour
    {
        [field: SerializeField] public int Health { get; protected set; } = 10;
        [field: SerializeField] public HexLocation Location { get; protected set; }
        [field: SerializeField] private Queue<IAction> Actions { get; } = new Queue<IAction>();

        public abstract void MoveTo(HexLocation newLocation);
        public void QueueMove(Vector3 mouseLocation)
        {
            PushAction(new Move(HexLocation.FromPixels(mouseLocation)));
        }

        public abstract void LaserAttack(HexDirection direction);
        public void QueueLaserAttack(Vector3 mouseLocation)
        {
            PushAction(new LaserAttack(Location.NearestDirection(mouseLocation)));
        }

        public abstract void KineticAttack(AbsDrone target);
        public void QueueKineticAttack(Vector3 mouseLocation)
        {
            var tile = HexLocation.FromPixels(mouseLocation);
            var drone = Utilities.FindDroneOnTile(tile);
            if (drone is null)
            {
                throw new MissingComponentException("Drone not found");
            }
            PushAction(new KineticAttack(drone));
        }
        
        public abstract void MissileAttack(AbsDrone target);
        public void QueueMissileAttack(Vector3 mouseLocation)
        {
            var tile = HexLocation.FromPixels(mouseLocation);
            var drone = Utilities.FindDroneOnTile(tile);
            if (drone is null)
            {
                throw new MissingComponentException("Drone not found");
            }
            PushAction(new MissileAttack(drone));
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                GameController.GameController.Instance.Kill(this);
            }
        }

        public void PushAction(IAction action)
        {
            Actions.Enqueue(action);
        }

        public void TakeNextAction()
        {
            Actions.Dequeue().TakeAction(this);
        }

        public abstract void TakeTurn();
    }
}