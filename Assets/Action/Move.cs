using System;
using UnityEngine;

namespace Action
{
    public class Move : IAction
    {
        private Tuple<int,int,int> _newPosition;

        public Move(int newX, int newY, int newZ)
        {
            _newPosition = Tuple.Create(newX, newY, newZ);
        }

        public string GetDescription()
        {
            return $"Move to {_newPosition}";
        }

        public Drone.Drone TakeAction(Drone.Drone drone)
        {
            throw new System.NotImplementedException();
        }

        public Color GetBackgroundColor()
        {
            throw new System.NotImplementedException();
        }
    }
}