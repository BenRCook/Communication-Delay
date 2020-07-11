using Drone;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ButtonController : MonoBehaviour
    {
        public String currentButton;
        private Drone.Drone drone;

        public void Awake()
        {
            drone = FindObjectOfType<Drone.Drone>();
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                switch (currentButton)
                {
                    case "move":
                        Debug.Log("move(" + Input.mousePosition + ")");
                        drone.QueueMove(worldPoint);
                        break;
                
                    case "missile":
                        Debug.Log("Missile(" + Input.mousePosition + ")");
                        drone.QueueMissileAttack(worldPoint);
                        break;
                    case "kinetic":
                        Debug.Log("Kinetic(" + Input.mousePosition + ")");
                        drone.QueueKineticAttack(worldPoint);
                        break;
                    case "lazer":
                        Debug.Log("lazer(" + Input.mousePosition + ")");
                        drone.QueueLaserAttack(worldPoint);
                        break;
                    case "nextTurn":
                        Debug.Log("nextTurn(" + Input.mousePosition + ")");
                        drone.TakeNextAction();
                        break;
                    case "":
                        Debug.Log("No button is selected / the button has no value, " + currentButton);
                        break;
                    default:
                        Debug.Log("This button has not been implemented");
                        break;
                }
            }
        }
        public void ButtonPress(String buttonType)
        {
            currentButton = buttonType;
        }
    }

}
