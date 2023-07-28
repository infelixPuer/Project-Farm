using System.Collections;
using _Scripts.NPCes;
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
        
        [SerializeField]
        private ScrollRect _dialogScrollRect;

        [SerializeField] 
        private PlayerMovement _playerMovement;

        [HideInInspector] 
        public NPC NPCToTalk;
        
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
                    StartCoroutine(ApplyScrollPosition(_dialogScrollRect, 0f));
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
                        NPCManager.Instance.StartAllNPC();

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
            _playerMovement.enabled = false;
            StartCoroutine(RotateTowardsInterloctor());
            UIManager.Instance.ShowCanvas(_dialogCanvas);
        }
        
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

        private IEnumerator ApplyScrollPosition(ScrollRect sr, float value)
        {
            yield return new WaitForEndOfFrame();
            
            sr.verticalNormalizedPosition = value;
        }

        private IEnumerator RotateTowardsInterloctor()
        {
            var targetDirection = NPCToTalk.transform.position - _playerMovement.transform.position;

            while (Quaternion.Angle(_playerMovement.transform.rotation, Quaternion.LookRotation(targetDirection)) > 0.1f)
            {
                _playerMovement.transform.rotation = Quaternion.RotateTowards(_playerMovement.transform.rotation, Quaternion.LookRotation(targetDirection), 20 * Time.deltaTime);
                yield return null;
            }
            
            NPCManager.Instance.StopAllNPC();
        }
        
        public void SetStory(TextAsset inkJson) => _story = new Story(inkJson.text);
    }
}