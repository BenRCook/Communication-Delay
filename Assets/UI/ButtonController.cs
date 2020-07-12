using Drone;
using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace UI
{
    public delegate void Action(Vector3 worldPoint);
    public class ButtonController : MonoBehaviour
    {
        public string currentButton;
        public Camera mainCamera;

        public void Start()
        {
            mainCamera = Camera.main;
        }

        public void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            var controller = Common.GameController.Instance;
            switch (currentButton)
            {
                case "move":
                    Debug.Log("move(" + Input.mousePosition + ")");
                    AttemptAction(controller.PlayerDrone.QueueMove);
                    break;
                case "missile":
                    Debug.Log("Missile(" + Input.mousePosition + ")");
                    AttemptAction(controller.PlayerDrone.QueueMissileAttack);
                    break;
                case "kinetic":
                    Debug.Log("Kinetic(" + Input.mousePosition + ")");
                    AttemptAction(controller.PlayerDrone.QueueKineticAttack);
                    break;
                case "lazer":
                    Debug.Log("lazer(" + Input.mousePosition + ")");
                    AttemptAction(controller.PlayerDrone.QueueLaserAttack);
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

        private void AttemptAction(Action droneAction)
        {
            var worldPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            try
            {
                droneAction(worldPoint);
                ErrorDisplay.Instance.Message = "";
            }
            catch (UserInputError e)
            {
                ErrorDisplay.Instance.Message = e.Message;
            }
        }
        
        public void ButtonPress(string buttonType)
        {
            currentButton = buttonType;
        }
    }

}
