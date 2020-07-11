using Drone;
using UnityEngine;

namespace Action
{
    public class MissileAttack : IAction
    {
        private readonly AbsDrone _target;

        public MissileAttack(AbsDrone target)
        {
            _target = target;
        }

        public string GetDescription()
        {
            return $"Shoot laser at target";
        }

        public void TakeAction(AbsDrone drone)
        {
            drone.MissileAttack(_target);
        }

        public Color GetBackgroundColor()
        {
            return new Color(255, 0, 0, 0.7F);
        }
    }
}