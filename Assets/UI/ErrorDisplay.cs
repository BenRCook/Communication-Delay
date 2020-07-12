using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ErrorDisplay : MonoBehaviour
    {
        public static ErrorDisplay Instance { get; private set; }
        private string _message;
        private bool _showing;
        private Text _text;
        private Image _image;

        public string Message
        {
            set
            {
                _message = value;
                if (_message.Length > 0 && ! _showing)
                {
                    _showing = true;
                    _text.text = _message;
                    _text.enabled = _showing;
                    _image.enabled = _showing;
                }
                else if (_message.Length == 0 && _showing)
                {
                    _showing = false;
                    _text.text = _message;
                    _text.enabled = _showing;
                    _image.enabled = _showing;
                }
            }
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

        private void Start()
        {
            _text = gameObject.GetComponentInChildren<Text>();
            _image = gameObject.GetComponent<Image>();
        }
    }

}