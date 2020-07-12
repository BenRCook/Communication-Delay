using Drone;
using UnityEngine;

namespace Action
{
    public class KineticAttack : IAction
    {
        private readonly AbsDrone _target;

        public KineticAttack(AbsDrone target)
        {
            _target = target;
        }

        public string GetDescription()
        {
            return $"Kinetic";
        }

        public void TakeAction(AbsDrone drone)
        {
            drone.KineticAttack(_target);
        }

        public Color GetBackgroundColor()
        {
            return new Color(255, 0, 0, 0.7F);
        }
    }
}