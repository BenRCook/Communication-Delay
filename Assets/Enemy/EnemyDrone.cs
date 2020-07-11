using System.Linq;
using Drone;
using TileLocation;

namespace Enemy
{
    public class EnemyDrone : AbsDrone
    {
        private const int LaserDamage = 10;
        private const int LaserRange = 10;
        private const int KineticDamage = 10;
        private const int KineticRange = 3;

        private AbsDrone[] _drones;

        private void Start()
        {
            _drones = FindObjectsOfType<AbsDrone>();
        }
        
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

        public override void MissileAttack(AbsDrone target) { }
    }
}