using System.Collections.Generic;
using Action;
using TileLocation;
using UnityEngine;
using UI;

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

        private LineRenderer _laser;
        private Camera _mainCamera;

        private void Start()
        {
            _laser = GetComponent<LineRenderer>();
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (ButtonController.Instance.currentButton == "lazer")
            {
                _laser.enabled = true;
                var worldPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var aim = Location.DrawLineTo(worldPoint, LaserRange);
                _laser.SetPosition(0, Location.GetPixelLocation());
                _laser.SetPosition(1, aim.GetPixelLocation());
            }
            else
            {
                _laser.enabled = false;
            }
        }

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
