using System;
using Action;
using System.Collections.Generic;
using TileLocation;
using UnityEngine;

namespace Drone
{
    public class Drone : MonoBehaviour, IDrone
    {
        [field: SerializeField] public int Health { get; private set; } = 10;
        [field: SerializeField] public Queue<IAction> Actions { get; } = new Queue<IAction>();
        [field: SerializeField] public HexDirection Facing { get; } = HexDirection.Right;
        [field: SerializeField] public HexLocation Location { get; } = new HexLocation(0, 0, 0);

        // // Start is called before the first frame update
        // void Start()
        // {
        //
        // }
        //
        // // Update is called once per frame
        // void Update()
        // {
        //
        // }

        public void MoveTo(HexLocation newLocation)
        {
            throw new NotImplementedException();
        }
        
        public void LaserAttack(HexDirection direction)
        {
            throw new NotImplementedException();
        }

        public void KineticAttack(IDrone target)
        {
            throw new NotImplementedException();
        }

        public void MissileAttack(IDrone target)
        {
            throw new NotImplementedException();
        }

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
