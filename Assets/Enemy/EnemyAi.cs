using Action;
using UnityEngine;

namespace Enemy
{
    public class EnemyAi : MonoBehaviour
    {
        private static IAction ChooseAction(EnemyDrone drone)
        {
            var drones = FindObjectsOfType<Drone.Drone>();
            return new Move(1, 2);
        }

        public static void AddAction(EnemyDrone drone)
        {
            drone.PushAction(ChooseAction(drone));
        }
    }
}