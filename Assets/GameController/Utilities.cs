using System;
using System.Collections.Generic;
using System.Linq;
using Drone;
using JetBrains.Annotations;
using TileLocation;

namespace GameController
{
    public static class Utilities
    {
        public static T RandomChoice<T> (IEnumerable<T> source) {
            var rnd = new Random();
            var result = default(T);
            var cnt = 0;
            foreach (var item in source) {
                cnt++;
                if (rnd.Next(cnt) == 0) {
                    result = item;
                }
            }
            return result;
        }
        
        [CanBeNull]
        public static AbsDrone FindDroneOnTile(HexLocation tile)
        {
            return GameController.Instance.Drones
                .FirstOrDefault(drone => drone.Location == tile);
        }
    }
    
}