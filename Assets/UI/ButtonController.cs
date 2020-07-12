using Action;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public delegate IAction ActionCreator(Vector3 worldPoint);
    public class ButtonController : MonoBehaviour
    {
        [field: SerializeField] public string CurrentButton { get; private set; }
        private Camera _mainCamera;
        private IAction _nextAction;

        private IAction Action
        {
            get => _nextAction;
            set
            {
                _nextAction = value;
                ActionFrameController.Instance.UpdateFrames();
            }
        }

        public string NextAction => Action is null ? "" : Action.GetDescription();
        
        public static ButtonController Instance { get; private set; }

        public void Start()
        {
            _mainCamera = Camera.main;
        }

        public void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            var controller = GameController.Instance;
            switch (CurrentButton)
            {
                case "move":
                    CurrentButton = "";
                    AttemptCreate(ActionBuilder.MakeMove);
                    break;
                case "missile":
                    CurrentButton = "";
                    AttemptCreate(ActionBuilder.MakeMissileAttack);
                    break;
                case "kinetic":
                    CurrentButton = "";
                    AttemptCreate(ActionBuilder.MakeKineticAttack);
                    break;
                case "lazer":
                    CurrentButton = "";
                    var worldPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    Action = new LaserAttack(controller.PlayerDrone.Location.NearestDirection(worldPoint));
                    break;
            }
        }

        private void AttemptCreate(ActionCreator creator)
        {
            var worldPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            try
            {
                Action = creator(worldPoint);
                ErrorDisplay.Instance.Message = "";
                if (Action.GetDescription() != "Missile" || GameController.Instance.PlayerDrone.MissileAmmo > 0) return;
                Action = null;
                ErrorDisplay.Instance.Message = "Out of missiles!";
            }
            catch (UserInputError e)
            {
                ErrorDisplay.Instance.Message = e.Message;
            }
        }

        public void NextTurn()
        {
            var controller = GameController.Instance;
            CurrentButton = "";
            ErrorDisplay.Instance.Message = "";
            if (controller.CurrentDrone == controller.PlayerDrone)
            {
                PlayerTurn();
            }
            else
            {
                controller.AdvanceTurn();
            }
        }

        private void PlayerTurn()
        {
            var controller = GameController.Instance;
            if (Action is null)
            {
                ErrorDisplay.Instance.Message = "Choose an action before advancing turn";
                return;
            }
            if (Action.GetDescription() == "Missile")
            {
                controller.PlayerDrone.MissileAmmo -= 1;
                GameObject.Find("Missile Button").GetComponentInChildren<Text>().text =
                    $"Missile ({controller.PlayerDrone.MissileAmmo})";
            }
            controller.PlayerDrone.PushAction(Action);
            Action = null;
            ErrorDisplay.Instance.Message = "";
            controller.AdvanceTurn();
            ActionFrameController.Instance.UpdateFrames();
        }
        
        public void ButtonPress(string buttonType)
        {
            CurrentButton = buttonType;
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
