using Action;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActionFrameController : MonoBehaviour
    {
        private IAction[] _actions;

        public Text actionText1;
        public Text actionText2;
        public Text actionText3;
        
        public static ActionFrameController Instance { get; private set; }
        
        public void UpdateFrames()
        {
            _actions = GameController.Instance.PlayerDrone.Actions.ToArray();
            actionText1.text = ButtonController.Instance.NextAction;
            actionText2.text = _actions[1].GetDescription();
            actionText3.text = _actions[0].GetDescription();
        }

        private void Start()
        {
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

    }
}