using System.Collections.Generic;
using System.Linq;
using Drone;
using JetBrains.Annotations;
using TileLocation;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

namespace Common
{
    public class Utilities : MonoBehaviour
    {
        public static Utilities Instance { get; private set; }
        private Tilemap _tilemap;
        [SerializeField] protected Sprite limit;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            _tilemap = FindObjectOfType<Tilemap>();
        }

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

        public bool IsTileWalkable(HexLocation tile)
        {
            if (!(FindDroneOnTile(tile) is null))
                return false;
            var pixelLocation = tile.GetPixelLocation();
            var spriteLocation = new Vector3Int((int) pixelLocation.x, (int) pixelLocation.y, 0);
            return _tilemap.GetSprite(spriteLocation) != limit;
        }
    }
    
}