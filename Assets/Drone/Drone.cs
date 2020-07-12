using System.Collections.Generic;
using System.Linq;
using Action;
using TileLocation;
using UnityEngine;

namespace Drone
{
    public class Drone : AbsDrone
    {
        [field: SerializeField] public override Queue<IAction> Actions { get; } = new Queue<IAction>(new []
        {
            new Wait(), new Wait(),
        });
        
        private const int MissileDamage = 10;
        private const int MissileRange = 10;
        private int _missileAmmo = 1;
        private const int LaserDamage = 10;
        private const int LaserRange = 10;
        private const int KineticDamage = 10;
        private const int KineticRange = 3;
        public override int Health { get; protected set; } = 30;

        public override void MoveTo(HexLocation newLocation)
        {
            Location = newLocation;
            transform.position = newLocation.GetPixelLocation();
           
        }

        public override void LaserAttack(HexDirection direction)
        {
            Common.GameController.Instance.Drones
                .Where(drone => drone.Location.IsVisibleFrom(Location, direction))
                .Where(drone => drone.Location.DistanceFrom(Location) < LaserRange)
                .ToList()
                .ForEach(drone => drone.TakeDamage(LaserDamage));
        }

        public override void KineticAttack(AbsDrone target)
        {
            if (target.Location.DistanceFrom(Location) < KineticRange)
            {
                target.TakeDamage(KineticDamage);
            }
        }

        public override void MissileAttack(AbsDrone target)
        {
            if (_missileAmmo <= 0) return;

            _missileAmmo -= 1;
            if (target.Location.DistanceFrom(Location) < MissileRange)
            {
                target.TakeDamage(MissileDamage);
            }
        }

        public override void TakeTurn()
        {
            TakeNextAction();
        }
    }
}
