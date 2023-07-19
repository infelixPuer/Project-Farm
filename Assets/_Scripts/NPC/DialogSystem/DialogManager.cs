using System;
using _Scripts.UI;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;

namespace _Scripts.DialogSystem
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] 
        private Canvas _dialogCanvas;
        
        [SerializeField]
        private TextMeshProUGUI _storyText;
        
        [SerializeField]
        private GameObject _choicePanel;
        
        [SerializeField]
        private Button _buttonPrefab;
        
        private Story _story;
        private bool _isStoryNeeded;
        
        public static DialogManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }

        private void Update()
        {
            if (_isStoryNeeded)
            {
                ClearChoicePanel();
                
                while (_story.canContinue)
                {
                    _storyText.text += _story.Continue();
                }
            
                if (_story.currentChoices.Count > 0)
                {
                    for (int i = 0; i < _story.currentChoices.Count; ++i)
                    {
                        var choice = Instantiate(_buttonPrefab, _choicePanel.transform);
                        var choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
                        choiceText.text = _story.currentChoices[i].text;
                        var index = i;
                        choice.onClick.AddListener(() => SelectChoice(index));
                    }
                }

                if (!_story.canContinue && _story.currentChoices.Count == 0)
                {
                    var endDialogButton = Instantiate(_buttonPrefab, _choicePanel.transform);
                    var endDialogText = endDialogButton.GetComponentInChildren<TextMeshProUGUI>();
                    endDialogText.text = "End dialog";
                    
                    endDialogButton.onClick.AddListener(() =>
                    {
                        UIManager.Instance.HideCanvas(_dialogCanvas);
                        _storyText.text = "";
                        _story = null;
                        _isStoryNeeded = false;

                        ClearChoicePanel();
                    });
                }

                _isStoryNeeded = false;
            }
        }

        public void StartDialog(TextAsset inkJson)
        {
            _story = new Story(inkJson.text);
            _isStoryNeeded = true;
            UIManager.Instance.ShowCanvas(_dialogCanvas);
        }
        
        // TODO: Make method for choosing options in dialog. This method will be assigned to buttons and it will be toogling _isStoryNeeded field
        public void SelectChoice(int index) 
        {
            _story.ChooseChoiceIndex(index);
            _isStoryNeeded = true;
        }
        
        private void ClearChoicePanel()
        {
            foreach (Transform child in _choicePanel.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        public void SetStory(TextAsset inkJson) => _story = new Story(inkJson.text);
    }
}