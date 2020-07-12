using System.Collections.Generic;
using System.Linq;
using Drone;
using TileLocation;
using UnityEngine;

namespace Action
{
    public class MoveStepByStep : IAction
    {
        private readonly IEnumerable<HexDirection> _moves;

        public MoveStepByStep(IEnumerable<HexDirection> moves)
        {
            _moves = moves;
        }

        public string GetDescription()
        {
            return $"Move drone";
        }

        public void TakeAction(AbsDrone drone)
        {
            foreach (var offset in _moves.Select(move => HexLocation.DirectionMappings[move]))
            {
                drone.MoveTo(drone.Location + offset);
            }
        }

        public Color GetBackgroundColor()
        {
            return new Color(0, 255, 0, 0.7F);
        }
    }
}