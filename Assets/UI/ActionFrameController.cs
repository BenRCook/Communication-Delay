using System;
using System.Collections.Generic;
using System.Net.Mime;
using Action;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActionFrameController : MonoBehaviour
    {
        private IAction[] Actions;
        
        public void UpdateFrames()
        {
            Actions = GameController.GameController.Instance.PlayerDrone.Actions.ToArray();
            GameObject.Find("Action Frame 1").GetComponentInChildren<Text>().text = Actions[0].GetDescription();
            GameObject.Find("Action Frame 2").GetComponentInChildren<Text>().text = Actions[1].GetDescription();
            GameObject.Find("Action Frame 3").GetComponentInChildren<Text>().text = Actions[2].GetDescription();

        }

        

        // public void Update()
        // {
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         UpdateFrames();
        //         Debug.Log("update");
        //     }
        //     
        // }
    }
}