using Drone;
using UnityEngine;

namespace Action
{
    public class Wait : IAction
    {
        public string GetDescription()
        {
            return $"Wait";
        }

        public void TakeAction(AbsDrone drone)
        {
            
        }

        public Color GetBackgroundColor()
        {
            return new Color(255, 255, 0, 0.7F);
        }
    }
}