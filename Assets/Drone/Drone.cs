using System.Linq;
using TileLocation;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Drone
{
    public class Drone : AbsDrone
    {
        private const int MissileDamage = 10;
        private const int MissileRange = 10;
        private int _missileAmmo = 1;
        private const int LaserDamage = 10;
        private const int LaserRange = 10;
        private const int KineticDamage = 10;
        private const int KineticRange = 3;

        private AbsDrone[] _drones;
        

        // Start is called before the first frame update
        private void Start()
        {
            _drones = FindObjectsOfType<AbsDrone>();
            // MoveTo(new HexLocation(-2, -1, +3));
        }
        
        // // Update is called once per frame
        // void Update()
        // {
        //
        // }

        public override void MoveTo(HexLocation newLocation)
        {
            Location = newLocation;
            transform.position = newLocation.GetPixelLocation();
           
        }

        public override void LaserAttack(HexDirection direction)
        {
            Facing = direction;
            // TODO Animations
            _drones
                .Where(drone => drone.Location.IsVisibleFrom(Location, direction))
                .Where(drone => drone.Location.DistanceFrom(Location) < LaserRange)
                .ToList()
                .ForEach(drone => drone.TakeDamage(LaserDamage));
        }

        public override void KineticAttack(AbsDrone target)
        {
            // TODO Animations
            if (target.Location.DistanceFrom(Location) < KineticRange)
            {
                target.TakeDamage(KineticDamage);
            }
        }

        public override void MissileAttack(AbsDrone target)
        {
            if (_missileAmmo <= 0) return; // TODO failure animation

            // TODO Animations
            _missileAmmo -= 1;
            if (target.Location.DistanceFrom(Location) < MissileRange)
            {
                target.TakeDamage(MissileDamage);
            }
        }
        
    }
}
