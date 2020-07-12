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

        public Text actionText1;
        public Text actionText2;
        public Text actionText3;
        
        public static ActionFrameController Instance { get; private set; }
        
        public void UpdateFrames()
        {
            Actions = GameController.GameController.Instance.PlayerDrone.Actions.ToArray();
            actionText1.text = Actions[0].GetDescription();
            actionText2.text = Actions[1].GetDescription();
            actionText3.text = Actions[2].GetDescription();
        }

        private void Start()
        {
            // actionFrames = new GameObject[]
            // {
            //     GameObject.Find("Action Frame 1"), GameObject.Find("Action Frame 2"), GameObject.Find("Action Frame 3")
            // };
            // actionTexts = new Text[]
            // {
            //     GameObject.Find("Action Frame 1").GetComponentInChildren<Text>(),
            //     GameObject.Find("Action Frame 2").GetComponentInChildren<Text>(),
            //     GameObject.Find("Action Frame 3").GetComponentInChildren<Text>()
            // };
            actionText1 = GameObject.Find("Action Frame 1").GetComponentInChildren<Text>();
            actionText2 = GameObject.Find("Action Frame 2").GetComponentInChildren<Text>();
            actionText3 = GameObject.Find("Action Frame 3").GetComponentInChildren<Text>();
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