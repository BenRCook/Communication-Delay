using Drone;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ButtonController : MonoBehaviour
    {
        public static ButtonController Instance { get; private set; }
        public string currentButton;
        
        public void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            var controller = GameController.GameController.Instance;
            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            switch (currentButton)
            {
                case "move":
                    Debug.Log("move(" + Input.mousePosition + ")");
                    controller.PlayerDrone.QueueMove(worldPoint);
                    break;
                
                case "missile":
                    Debug.Log("Missile(" + Input.mousePosition + ")");
                    controller.PlayerDrone.QueueMissileAttack(worldPoint);
                    break;
                case "kinetic":
                    Debug.Log("Kinetic(" + Input.mousePosition + ")");
                    controller.PlayerDrone.QueueKineticAttack(worldPoint);
                    break;
                case "lazer":
                    Debug.Log("lazer(" + Input.mousePosition + ")");
                    controller.PlayerDrone.QueueLaserAttack(worldPoint);
                    break;
                case "nextTurn":
                    Debug.Log("nextTurn(" + Input.mousePosition + ")");
                    controller.AdvanceTurn();
                    break;
                case "":
                    Debug.Log("No button is selected / the button has no value, " + currentButton);
                    break;
                default:
                    Debug.Log("This button has not been implemented");
                    break;
            }
        }
        public void ButtonPress(string buttonType)
        {
            currentButton = buttonType;
        }
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
    }

}
