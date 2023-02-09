using System;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    private float startTime;
    
    [SerializeField]
    private float tickDuration;
    
    private TextMeshProUGUI _timerText;
    
    private float _remainingTime;
    private float _tickTime;
    private bool _isRunning;
    private bool _hasText;

    public TimeSpan RemainingTime => TimeSpan.FromSeconds(_remainingTime);

    public event EventHandler TimeOver;
    public event EventHandler Tick;
    
    private void Start()
    {
        _remainingTime = startTime;
        _timerText = GetComponent<TextMeshProUGUI>();
        _hasText = _timerText != null;
    }

    private void Update()
    {
        if (!_isRunning)
            return;
        
        _tickTime += Time.deltaTime;
        if (_tickTime > tickDuration)
        {
            _tickTime = 0f;
            UpdateText();
            Tick?.Invoke(this, EventArgs.Empty);
        }
        
        _remainingTime -= Time.deltaTime;
        if (_remainingTime < 0f)
            TimeOver?.Invoke(this, EventArgs.Empty);
    }

    public void Run()
    {
        _isRunning = true;
    }

    public void Stop()
    {
        _isRunning = false;
    }

    public void ResetTimer()
    {
        _remainingTime = startTime;
        _isRunning = false;
        UpdateText();
    }
    
    public void AppendTime(TimeSpan time)
    {
        _remainingTime += (float)time.TotalSeconds;
        UpdateText();
    }

    public void SetTime(TimeSpan time)
    {
        _remainingTime = (float)time.TotalSeconds;
        UpdateText();
    }

    private void UpdateText()
    {
        if (!_hasText)
            return;
        
        _timerText.text = RemainingTime.ToString("ss");
    }
}