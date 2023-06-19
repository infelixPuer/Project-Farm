using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        
        [SerializeField]
        private Canvas _persistentCanvas;

        [SerializeField] 
        private PlayerMovement _playerMovement;

        private Canvas _activeCanvas;
        private List<Canvas> _canvasList;

        private Stack<Canvas> _history = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }

            _canvasList = GetComponentsInChildren<Canvas>().ToList();
            _canvasList.Remove(_persistentCanvas);
            _canvasList.ForEach(x => x.gameObject.SetActive(false));
            _persistentCanvas.gameObject.SetActive(true);
        }

        public void ShowCanvas(Canvas canvas)
        {
            if (_history.Count > 0)
            {
                var previousCanvas = _history.Peek();
                previousCanvas.gameObject.SetActive(false);
            }
            
            _history.Push(canvas);
            canvas.gameObject.SetActive(true);
            _playerMovement.enabled = false;
            TimeManager.Instance.TimeBlocked = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        
        public void HideCanvas(Canvas canvas)
        {
            if (_history.Count > 0 && _history.Peek() != canvas) return;
            
            if (_history.Count > 0)
            {
                var previousCanvas = _history.Pop();
                previousCanvas.gameObject.SetActive(false);
            }
            
            _playerMovement.enabled = true;
            TimeManager.Instance.TimeBlocked = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}