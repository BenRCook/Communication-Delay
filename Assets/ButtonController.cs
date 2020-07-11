using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public String currentButton;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            switch (currentButton)
            {
                case "move":
                    Debug.Log("move(" + Input.mousePosition + ")");
                    break;
                case "missile":
                    Debug.Log("Missile(" + Input.mousePosition + ")");
                    break;
                case "kinetic":
                    Debug.Log("Kinetic(" + Input.mousePosition + ")");
                    break;
                case "lazer":
                    Debug.Log("Missile(" + Input.mousePosition + ")");
                    break;
                case "":
                    Debug.Log("No button is selected / the button has no value");
                    break;
                default:
                    Debug.Log("This Button Does Nothing");
                    break;
            }
        }
    }
    public void ButtonPress(String buttonType)
    {
        currentButton = buttonType;
    }
}
