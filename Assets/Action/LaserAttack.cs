using Drone;
using TileLocation;
using UnityEngine;

namespace Action
{
    public class LaserAttack : IAction
    {
        private readonly HexDirection _direction;

        public LaserAttack(HexDirection direction)
        {
            _direction = direction;
        }

        public string GetDescription()
        {
            return $"Shoot laser in direction";
        }

        public void TakeAction(AbsDrone drone)
        {
            drone.LaserAttack(_direction);
        }

        public Color GetBackgroundColor()
        {
            return new Color(255, 0, 0, 0.7F);
        }
    }
}