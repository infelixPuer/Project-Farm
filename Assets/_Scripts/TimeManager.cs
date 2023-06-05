using System;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] 
    private float _timeMultipier;

    [SerializeField] 
    private int _startHour;

    [SerializeField] 
    private TextMeshProUGUI _textMeshPro;

    public static TimeManager Instance;
    
    private DateTime _currentTime;
    private int _dayCounter;
    private bool _isNewDay;

    public bool TimeBlocked;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }

    private void Start()
    {
        _currentTime = DateTime.Now.Date + TimeSpan.FromHours(_startHour);
    }

    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        if (TimeBlocked) return;
        
        _currentTime = _currentTime.AddSeconds(Time.deltaTime * _timeMultipier);
        
        if (_currentTime.Hour == 0 && _currentTime.Minute == 0 && !_isNewDay)
        {
            _dayCounter++;
            _isNewDay = true;
        }
        else if (_currentTime.Hour == 0 && _currentTime.Minute != 0)
        {
            _isNewDay = false;
        }

        _textMeshPro.text = _currentTime.ToString("HH:mm") + "\nDay: " + _dayCounter;
    }

    public int GetCurrentDay() => _dayCounter;
    
    public DateTime GetCurrentTime() => _currentTime;
}
