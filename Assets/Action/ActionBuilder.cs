using Common;
using TileLocation;
using UnityEngine;

namespace Action
{
    public class ActionBuilder
    {
        public static Move MakeMove(Vector3 mouseLocation)
        {
            var hex = HexLocation.FromPixels(mouseLocation);
            if (!Utilities.Instance.IsTileWalkable(hex))
                throw new UserInputError("Tile is inaccessible!");
            return new Move(hex);
        }

        public static KineticAttack MakeKineticAttack(Vector3 mouseLocation)
        {
            var tile = HexLocation.FromPixels(mouseLocation);
            var drone = Utilities.FindDroneOnTile(tile);
            if (drone is null)
            {
                throw new UserInputError("Drone not found on that tile");
            }
            return new KineticAttack(drone);
        }

        public static MissileAttack MakeMissileAttack(Vector3 mouseLocation)
        {
            var tile = HexLocation.FromPixels(mouseLocation);
            var drone = Utilities.FindDroneOnTile(tile);
            if (drone is null)
            {
                throw new UserInputError("Drone not found on that tile");
            }
            return new MissileAttack(drone);
        }
    }
}