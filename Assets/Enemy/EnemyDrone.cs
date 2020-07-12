using System.Linq;
using Drone;
using TileLocation;

namespace Enemy
{
    public class EnemyDrone : AbsAiDrone
    {
        private const int LaserDamage = 10;
        public static int LaserRange { get; } = 10;
        private const int KineticDamage = 10;
        public static int KineticRange { get; } = 3;
        public static int MoveLimit { get; } = 3;

        private void Start()
        {
            droneAi = gameObject.AddComponent<EnemyAi>();
        }
        
        public override void MoveTo(HexLocation newLocation)
        {
            Location = newLocation;
            transform.position = newLocation.GetPixelLocation();
        }

        public override void LaserAttack(HexDirection direction)
        {
            Facing = direction;
            GameController.GameController.Instance.Drones
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

        public override void MissileAttack(AbsDrone target) { }
    }
}