using Drone;
using TileLocation;
using UnityEngine;

namespace Action
{
    public class Move : IAction
    {
        private readonly HexLocation _newPosition;

        public Move(HexLocation location)
        {
            _newPosition = location;
        }

        public string GetDescription()
        {
            return $"Move";
        }

        public void TakeAction(AbsDrone drone)
        {
            drone.MoveTo(_newPosition);
        }

        public Color GetBackgroundColor()
        {
             return new Color(0, 255, 0, 0.7F);
        }
    }
}