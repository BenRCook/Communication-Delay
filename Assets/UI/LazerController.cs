using System;
using UnityEngine;

namespace UI
{
    public class LazerController : MonoBehaviour
    {
        private GameObject drone;
        private LineRenderer lazer;
        

        private void Update()
        {
            // if (ButtonController.Instance.currentButton == "lazer")
            // {
                lazer.enabled = true;
                // lazer.SetPosition(0, drone.gameObject.transform.position);
                lazer.SetPosition(0, new Vector3(10f, 10f));
                lazer.SetPosition(1, new Vector3(0f, 0f));
                Debug.Log(lazer.GetPosition(0));
                
                
            // }
            // else
            // {
            //     lazer.enabled = false;
            // }
        }

        private void Start()
        {
            drone = GameController.GameController.Instance.PlayerDrone.gameObject;
            lazer = drone.GetComponent<LineRenderer>();
        }
    }
}