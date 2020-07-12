using System.Collections.Generic;
using System.Linq;
using Drone;
using Enemy;
using TileLocation;
using UnityEngine;

namespace Common
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] protected GameObject playerDronePrefab;
        [SerializeField] protected GameObject enemyDronePrefab;
        [SerializeField] private int turnCounter;

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

        private void OnEnable()
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
            CurrentDrone = PlayerDrone = playerPrefab.GetComponent<Drone.Drone>();
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
            // Drone takes turn
            CurrentDrone.TakeTurn();
            
            // Rotate drones and take next
            CurrentDrone = Drones.Dequeue();
            Drones.Enqueue(CurrentDrone);

            // Spawn enemy sometimes
            if (CurrentDrone != PlayerDrone) return;
            turnCounter += 1;
            if (turnCounter > 10 || turnCounter > 5 && turnCounter % 2 == 0 || turnCounter < 5 && turnCounter % 3 == 0)
            {
                SpawnEnemy();
            }
        }
    }
}