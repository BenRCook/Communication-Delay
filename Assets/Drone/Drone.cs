using System;
using Action;
using System.Collections.Generic;
using TileLocation;
using UnityEngine;

namespace Drone
{
    public class Drone : MonoBehaviour, IDrone
    {
        [SerializeField] private int health;
        [SerializeField] private int movementLeft;
        [SerializeField] private Queue<IAction> actions;
        [SerializeField] private HexDirection facing;

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

        public HexLocation GetLocation()
        {
            throw new NotImplementedException();
        }

        public int GetHealth()
        {
            return health;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
        }

        public void PushAction(IAction action)
        {
            actions.Enqueue(action);
        }

        public void TakeNextAction()
        {
            actions.Dequeue().TakeAction(this);
        }
    }
}
