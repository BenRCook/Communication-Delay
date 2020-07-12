using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Action;
using Drone;
using TileLocation;
using UnityEngine;

namespace Enemy
{
    public class EnemyAi : MonoBehaviour, IDroneAi
    {
        private bool _chasing;


        public IAction ChooseAction(AbsDrone drone)
        {
            var drones = new List<Drone.Drone>{GameController.GameController.Instance.PlayerDrone};
            var close = drones
                .FirstOrDefault(other => other.Location.DistanceFrom(drone.Location) <= EnemyDrone.KineticRange);
            
            if (!(close is null))
            {
                _chasing = false;
                return new KineticAttack(close);
            }

            var inLine = drones
                .Where(other => CanSeeAnyDirection(drone.Location, other.Location))
                .FirstOrDefault(other => drone.Location.DistanceFrom(other.Location) < EnemyDrone.LaserRange);
            
            if (!(inLine is null) && ! _chasing)
            {
                _chasing = true;
                return new LaserAttack(DirectionTo(drone.Location, inLine.Location));
            }

            var min = drones.Min(other => other.Location.DistanceFrom(drone.Location));
            var closest = drones.First(other => other.Location.DistanceFrom(drone.Location) == min);
            
            return new MoveStepByStep(drone.Location.ShortestPath(closest.Location).Take(EnemyDrone.MoveLimit));
            
        }

        private static bool CanSeeAnyDirection(HexLocation a, HexLocation b)
        {
            return Enum.GetValues(typeof(HexDirection))
                .Cast<HexDirection>()
                .Any(direction => a.IsVisibleFrom(b, direction));
        }

        private static HexDirection DirectionTo(HexLocation origin, HexLocation target)
        {
            foreach (HexDirection direction in Enum.GetValues(typeof(HexDirection)))
            {
                if (target.IsVisibleFrom(origin, direction))
                {
                    return direction;
                }
            }
            throw new InvalidDataException("Cannot see target!");
        }
    }
}