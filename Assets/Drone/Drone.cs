using System;
using Action;
using System.Collections.Generic;
using System.Linq;
using TileLocation;
using UnityEngine;

namespace Drone
{
    public class Drone : MonoBehaviour, IDrone
    {
        [field: SerializeField] public int Health { get; private set; } = 10;
        [field: SerializeField] public Queue<IAction> Actions { get; } = new Queue<IAction>();
        [field: SerializeField] public HexDirection Facing { get; private set; } = HexDirection.Right;
        [field: SerializeField] public HexLocation Location { get; private set; } = new HexLocation(0, 0, 0);

        private const int MissileDamage = 10;
        private const int MissileRange = 10;
        private int _missileAmmo = 1;
        private const int LaserDamage = 10;
        private const int LaserRange = 10;
        private const int KineticDamage = 10;
        private const int KineticRange = 3;

        private Drone[] _drones;
        

        // Start is called before the first frame update
        // TODO Make this work with any IDrone
        private void Start()
        {
            _drones = FindObjectsOfType<Drone>();
        }
        
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
            Facing = direction;
            // TODO Animations
            _drones
                .Where(drone => drone.Location.IsVisibleFrom(Location, direction))
                .Where(drone => drone.Location.DistanceFrom(Location) < LaserRange)
                .ToList()
                .ForEach(drone => drone.TakeDamage(LaserDamage));
        }

        public void KineticAttack(IDrone target)
        {
            // TODO Animations
            if (target.Location.DistanceFrom(Location) < KineticRange)
            {
                target.TakeDamage(KineticDamage);
            }
        }

        public void MissileAttack(IDrone target)
        {
            if (_missileAmmo <= 0) return; // TODO failure animation

            // TODO Animations
            _missileAmmo -= 1;
            if (target.Location.DistanceFrom(Location) < MissileRange)
            {
                target.TakeDamage(MissileDamage);
            }
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
