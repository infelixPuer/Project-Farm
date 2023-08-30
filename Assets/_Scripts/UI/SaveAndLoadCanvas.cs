using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class SaveAndLoadCanvas : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;
        
        [SerializeField]
        private Button _createNewMapButton;

        [SerializeField]
        private Button _loadMapButton;

        [SerializeField]
        private Button _saveMapButton;

        private void Awake()
        {
            _createNewMapButton.onClick.AddListener(() => UIManager.Instance.HideCanvas(_canvas));
            _loadMapButton.onClick.AddListener(() => UIManager.Instance.HideCanvas(_canvas));
            _saveMapButton.onClick.AddListener(() => UIManager.Instance.HideCanvas(_canvas));
        }
    }
}