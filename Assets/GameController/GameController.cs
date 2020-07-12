using System.Collections.Generic;
using System.Linq;
using Drone;
using Enemy;
using TileLocation;
using UnityEngine;

namespace GameController
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] protected GameObject playerDronePrefab;
        [SerializeField] protected GameObject enemyDronePrefab;
        [SerializeField] protected GameObject deathParticlePrefab;
        
        public static GameController Instance { get; private set; }

        public Queue<AbsDrone> Drones { get; private set; } = new Queue<AbsDrone>();
        public Drone.Drone PlayerDrone { get; private set; }
        public AbsDrone CurrentDrone { get; private set; }

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
            // Cleanup (shouldn't be important)
            foreach (var drone in Drones)
            {
                Destroy(drone);
            }
            
            // Initializing game
            var hexLocation = new HexLocation(-7, 6, 1);
            // var hexLocation = new HexLocation(1, 1, -2);
            var playerLocation = hexLocation.GetPixelLocation();
            var playerPrefab = Instantiate(playerDronePrefab, playerLocation, Quaternion.identity);
            PlayerDrone = playerPrefab.GetComponent<Drone.Drone>();
            PlayerDrone.MoveTo(hexLocation);
            Drones = new Queue<AbsDrone>();
            Drones.Enqueue(PlayerDrone);
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            var center = new HexLocation(6, -4, -2);
            var location = Utilities.RandomChoice(
                center.Neighbours().Select(item => item.Item1).Append(center)
            );
            var coords = location.GetPixelLocation();
            var enemyPrefab = Instantiate(enemyDronePrefab, coords, Quaternion.identity);
            var enemy = enemyPrefab.GetComponent<EnemyDrone>();
            enemy.TakeDamage(3);
            enemy.MoveTo(location);
            Drones.Enqueue(enemy);
        }

        public void AdvanceTurn()
        {
            // Rotate drones and take next
            CurrentDrone = Drones.Dequeue();
            Drones.Enqueue(CurrentDrone);
            
            // Drone takes turn
            CurrentDrone.TakeTurn();
        }

        public void Kill(AbsDrone drone)
        {
            var particleLocation = drone.Location.GetPixelLocation();
            Drones = new Queue<AbsDrone>(Drones.Where(d => d != drone));
            
            Destroy(drone.gameObject);
            Instantiate(deathParticlePrefab, particleLocation, Quaternion.identity);
        }
    }
}