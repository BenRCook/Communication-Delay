using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public String currentButton;

    private void OnMouseDown()
    {
        switch (currentButton)
        {
            case "move":
                Console.WriteLine("move(" + Input.mousePosition + ")");
                break;
            case "missile":
                Console.WriteLine("Missile(" + Input.mousePosition + ")");
                break;
            case "kinetic":
                Console.WriteLine("Kinetic(" + Input.mousePosition + ")");
                break;
            case "lazer":
                Console.WriteLine("Missile(" + Input.mousePosition + ")");
                break;
            default:
                Console.WriteLine("This Button Does Nothing");
                break;
        }
    }

    public void ButtonPress(String buttonType)
    {
        currentButton = buttonType;
    }
}
